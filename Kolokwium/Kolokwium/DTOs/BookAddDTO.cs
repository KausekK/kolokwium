namespace Kolokwium.DTOs;

public class BookAddDTO
{
   
    public string title { get; set; }
    public List<string> genres { get; set; }

    public BookAddDTO()
    {
        genres = new List<string>();
    }
}