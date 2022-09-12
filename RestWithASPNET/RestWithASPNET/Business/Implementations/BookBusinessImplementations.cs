using RestWithASPNET.Business.Interfaces;
using RestWithASPNET.Data.Converter.Implementations;
using RestWithASPNET.Data.VO;
using RestWithASPNET.Models.Entites;
using RestWithASPNET.Repository.Generic;
using System;
using System.Collections.Generic;

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

    }
}
