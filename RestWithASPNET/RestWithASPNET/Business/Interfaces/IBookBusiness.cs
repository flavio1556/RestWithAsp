using RestWithASPNET.Data.VO;
using RestWithASPNET.HyperMedia.Utils;
using System.Collections.Generic;

namespace RestWithASPNET.Business.Interfaces
{
    public interface IBookBusiness
    {
        BookVO FindById(long id);
        List<BookVO> FindAll();
        BookVO Create(BookVO book);
        BookVO Update(BookVO book);
        void Delete(long id);
        PagedSarchVO<BookVO> FindWithPagedSarch(string title, string sortDirection, int pageSize, int Page);
    }
}
