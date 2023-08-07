using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScortelApi.Models
{
    public class CreateUserClass
    {
        public string uname { get; set; }
        public string upass { get; set; }
        public string email { get; set; }
        public string role { get; set; }
        public string res { get; set; }
        //public List<ErrorClass> errors { get; set; }
        public ErrorClass[] errors { get; set; }

        public CreateUserClass()
        {
            //errors = new List<ErrorClass>();
            errors = new ErrorClass[] { null};
        }
    }
}
