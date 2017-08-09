using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApiDemo.Models;

namespace WebApiDemo.Controllers
{
    public class HomeController : Controller
    {

       public ActionResult Index(int pageNumber=0)
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult Create()
        {
            return View();
        }
       
        
       [HttpGet]
        //Home/Edit/{id}
       public async Task<ActionResult> Edit(int id)
        {
           StudentData StudentInfo = new StudentData();

            string Baseurl = "http://localhost:49484";

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string req = "api/values/" + id;
                HttpResponseMessage Res = await client.GetAsync(req);

                if (Res.IsSuccessStatusCode)
                {
                    var StudentResponse = Res.Content.ReadAsStringAsync().Result;

                    StudentInfo = JsonConvert.DeserializeObject<StudentData>(StudentResponse);
                }
            }

            return View(StudentInfo);
        }

    }
}
