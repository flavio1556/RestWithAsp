using RestWithASPNET.Data.VO;
using RestWithASPNET.HyperMedia.Utils;
using System.Collections.Generic;

namespace RestWithASPNET.Business.Interfaces
{
    public interface IPersonBusiness

    {
        PersonVO FindById(long id);
        List<PersonVO> FindByName(string firstName, string secondName);
        List<PersonVO> FindAll();
        PersonVO Create(PersonVO person);
        PersonVO Update(PersonVO person);
        PersonVO Disable(long Id);
        void Delete(long id);
        PagedSarchVO<PersonVO> FindWithPagedSarch(string name, string sortDirection, int pageSize, int Page);

    }
}
