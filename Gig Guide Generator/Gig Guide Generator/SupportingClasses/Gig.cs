using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gig_Guide_Generator.SupportingClasses
{
    class Gig
    {
        [JsonProperty("end_time")]
        public DateTime EndTime { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("place")]
        public Place Place { get; set; }
        [JsonProperty("start_time")]
        public DateTime StartTime { get; set; }
        [JsonProperty("id")]
        public string ID { get; set; }

        public string PrintGig()
        {
            return string.Format("*{0}* - {1}\nhttps://www.facebook.com/events/{2}/", Place.Name, Name, ID);
        }
    }

    class Place
    {
        [JsonProperty("name")]
        public string Name;
        [JsonProperty("location")]
        public Location Location;
        [JsonProperty("id")]
        public long ID;
    }

    class Location
    {
        [JsonProperty("city")]
        public string City;
        [JsonProperty("country")]
        public string Country;
        [JsonProperty("latitude")]
        public long Latitude;
        [JsonProperty("longitude")]
        public long Longitude;
        [JsonProperty("street")]
        public string Street;
        [JsonProperty("zip")]
        public string Zip;
    }
}
