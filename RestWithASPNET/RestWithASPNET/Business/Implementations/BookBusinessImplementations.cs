using RestWithASPNET.Business.Interfaces;
using RestWithASPNET.Data.Converter.Implementations;
using RestWithASPNET.Data.VO;
using RestWithASPNET.HyperMedia.Utils;
using RestWithASPNET.Models.Entites;
using RestWithASPNET.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestWithASPNET.Business.Implementations
{
    public class BookBusinessImplementations : IBookBusiness
    {
        private readonly IRepository<Book> _repository;
        private readonly BookConverter _converter;
        public BookBusinessImplementations(IRepository<Book> repository)
        {
            _repository = repository;
            _converter = new BookConverter();
        }
        public BookVO FindById(long id)
        {
            try
            {
                return _converter.Parse(_repository.FindById(id));

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<BookVO> FindAll()
        {
            try
            {
                return _converter.Parse(_repository.FindAll());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public BookVO Create(BookVO book)
        {
            try
            {
                var bookEntity = _converter.Parse(book);
                bookEntity = _repository.Create(bookEntity);
                return _converter.Parse(bookEntity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        public BookVO Update(BookVO book)
        {
            try
            {
                var bookEntity = _converter.Parse(book);
                bookEntity = _repository.Update(bookEntity);
                return _converter.Parse(bookEntity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void Delete(long id)
        {
            try
            {
                _repository.Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public PagedSarchVO<BookVO> FindWithPagedSarch(string title, string sortDirection, int pageSize, int Page)
        {
            PagedSarchVO<BookVO> pagedSarch = new PagedSarchVO<BookVO>();
            pagedSarch.CurrentPage = Page;
            pagedSarch.PageSize = (pageSize <1)? 10: pageSize;
            pagedSarch.SortDirections = (sortDirection.Equals("desc"))  && (!string.IsNullOrWhiteSpace(sortDirection)) ? "asc" : "desc";
            var offset = Page > 0 ? (Page - 1) * pagedSarch.PageSize : 0;
            string query = @"select * from book b Where 1=1 ";
            if (!string.IsNullOrWhiteSpace(title))
            {
                query = query + $"and b.title like '%{title}%' ";
            }
            query += $"order by b.title {pagedSarch.SortDirections} limit {pagedSarch.PageSize} offset {offset}";
            var books = _repository.FindWithPagedSearch(query);
            
            var totalresult = books.Count();


            pagedSarch.list = _converter.Parse(books);
            pagedSarch.TotalResults = totalresult;


            return pagedSarch;

        }
    }
}
