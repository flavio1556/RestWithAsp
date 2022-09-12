using RestWithASPNET.Business.Interfaces;
using System;
using System.Collections.Generic;
using RestWithASPNET.Models.Entites;
using RestWithASPNET.Repository.Generic;
using RestWithASPNET.Data.VO;
using RestWithASPNET.Data.Converter.Implementations;
using RestWithASPNET.Repository.Person;
using RestWithASPNET.HyperMedia.Utils;
using System.Linq;

namespace RestWithASPNET.Business.Implementations
{
    public class PersonBusinessImplementations : IPersonBusiness
    {
        private readonly IPersonRepository _repository;
        private readonly PersonConverter _converter;
        public PersonBusinessImplementations(IPersonRepository repository)
        {
            _repository = repository;
            _converter = new PersonConverter();
        }
        public List<PersonVO> FindAll()
        {

            return _converter.Parse(_repository.FindAll());
        }
        public List<PersonVO> FindByName(string firstName, string secondName)
        {
            return _converter.Parse(_repository.FindByName(firstName, secondName));
        }

        public PersonVO FindById(long id)
        {
            try
            {
                var result = _repository.FindById(id);
                return _converter.Parse(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public PersonVO Create(PersonVO person)
        {

            try
            {
                var personEntity = _converter.Parse(person);
                personEntity = _repository.Create(personEntity);

                return _converter.Parse(personEntity);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public PersonVO Update(PersonVO person)
        {
            if (!_repository.Exists(person.Id)) return new PersonVO();
            try
            {
                var personEntity = _converter.Parse(person);
                personEntity = _repository.Update(personEntity);
                return _converter.Parse(personEntity);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public PersonVO Disable(long Id)
        {
            try
            {
                var entityperson = _repository.Disable(Id);
                return _converter.Parse(entityperson);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void Delete(long id)
        {
            if (_repository.Exists(id))
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

        public PagedSarchVO<PersonVO> FindWithPagedSarch(string name, string sortDirection,
            int pageSize, int Page)
        {

            var sort = (!string.IsNullOrWhiteSpace(sortDirection) && !sortDirection.Equals("desc")) ? "asc" : "desc";
            var size = (pageSize < 1) ? 10 : pageSize;
            var offset = Page > 0 ? (Page - 1) * size : 0;
            string query = @"select * from person p Where 1=1 ";
            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query + $"and p.first_name like '%{name}%' "; 
            }
            query += $"order by p.first_name {sort} limit {size} offset {offset}";
            var person = _repository.FindWithPagedSearch(query);
            var totalresult = person.Count();
            return new PagedSarchVO<PersonVO>
            {
                CurrentPage = Page,
                list = _converter.Parse(person),
                PageSize = size,
                SortDirections = sortDirection,
                TotalResults   = totalresult
            };
        }
    }
}
