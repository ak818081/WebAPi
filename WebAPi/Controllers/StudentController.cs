using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace WebAPi.Controllers
{
    public class StudentController : ApiController
    {
        // GET: Student
        public string Get()
        {
            return "Welcome To Student Web API";
        }
        public List<string> Get(int Id)
        {
            return new List<string> {
                "Student 1",
                "Student 2"
            };
        }
    }
}