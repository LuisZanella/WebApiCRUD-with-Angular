using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiWebObj.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FLastName { get; set; }
        public string SLastName { get; set; }
        public string Nick { get; set; }
        public string Password { get; set; }
        public DateTime BirthDate { get; set; }
    }
}