namespace Kolokwium.DTOs;

public class BookDTO
{
    public int id { get; set; }
    public string title { get; set; }
    public List<string> genres { get; set; }

    public BookDTO()
    {
        genres = new List<string>();
    }
}