using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestWithASPNET.Business.Interfaces;
using RestWithASPNET.Data.VO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace RestWithASPNET.Controllers.FileControllers
{
    [ApiVersion("1")]
    [ApiController]
    [Authorize("Bearer")]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class FileController : ControllerBase
    {
        private readonly IFileBusiness _fileBusiness;

        public FileController(IFileBusiness fileBusiness)
        {
            _fileBusiness = fileBusiness;
        }

        [HttpGet("downloadFile/{fileName}")]
        [ProducesResponseType((200), Type = typeof(byte[]))]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(401)]
        [Produces("application/octet-stream")]
        public async Task<IActionResult> GetFileAsync(string fileName)
        {
            try
            {
                byte[] buffer =  _fileBusiness.GetFile(fileName);
                if (buffer !=null)
                {
                    HttpContext.Response.ContentType = $"application/{Path.GetExtension(fileName).Replace(".","")}";
                    HttpContext.Response.Headers.Add("content-length", buffer.Length.ToString());
                    await HttpContext.Response.Body.WriteAsync(buffer, 0, buffer.Length);
                }
                return new ContentResult();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("uploadFile")]
        [ProducesResponseType((200), Type = typeof(FileDetailVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Produces("application/json")]
        public async Task<IActionResult> UploadOneFile([FromForm] IFormFile file)
        {
            try
            {
                FileDetailVO detail = await _fileBusiness.SaveFileToDisk(file);
                return new OkObjectResult(detail);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("uploadMultipleFiles")]
        [ProducesResponseType((200), Type = typeof(List<FileDetailVO>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Produces("application/json")]
        public async Task<IActionResult> UploadOneFile([FromForm] List<IFormFile> files)
        {
            try
            {
                List<FileDetailVO> detail = await _fileBusiness.SaveFilesToDisk(files);
                return new OkObjectResult(detail);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
