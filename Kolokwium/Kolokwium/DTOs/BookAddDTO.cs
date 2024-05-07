namespace Kolokwium.DTOs;

public class BookAddDTO
{
   
    public string title { get; set; }
    public List<GenreDTO> genres { get; set; }

    public BookAddDTO()
    {
        genres = new List<GenreDTO>();
    }
}