namespace MoviesApi.Models.Request;

public record AddMovieRequest
{
    public int Id;
    public string? Title;
    public DateTime? CreateDate;
    public DateTime? UpdateDate;
    public string? Description;
    public TimeOnly? Duration;
    public decimal? TicketValue;
}