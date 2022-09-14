using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using RestWithASPNET.Models;
using RestWithASPNET.Business.Implementations;
using RestWithASPNET.Business.Interfaces;
using RestWithASPNET.Models.Entites;
using RestWithASPNET.Data.VO;
using RestWithASPNET.HyperMedia.Filters;
using Microsoft.AspNetCore.Authorization;
using RestWithASPNET.HyperMedia.Utils;

namespace RestWithASPNET.Controllers.PersonControllers
{
    [ApiVersion("1")]
    [ApiController]
    [Authorize("Bearer")]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class PersonController : ControllerBase
    {      
       
        private readonly IPersonBusiness _personBusiness;

        public PersonController(ILogger<PersonController> logger, IPersonBusiness personBusiness)
        {
            _personBusiness = personBusiness;
            
        }

        [HttpGet("{id}")]
        [ProducesResponseType((200), Type = typeof(PersonVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get(long id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest("input invalid");
                } 
                var result = _personBusiness.FindById(id);
                if (result == null) return NotFound();
                return Ok(result);                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }  

        [HttpGet("{sortDirection}/{pageSize}/{page}")]
        [ProducesResponseType((200), Type = typeof(List<PagedSarchVO<PersonVO>>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get([FromQuery] string name,
            string sortDirection,
            int pageSize,
            int page
            )
        {
            try
            {
                return Ok(_personBusiness.FindWithPagedSarch(name,sortDirection,pageSize, page));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType((200), Type = typeof(PersonVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Post([FromBody] PersonVO person)
        {
            try
            {
                if(person == null) return BadRequest("input invalid");                
                return Ok(_personBusiness.Create(person));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [ProducesResponseType((200), Type = typeof(PersonVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Put([FromBody] PersonVO person)
        {
            try
            {
                if (person == null) return BadRequest("input invalid");
                var result = _personBusiness.Update(person);
                if (result.Id == 0 && result.FirstName == null && result.Links.Count() == 0) return NotFound("Person NotFound");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("{id}")]
        [ProducesResponseType((200), Type = typeof(PersonVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Patch(long id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest("input invalid");
                }
                var result = _personBusiness.Disable(id);
                if (result == null) return NotFound();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Delete(long id)
        {
            try
            {
                if(id == 0) BadRequest("input invalid");
                _personBusiness.Delete(id);
                return NoContent();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("findPersonByName")]
        [ProducesResponseType((200), Type = typeof(PersonVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get([FromQuery]string firstName, [FromQuery] string lastName)
        {
            try
            {

                var result = _personBusiness.FindByName(firstName,lastName);
                if (!result.Any()) return NotFound();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}
