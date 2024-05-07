using Kolokwium.DTOs;
using Kolokwium.Service;
using Microsoft.AspNetCore.Mvc;

namespace Kolokwium.Controllers;
[ApiController]
[Route("/api/book")]
public class BookController : ControllerBase
{
    private readonly BookService _bookService;
    
    public BookController(BookService bookService)
    {
        _bookService = bookService;
    }
    
    [HttpGet("{id}/genres")]
    public IActionResult GetAnimal(int id)
    {
        var book = _bookService.GetBookInfo(id);
        if (book == null)
        {
            return NotFound($"Book with id:{id} does not exist");
        }
        return Ok(book);
    }
    
    [HttpPost]
    public IActionResult InsertBook([FromBody] BookAddDTO bookDto)
    {
        try
        {
            var bookId = _bookService.InsertBook(bookDto);
            if (bookId > 0)
            {
                return Ok(new { Message = "Book added successfully", BookId = bookId });
            }

            return BadRequest("Failed to add the book");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
    
}