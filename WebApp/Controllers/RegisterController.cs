using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TechSharedLibraries;
using WebApp.Models;
using WebApp.TechSupportServiceReference;
using System.IO;

namespace WebApp.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        public ActionResult Index()
        {
            return View("RegisterIndex");
        }

        [HttpPost]
        public async Task<ActionResult> Registration(string username, string fullname, string password, string email, HttpPostedFileBase uploadimage)
        {
            using (TechSupportService1Client client = new TechSupportService1Client())
            {
                string path = Path.GetFullPath(uploadimage.FileName);
                client.Open();
                string uploadedFile = "";
                if (uploadimage != null)
                {
                    uploadedFile = await AzureBlobUploader.UploadImageAsync(path);
                }
                client.RegisterNewUser(fullname, email, username, password, uploadedFile);
            }

            return View(new RegisterModel(username, fullname, password, email, uploadimage));
        }

        private async Task<string> Upload(string file)
        {
            return await AzureBlobUploader.UploadImageAsync(file);
        }
        
    }
}