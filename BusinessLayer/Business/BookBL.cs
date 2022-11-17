using BusinessLayer.Interface;
using ModelLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Business
{
    public class BookBL : IBookBL
    {
        
            private readonly IBookRL bookRL;

            public BookBL(IBookRL bookRL)
            {
                this.bookRL = bookRL;
            }

            public BookModel AddBook(BookModel addbook, string adminid)
            {
                try
                {
                    return bookRL.AddBook(addbook,adminid);
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }

            public BookModel UpdateBook(BookModel editbook, string id,string adminid)
            {
                try
                {
                    return this.bookRL.UpdateBook(editbook,id,adminid);
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }

        public bool DeleteBook(string id)
        {
            try
            {
                return this.bookRL.DeleteBook(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<BookModel> GetAllBook()
        {
            try
            {
                return this.bookRL.GetAllBook();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public BookModel GetByBookId(string id)
        {
            try
            {
                return this.bookRL.GetByBookId(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}

