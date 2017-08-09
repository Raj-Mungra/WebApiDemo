using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web;
using WebApiDemo.Models;
using System.Web.Http.Routing;
using System.Web.Http.Description;
using System.Security.Policy;
namespace WebApiDemo.Controllers
{
    public class ValuesController : ApiController
    {
        int pageSize = 5;
        //List<StudentData> PreviousPageUrl = new List<StudentData>();
        StudentEntities studentEntity = new StudentEntities();
        FinalViewModel studentList = new FinalViewModel();
        // GET api/values
       
        
        public IHttpActionResult Get(int pageNumber = 0)
        {
           
            var Records = studentEntity.StudentDatas.OrderBy(s => s.StudentID);
            var maxRecords = Records.Count(); 

            var totalPages = (int)Math.Ceiling((double)maxRecords / pageSize);
            var urlHelper = new UrlHelper(Request);
            int prevPageLink, nextPageLink;
           // var prevPageLink = pageNumber > 0 ? urlHelper.Link("DefaultMVC", new { pageNumber = pageNumber - 1 }) : "";
            //var nextPageLink = pageNumber < totalPages - 1 ? urlHelper.Link("DefaultMVC", new { pageNumber = pageNumber + 1 }) : "";

            var link = Request.RequestUri.Query;
            var parameter = HttpUtility.ParseQueryString(link);

            string pgnumber = parameter["pageNumber"];
           //string temp = pgnumber;
                if(pgnumber=="null")
                {
                    pgnumber = "-1";
                }

                int pgnum = Convert.ToInt32(pgnumber);
                if (pgnum <= 0)
                {
                    prevPageLink = -1;
                }
                else
                {
                    prevPageLink = pgnum - 1;
                }
                if (pgnum > totalPages-2)
                {
                    nextPageLink = -1;
                }
                else
                {
                    if(pgnum==-1)
                    {
                        nextPageLink = pgnum + 2;
                    }
                    else
                        nextPageLink = pgnum + 1;
                }

            
            var records = (from s in studentEntity.StudentDatas
                           orderby s.StudentID ascending
                           select s).Skip(5 * pageNumber).Take(pageSize).ToList();


            studentList.studentDetails = records;
            studentList.nextPageUrl = nextPageLink.ToString();
            studentList.previousPageUrl = prevPageLink.ToString();
          
            return Ok(studentList);

            
        }

        // GET api/values/5
        //[HttpGet]
        public IHttpActionResult GetData(int id)
        {
            var record = studentEntity.StudentDatas.FirstOrDefault(s => s.StudentID == id);
            if (record == null)
                return NotFound();
            return Ok(record);
        }
        
        // POST api/values
        public IHttpActionResult Post(List<string> createStudent)
        {
            if (createStudent == null)
                return BadRequest();

            StudentData studentData = new StudentData();

            studentData.Name = createStudent[0];
            studentData.Class = createStudent[1];
            studentEntity.StudentDatas.Add(studentData);
            studentEntity.SaveChanges();
            return Ok(studentData);
        }

        // PUT api/values/5
        public IHttpActionResult Put(List<string> studentData)
        {
            if (studentData == null)
                return NotFound();
            else
            {
                var id = Convert.ToInt16(studentData[0]);

                var record = studentEntity.StudentDatas.FirstOrDefault(s => s.StudentID == id);
                if (record == null)
                    return NotFound();
                else
                {
                    record.Name = studentData[1];
                    record.Class = studentData[2];
                    studentEntity.SaveChanges();
                    return Ok();
                }
            }
        }

        // DELETE api/values/5
        public IHttpActionResult Delete(int id)
        {
            var record = studentEntity.StudentDatas.FirstOrDefault(s => s.StudentID == id);
            if (record == null)
                return NotFound();
            else
            {
                studentEntity.StudentDatas.Remove(record);
                studentEntity.SaveChanges();
                return Ok();
            }

         
        }
        
        [HttpPatch]
        public IHttpActionResult Patch(int id,[FromBody]StudentData classOfStudent)
        {
            var record = studentEntity.StudentDatas.FirstOrDefault(s => s.StudentID == id);
            if (record == null)
                return NotFound();
            else
            {
                record.Class = classOfStudent.Class;
                studentEntity.SaveChanges();
                return Ok(record.StudentID);
            }


            
        }
    }
}
