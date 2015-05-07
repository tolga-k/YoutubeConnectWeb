using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using System.Data.Entity;
using System.Web.Routing;
using Newtonsoft.Json;
using YoutubeConnectWeb.Filters;
using YoutubeConnectWeb.Models;

namespace YoutubeConnectWeb.Controllers
{
    public class RoomController : Controller
    {
        RoomModelDB storeDB = new RoomModelDB();
        //
        // GET: /Room/

        public ActionResult Index()
        {
            return View();
        }

        /*
        //
        // GET: /Room/Manage
        [HttpGet]
        public ActionResult Manage(string roomName)
        {

            if (!string.IsNullOrEmpty(roomName))
            {

                var returnData = storeDB.RoomData.Select(rd => rd.data.Where<UrlData>(ud => ud.Roomname == roomName)).FirstOrDefault(find => find.Count() > 0);

                

                if (returnData != null)
                {
                    return View(returnData);
                }
                else
                {


                    return View(new List<UrlData>());
                }

            }
            return View();


        }*/

        [HttpGet]
        public ActionResult Manage(string roomName, string id)
        {

            if (!string.IsNullOrEmpty(roomName))
            {
                var returnData = storeDB.RoomData.Select(rd => rd.data.Where<UrlData>(ud => ud.Roomname == roomName)).FirstOrDefault(find => find.Count() > 0);

                if (id != null)
                {
                    List<UrlData> specific = new List<UrlData>();
                    specific.Add(returnData.FirstOrDefault(first => first.id.Equals(Convert.ToInt32(id))));
                    return View(specific);
                }


                return View(returnData.Any() ? returnData : new List<UrlData>());
            }
            return View();


        }

        
        [HttpPost]
        public ActionResult Control(string jsonData)
        {
            try
            {
                UrlData data = JsonConvert.DeserializeObject<UrlData>(jsonData);
                var singleOrDefault = storeDB.RoomData.SingleOrDefault(nm => nm.Roomname.Equals(data.Roomname));
                if (singleOrDefault != null)
                    singleOrDefault.data.ForEach(rdFor =>
                    {
                        if (rdFor.id.Equals(data.id))
                        {
                            rdFor.title = "DONE";
                        }
                    });
                return View();
                //return RedirectToAction("Control","Room",View);
            }
            catch (Exception)
            {

                throw;
            }
            return HttpNotFound();
        }

        //
        //POST: /Room/Home

        [HttpPost]
        public ActionResult Home(string Roomname)
        {
            if (storeDB.RoomData.Select(taken => taken.Roomname.Equals(Roomname)).Any())
            {
                return RedirectToAction("Manage", "Room", new { Roomname = Roomname });
            }
            if (!string.IsNullOrEmpty(Roomname))
            {
                RoomData rd = new RoomData();
                rd.Roomname = Roomname;
                rd.data = new List<UrlData>();
                rd.data.Add(new UrlData() { url = "test", title = "title", Roomname = rd.Roomname, RoomData_Roomname = rd });
                rd.data.Add(new UrlData() { url = "test2", title = "title2", Roomname = rd.Roomname, RoomData_Roomname = rd });
                rd.data.Add(new UrlData() { url = "test3", title = "title3", Roomname = rd.Roomname, RoomData_Roomname = rd });
                rd.data.Add(new UrlData() { url = "test4", title = "title4", Roomname = rd.Roomname, RoomData_Roomname = rd });

                storeDB.RoomData.Add(rd);
                storeDB.SaveChanges();

                return RedirectToAction("Manage", "Room", new { roomName = rd.Roomname });

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }



        }

        public ActionResult Home()
        {
            ViewBag.Message = "Your room page.";

            return View();
        }

     

        public ActionResult Control()
        {
            ViewBag.Title = "JA";
            return View();
        }

    }
}
