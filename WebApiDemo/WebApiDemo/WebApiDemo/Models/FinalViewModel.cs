using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiDemo.Models
{
    using System;
    using System.Collections.Generic;

    public partial class FinalViewModel
    {

        public List<StudentData> studentDetails{get; set;}
        public string previousPageUrl { get; set; }
        public string nextPageUrl { get; set; }
        
    }
}