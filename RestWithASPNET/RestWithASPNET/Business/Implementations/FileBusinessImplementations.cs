using Microsoft.AspNetCore.Http;
using RestWithASPNET.Business.Interfaces;
using RestWithASPNET.Data.VO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using RestWithASPNET.Ultis;

namespace RestWithASPNET.Business.Implementations
{

    public class FileBusinessImplementations : IFileBusiness
    {
        private readonly string _basePath;
        private readonly IHttpContextAccessor _context;

        public FileBusinessImplementations(IHttpContextAccessor context)
        {
            _context = context;
            _basePath = Directory.GetCurrentDirectory() + "\\UploadDir\\";
        }

        public byte[] GetFile(string filename)
        {
            var filepath = _basePath + filename;
            return File.ReadAllBytes(filepath);
        }
        public async Task<FileDetailVO> SaveFileToDisk(IFormFile file)
        {
            FileDetailVO fileDetail = new FileDetailVO();

            var url = _context.HttpContext.Request.GetAbsoluteUri();
            url = url.RemoveLastSegement();
            
            if (CheckFile(file))
            {
                var fileType = Path.GetExtension(file.FileName);
                var docName = Path.GetFileName(file.FileName.Replace(" ", "-"));
                var destination = Path.Combine(_basePath, "", docName);
                fileDetail.DocumentName = docName;
                fileDetail.DocType = fileType;
                fileDetail.DocUrl = Path.Combine(url.AbsoluteUri + "/" + fileDetail.DocumentName);

                using (var stream = new FileStream(destination, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }

            return fileDetail;
        }
        public async Task<List<FileDetailVO>> SaveFilesToDisk(IList<IFormFile> files)
        {
            List<FileDetailVO> listResponse = new List<FileDetailVO>();
            foreach (var file in files)
            {
                listResponse.Add(await this.SaveFileToDisk(file));
            }

            return listResponse;
        }
        private bool CheckFile(IFormFile file)
        {
            var response = false;
            var fileType = Path.GetExtension(file.FileName);
            if (fileType.ToLower() == ".pdf" || fileType.ToLower() == ".jpg" ||
               fileType.ToLower() == ".png" || fileType.ToLower() == ".Jpeg")
            {
                if (file != null && file.Length > 0)
                {
                    return true;
                }
            }
            return response;

        }




    }
}
