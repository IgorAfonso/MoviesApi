using System.Text.Json.Serialization;

namespace MoviesApi.Models
{
    public class RootObject
    {
        [JsonPropertyName("Data")]
        public List<MovieModel> Data { get; set; } = new List<MovieModel>();
    }


}
