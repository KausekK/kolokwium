namespace Kolokwium.DTOs;

public class RequestBookDTO
{
    public string title { get; set; }
    public List<GenreDTO> genres { get; set; }
}