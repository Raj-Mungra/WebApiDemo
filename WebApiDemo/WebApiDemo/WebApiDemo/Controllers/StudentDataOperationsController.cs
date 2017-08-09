using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApiDemo.Models;

namespace WebApiDemo.Controllers
{
    public class StudentDataOperationsController : ApiController
    {
        private StudentEntities db = new StudentEntities();

        // GET api/StudentDataOperations
        public IQueryable<StudentData> GetStudentDatas()
        {
            return db.StudentDatas;
        }

        // GET api/StudentDataOperations/5
        [ResponseType(typeof(StudentData))]
        public IHttpActionResult GetStudentData(int id)
        {
            StudentData studentdata = db.StudentDatas.Find(id);
            if (studentdata == null)
            {
                return NotFound();
            }

            return Ok(studentdata);
        }

        // PUT api/StudentDataOperations/5
        public IHttpActionResult PutStudentData(int id, StudentData studentdata)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != studentdata.StudentID)
            {
                return BadRequest();
            }

            db.Entry(studentdata).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentDataExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/StudentDataOperations
        [ResponseType(typeof(StudentData))]
        public IHttpActionResult PostStudentData(StudentData studentdata)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.StudentDatas.Add(studentdata);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = studentdata.StudentID }, studentdata);
        }

        // DELETE api/StudentDataOperations/5
        [ResponseType(typeof(StudentData))]
        public IHttpActionResult DeleteStudentData(int id)
        {
            StudentData studentdata = db.StudentDatas.Find(id);
            if (studentdata == null)
            {
                return NotFound();
            }

            db.StudentDatas.Remove(studentdata);
            db.SaveChanges();

            return Ok(studentdata);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StudentDataExists(int id)
        {
            return db.StudentDatas.Count(e => e.StudentID == id) > 0;
        }
    }
}