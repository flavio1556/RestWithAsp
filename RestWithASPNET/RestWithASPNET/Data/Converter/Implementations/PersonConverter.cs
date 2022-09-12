using RestWithASPNET.Data.Converter.Contract;
using RestWithASPNET.Data.VO;
using RestWithASPNET.Models.Entites;
using System.Collections.Generic;
using System.Linq;

namespace RestWithASPNET.Data.Converter.Implementations
{
    public class PersonConverter : IParse<PersonVO, Person>, IParse<Person, PersonVO>
    {
        public Person Parse(PersonVO origin)
        {
            if (origin == null) return null;
            return new Person
            {
                Id = origin.Id,
                FirstName = origin.FirstName,
                LastName = origin.LastName,
                Address = origin.Address,
                Gender = origin.Gender,
                Enabled = origin.Enabled
            };
        }
        public PersonVO Parse(Person origin)
        {
            if (origin == null) return null;
            return new PersonVO
            {
                Id = origin.Id,
                FirstName = origin.FirstName,
                LastName = origin.LastName,
                Address = origin.Address,
                Gender = origin.Gender,
                Enabled = origin.Enabled
            };
        }

        public List<Person> Parse(List<PersonVO> listorigin)
        {
            if (listorigin == null) return null;
            return listorigin.Select(item =>Parse(item)).ToList();
        }        

        public List<PersonVO> Parse(List<Person> listorigin)
        {
            if (listorigin == null) return null;
            return listorigin.Select(item => Parse(item)).ToList();
        }
    }
}
