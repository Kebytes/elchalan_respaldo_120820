//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

//namespace ChalanWeb.Models
//{
//    public class MapsClass
//    {
//    }
//}
namespace ChalanWeb.Models
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class MapsObject
    {
        [JsonProperty("plus_code")]
        public PlusCode PlusCode { get; set; }

        [JsonProperty("results")]
        public Result[] Results { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public partial class PlusCode
    {
        [JsonProperty("compound_code")]
        public string CompoundCode { get; set; }

        [JsonProperty("global_code")]
        public string GlobalCode { get; set; }
    }

    public partial class Result
    {
        [JsonProperty("address_components")]
        public AddressComponent[] AddressComponents { get; set; }

        [JsonProperty("formatted_address")]
        public string FormattedAddress { get; set; }

        [JsonProperty("geometry")]
        public Geometry Geometry { get; set; }

        [JsonProperty("place_id")]
        public string PlaceId { get; set; }

        [JsonProperty("types")]
        public string[] Types { get; set; }

        [JsonProperty("plus_code", NullValueHandling = NullValueHandling.Ignore)]
        public PlusCode PlusCode { get; set; }
    }

    public partial class AddressComponent
    {
        [JsonProperty("long_name")]
        public string LongName { get; set; }

        [JsonProperty("short_name")]
        public string ShortName { get; set; }

        [JsonProperty("types")]
        public string[] Types { get; set; }
    }

    public partial class Geometry
    {
        [JsonProperty("location")]
        public Location Location { get; set; }

        [JsonProperty("location_type")]
        public string LocationType { get; set; }

        [JsonProperty("viewport")]
        public Bounds Viewport { get; set; }

        [JsonProperty("bounds", NullValueHandling = NullValueHandling.Ignore)]
        public Bounds Bounds { get; set; }
    }

    public partial class Bounds
    {
        [JsonProperty("northeast")]
        public Location Northeast { get; set; }

        [JsonProperty("southwest")]
        public Location Southwest { get; set; }
    }

    public partial class Location
    {
        [JsonProperty("lat")]
        public double Lat { get; set; }

        [JsonProperty("lng")]
        public double Lng { get; set; }
    }

    //public partial class Welcome
    //{
    //    public static Welcome FromJson(string json) => JsonConvert.DeserializeObject<Welcome>(json, QuickType.Converter.Settings);
    //}

    //public static class Serialize
    //{
    //    public static string ToJson(this Welcome self) => JsonConvert.SerializeObject(self, QuickType.Converter.Settings);
    //}

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}