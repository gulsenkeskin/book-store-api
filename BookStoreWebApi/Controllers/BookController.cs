using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BookStoreWebApi.AddControllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private static List<Book> BookList = new List<Book>(){
            new Book{
                Id=1,
                Title="Learn Startup",
                GenreId=1,
                PageCount=200,
                PublishDate=new DateTime(2001,06,12)
            },
               new Book{
                Id=2,
                Title="Herland",
                GenreId=2,
                PageCount=250,
                PublishDate=new DateTime(2001,06,12)
            }


    };


        // [HttpGet]
        // public List<Book> GetBooks()
        // {
        //     var bookList = BookList.OrderBy(x => x.Id).ToList<Book>();
        //     return bookList;
        // }

        [HttpGet("{id}")]
        public Book? GetById(int id)
        {
            var book = BookList.Where(book => book.Id == id).SingleOrDefault();
            return book;
        }

        [HttpGet]
        public Book? Get([FromQuery] string id)
        {
            var book = BookList.Where(book => book.Id == Convert.ToInt32(id)).SingleOrDefault();
            return book;
        }

    }
}

