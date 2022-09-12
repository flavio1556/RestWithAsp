using Microsoft.AspNetCore.Mvc;
using RestWithASPNET.Data.VO;
using RestWithASPNET.HyperMedia.Constants;
using System.Text;
using System.Threading.Tasks;

namespace RestWithASPNET.HyperMedia.Enricher
{
    public class PersonEnricher : ContentResponseEnricher<PersonVO>
    {
        protected override Task EnrichModel(PersonVO content, IUrlHelper urlHelper)
        {
            var path = "api/person/v1";
            string linkWithId = GetLink(content.Id, urlHelper, path);
            string linkWithoutId = GetLink(urlHelper, path);
            content.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.Get,
                Href = linkWithId,
                Rel = RelationType.self,
                Type = ResponseTypeFormat.DefaultGet
            });

            content.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.Post,
                Href = linkWithoutId,
                Rel = RelationType.self,
                Type = ResponseTypeFormat.DefaultPost
            });

            content.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.Put,
                Href = linkWithoutId,
                Rel = RelationType.self,
                Type = ResponseTypeFormat.DefaultPut
            });


            content.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.Patch,
                Href = linkWithoutId,
                Rel = RelationType.self,
                Type = ResponseTypeFormat.DefaultPatch
            });
            content.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.Delete,
                Href = linkWithId,
                Rel = RelationType.self,
                Type = "int"
            });
            return null;

        }

        //private string GetLink(long id, IUrlHelper urlHelper, string path)
        //{
        //    lock (_locker)
        //    {
        //        var url = new {controller = path, id = id};
        //        return new StringBuilder(urlHelper.Link("DefaultApi", url)).Replace("%2F", "/").ToString();
        //    }
        //}
        //private string GetLink(IUrlHelper urlHelper, string path)
        //{
        //    lock (_locker)
        //    {
        //        var url = new { controller = path };
        //        return new StringBuilder(urlHelper.Link("DefaultApi", url)).Replace("%2F", "/").ToString();
        //    }
        //}
    }
}
