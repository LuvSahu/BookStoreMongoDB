using ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IBookBL
    {
        public BookModel AddBook(BookModel addbook, string adminid);

        public BookModel UpdateBook(BookModel editbook, string id, string adminid);

        public bool DeleteBook(string id);

        public IEnumerable<BookModel> GetAllBook();

        public BookModel GetByBookId(string id);

    }
}
