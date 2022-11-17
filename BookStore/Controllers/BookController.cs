using BusinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookBL bookBL;

        public BookController(IBookBL bookBL)
        {
            this.bookBL = bookBL;

        }

        [Authorize]
        [HttpPost]
        [Route("AddBook")]
        public ActionResult AddBook(BookModel addbook)
        {
            try
            {
                string adminId = User.Claims.FirstOrDefault(e => e.Type == "AdminId").Value;
                var result = this.bookBL.AddBook(addbook,adminId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Book Added Successfull", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Book Added failed" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [Authorize]
        [HttpPut]
        [Route("UpdateBook")]
        public ActionResult UpdateBook(BookModel editbook, string id)
        {
            try
            {
                string adminId = User.Claims.FirstOrDefault(e => e.Type == "AdminId").Value;
                var result = bookBL.UpdateBook(editbook,id,adminId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Book Update Successfull", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Book Update failed" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("DeleteBook")]
        public ActionResult DeleteBook(string id)
        {
            try
            {
                string adminId = User.Claims.FirstOrDefault(e => e.Type == "AdminId").Value;
                var result = bookBL.DeleteBook(id);
                if (result == true)
                {
                    return Ok(new { success = true, message = "Book deleted Successfull", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Book deleted failed" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [Authorize]
        [HttpGet]
        [Route("Getallbooks")]
        public IActionResult GetAllBook()
        {
            try
            {
                string adminId = User.Claims.FirstOrDefault(e => e.Type == "AdminId").Value;
                IEnumerable<BookModel> ifExists = this.bookBL.GetAllBook();
                if (ifExists != null)
                {
                    return this.Ok(new { Status = true, Message = "Books Retrived Successfully", Data = ifExists });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Books not Found" });
                }

            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }

        [Authorize]
        [HttpGet]
        [Route("Getbooksbyid")]
        public IActionResult GetByBookId(string id)
        {
            try
            {
                string adminId = User.Claims.FirstOrDefault(e => e.Type == "AdminId").Value;
                var ifExists = this.bookBL.GetByBookId(id);
                if (ifExists != null)
                {
                    return this.Ok(new { Status = true, Message = "Book Found Successfully", Data = ifExists });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Book not Found" });
                }

            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }
    }
}
