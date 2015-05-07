using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Helpers;
using System.Web.Security;
using Newtonsoft.Json;

namespace YoutubeConnectWeb.Models
{
    public class RoomModelDB : DbContext
    {
        public RoomModelDB()
            : base("RoomDB")
        { 
        
        }
        public DbSet<RoomData> RoomData { get; set; }
    }

    public class ControlModel
    {
        [Display(Name = "Json")]
        public string jsonData { get; set; }
    }

    public class RoomSetupModel 
    {
        [Required]
        [Display(Name = "Room Name")]
        public string roomName { get; set; }
    }

    public class SendJsonRoomModel
    {
        [Required]
        [Display(Name = "Json Data")]
        public string jsonData { get; set; }
    }

    [Table("UserProfile")]
    public class RoomData
    {
        [Key]
        public string Roomname { get; set; }

        public List<UrlData> data { get; set; }
    }
    [Table("UrlData")]
    [JsonObject(ItemTypeNameHandling = TypeNameHandling.Objects)]
    public class UrlData
    {

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [JsonProperty(PropertyName = "id")]
        public virtual int id { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string url { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string title { get; set; }

        [JsonProperty(PropertyName = "Roomname")]
        [ForeignKey("RoomData_Roomname")]
        public virtual string Roomname { get; set; }

        [JsonIgnore]
        public virtual RoomData RoomData_Roomname { get; set; }
    }


}