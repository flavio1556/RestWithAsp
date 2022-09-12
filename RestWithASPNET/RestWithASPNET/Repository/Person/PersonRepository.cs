using RestWithASPNET.Models.Context;
using RestWithASPNET.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestWithASPNET.Repository.Person
{
    public class PersonRepository : GenericRepository<Models.Entites.Person>, IPersonRepository
    {
        public PersonRepository(MySQLContext context) : base(context)
        {

        }

        public Models.Entites.Person Disable(long Id)
        {
            if(!Exists(Id)) return null;
            var person = FindById(Id);
            if(person == null) return null;
            person.Enabled = false;
            try
            {
                _context.Entry(person).CurrentValues.SetValues(person);
                save();
                return person;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Models.Entites.Person> FindByName(string firstName, string secondName)
        {
            if (!string.IsNullOrWhiteSpace(firstName) && !string.IsNullOrWhiteSpace(secondName))
            {
                return _dataSet.Where(p => p.FirstName.Contains(firstName) && p.LastName.Contains(secondName)).ToList();
            }
            else if (!string.IsNullOrWhiteSpace(firstName) && string.IsNullOrWhiteSpace(secondName))
            {
                return _dataSet.Where(p => p.FirstName.Contains(firstName)).ToList();

            } 
            else if (string.IsNullOrWhiteSpace(firstName) && !string.IsNullOrWhiteSpace(secondName))
            {
                return _dataSet.Where(p => p.LastName.Contains(secondName)).ToList();
            }
            return null;

        }
    }
}
