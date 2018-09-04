using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiWebObj.Models
{
    public class Diary
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public User Person { get; set; }
    }
}