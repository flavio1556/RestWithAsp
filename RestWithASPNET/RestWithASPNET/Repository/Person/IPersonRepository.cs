using RestWithASPNET.Repository.Generic;
using RestWithASPNET.Models.Entites;
using System.Collections.Generic;

namespace RestWithASPNET.Repository.Person
{
    public interface IPersonRepository : IRepository<Models.Entites.Person>
    {
        Models.Entites.Person Disable(long Id);
        List<Models.Entites.Person> FindByName(string firstName, string secondName);
    }
}
