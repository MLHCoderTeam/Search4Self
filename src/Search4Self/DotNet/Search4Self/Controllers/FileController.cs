using Search4Self.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Search4Self.Controllers
{
    [RoutePrefix("api/file")]
    public class FileController : AuthInjectedController
    {
        [Route("Upload")]
        [HttpPost]
        public async System.Threading.Tasks.Task<IHttpActionResult> UploadAsync()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                Request.CreateResponse(HttpStatusCode.UnsupportedMediaType);
            }

            Authorize();

            string root = HttpContext.Current.Server.MapPath("~/temp");
            var provider = new MultipartFormDataStreamProvider(root);
            var result = await Request.Content.ReadAsMultipartAsync(provider);

            // On upload, files are given a generic name like "BodyPart_26d6abe1-3ae1-416a-9429-b35f15e6e5d5"
            // so this is how you can get the original file name
            //var originalFileName = GetDeserializedFileName(result.FileData.First());
            // var uploadedFileInfo = new FileInfo(result.FileData.First().LocalFileName);
            string path = result.FileData.First().LocalFileName;

            using (var fileStream = File.OpenRead(path))
            {
                await ArchiveService.UnzipAsync(fileStream, User.Id).ConfigureAwait(false);
            }

            // Delete the file at the end
            File.Delete(path);

            return Ok();
        }



    }
}