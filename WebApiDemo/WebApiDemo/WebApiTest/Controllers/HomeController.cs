using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApiTest.Models;



namespace WebApiTest.Controllers
{


    public class HomeController : Controller
    {

        string Baseurl = "http://localhost:49484";

        public async Task<ActionResult> Index()
        {

            List<StudentData> StudentInfo = new List<StudentData>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync("api/values");  

                if (Res.IsSuccessStatusCode)
                {
                    var StudentResponse = Res.Content.ReadAsStringAsync().Result;

                    StudentInfo = JsonConvert.DeserializeObject<List<StudentData>>(StudentResponse);
                }
            }

            return View(StudentInfo);
        }


        public async Task<ActionResult> Details(int id)
        {

            StudentData StudentInfo = new StudentData();

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


        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Create(StudentData studentData)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
             
                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.PostAsync("api/values", content);

               // var result = postTask.Result;
                if (Res.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

           }


            return View(studentData);
        }
       















        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}