using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Collections.Generic;




namespace MVCpostgres.Models
{
    public class StudentModel
    {
      
        public int stdid { get; set; }
        public string stdname {  get; set; }
        public string email {  get; set; }

    }
}
