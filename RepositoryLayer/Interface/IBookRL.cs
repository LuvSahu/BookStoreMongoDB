using ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IBookRL
    {
        public BookModel AddBook(BookModel addbook, string adminid);

        public BookModel UpdateBook(BookModel editbook,string id, string adminid);

        public bool DeleteBook(string id);

        public IEnumerable<BookModel> GetAllBook();

        public BookModel GetByBookId(string id);





    }
}
