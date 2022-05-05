using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebAPi.Models;

namespace WebAPi.Controllers
{
    public class StudentAPIController : ApiController
    {
        HttpResponseMessage response;
        static List<Student> studentList = new List<Student>();
        // GET: api/StudentAPI
        public HttpResponseMessage GetStudent()
        {
            
            using (StudentAPIEntities dc = new StudentAPIEntities())
            {
                studentList = dc.Students.OrderBy(a => a.StudentName).ToList();
               
                response = Request.CreateResponse(HttpStatusCode.OK, studentList);
                return response;
            }
        }

        // GET: api/StudentAPI/5
        [Microsoft.AspNetCore.Mvc.HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            using (StudentAPIEntities dc = new StudentAPIEntities())
            {
                var student = await dc.Students.FindAsync(id);
            
                if ( student== null)
                {
                    return student;
                }

            return student;
            }
        }


        [System.Web.Http.HttpPost]
        // POST: api/StudentAPI/PostNewStudent
        public IHttpActionResult PostNewStudent(Student stModel)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");

            using (var ctx = new StudentAPIEntities())
            {
                ctx.Students.Add(new Student()
                {
                    Id = stModel.Id,
                    StudentName = stModel.StudentName,
                    Gender = stModel.Gender,
                    IsActive= true,
                    CreatedDate = DateTime.Now,
                    CreatedBy=1,
                    ModifiedDate= DateTime.Now,
                    ModifiedBy=null
                });

                ctx.SaveChanges();
            }

            return Ok("Record inserted succesfull.");
        }


        [System.Web.Http.HttpPut]
        // PUT: api/StudentAPI/UpdateStudent/5
        public IHttpActionResult UpdateStudent(Student stModel)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            using (var ctx = new StudentAPIEntities())
            {
                var existingStudent = ctx.Students.Where(s => s.Id == stModel.Id)
                                                        .FirstOrDefault<Student>();

                if (existingStudent != null)
                {
                    existingStudent.StudentName = stModel.StudentName;
                    existingStudent.Gender = stModel.Gender;
                    existingStudent.IsActive = true;
                    existingStudent.CreatedDate = DateTime.Now;
                    existingStudent.CreatedBy = 1;
                    existingStudent.ModifiedDate = DateTime.Now;
                    existingStudent.ModifiedBy = 2;

                    ctx.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }

            return Ok("Student Record Updated Succesfull.");
        }


        //DELETE: api/StudentAPI/DeleteStudent/5
        [Microsoft.AspNetCore.Mvc.HttpDelete("{id}")]
        public async Task<ActionResult<Student>> DeleteStudent(int id)
        {
            using (var ctx = new StudentAPIEntities())
            {
                var student = await ctx.Students.FindAsync(id);
                if (student == null)
                {
                    return student;
                }

                ctx.Students.Remove(student);
                await ctx.SaveChangesAsync();
            }
            return studentList.FirstOrDefault();
        }
    }
}
