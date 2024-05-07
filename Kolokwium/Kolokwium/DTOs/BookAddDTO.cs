namespace Kolokwium.DTOs;

public class BookAddDTO
{
    public int id { get; set; }
    public string title { get; set; }
    public List<GenreDTO> genres { get; set; }

    public BookAddDTO()
    {
        genres = new List<GenreDTO>();
    }
}