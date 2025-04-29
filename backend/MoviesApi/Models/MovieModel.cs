namespace MoviesApi.Models
{
    public class MovieModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string? Description { get; set; }
        public TimeOnly? Duration { get; set; }
        public decimal? TicketValue { get; set; }
        public DateTime? LastUpdate { get; set; }
    }
}
