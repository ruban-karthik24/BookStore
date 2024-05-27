using BookStore.Models;
using BookStore.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public BooksController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("sorted-by-publisher-author-title")]
        public async Task<IActionResult> GetBooksSortedByPublisherAuthorTitle()
        {
            try
            {
                var books = await _unitOfWork.Books.GetAllBooksSortedByPublisherAuthorTitleAsync();
               // return Ok();
                return Ok(books);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("sorted-by-author-title")]
        public async Task<IActionResult> GetBooksSortedByAuthorTitle()
        {
            try
            {
                var books = await _unitOfWork.Books.GetAllBooksSortedByAuthorTitleAsync();
                return Ok(books);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("total-price")]
        public async Task<IActionResult> GetTotalPrice()
        {
            try
            {
                var totalPrice = await _unitOfWork.Books.GetTotalPriceAsync();
                return Ok(totalPrice);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddBooks([FromBody] IEnumerable<Book> books)
        {
            try
            {
                await _unitOfWork.Books.AddBooksAsync(books);
                await _unitOfWork.CompleteAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("citations")]
        public async Task<IActionResult> GetBooksCitations()
        {
            try
            {
                var books = await _unitOfWork.Books.GetAllBooksAsync();
                var citations = books.Select(b => new
                {
                    b.MLACitation,
                    b.ChicagoCitation
                });

                return Ok(citations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
