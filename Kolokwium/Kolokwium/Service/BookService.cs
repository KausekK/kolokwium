using System.Data;
using System.Data.SqlClient;
using Kolokwium.DTOs;

namespace Kolokwium.Service;

public class BookService
{
    private readonly IConfiguration _configuration;

    public BookService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public BookDTO GetBookInfo(int id)
    {
        using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default")))
        {
            connection.Open();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connection;
                command.CommandText =
                    "SELECT b.pk, b.title, g.name FROM books b " +
                    "JOIN books_genres bg ON bg.FK_book = b.PK " +
                    "JOIN genres g ON bg.FK_genre = g.PK " +
                    "WHERE b.pk = @id";

                command.Parameters.AddWithValue("@Id", id);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        var book = new BookDTO();
                        book.genres = new List<string>();
                        while (reader.Read())
                        {
                            book.id = reader.GetInt32(0);
                            book.title = reader.GetString(1);
                            book.genres.Add(reader.GetString(2));
                        }
                        return book;
                    }
                    return null;
                }
            }
        }
    }
    
    public int InsertBook(BookAddDTO bookDto)
    {
        int bookId = -1;
        using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default")))
        {
            connection.Open();
            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                SqlCommand command = connection.CreateCommand();
                command.Transaction = transaction;
                try
                {
                    command.CommandText = "INSERT INTO books (title) VALUES (@title); SELECT SCOPE_IDENTITY();";
                    command.Parameters.AddWithValue("@title", bookDto.title);
                    bookId = Convert.ToInt32(command.ExecuteScalar());
            
                   
                       
                            command.Parameters.Clear();
                            command.CommandText = "INSERT INTO books_genres (FK_book, FK_genre) VALUES (@book, @genre)";
                            command.Parameters.AddWithValue("@book", bookId);
                            command.Parameters.AddWithValue("@genre",bookDto.genres );
                            command.ExecuteNonQuery();
                        
                    transaction.Commit();
                    return bookId;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Failed to insert book", ex);
                }
            }
        }
    }

}
