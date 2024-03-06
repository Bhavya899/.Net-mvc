using Microsoft.AspNetCore.Mvc;
using MVCpostgres.Models;
using System.Diagnostics;
using Npgsql;
using MVCpostgres.Models;
using System.Security.Cryptography.X509Certificates;

namespace MVCpostgres.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using static MVCpostgres.Models.DBconfiguration;

    public class StudentController : Controller
    {
        private readonly YourDatabaseService _databaseService;

        public StudentController(YourDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        // GET: /Student/Index
        public IActionResult Index()
        {
            var students = _databaseService.GetAllStudents();
            return View(students);
        }

        // GET: /Student/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Student/Create
        [HttpPost]
        public IActionResult Create([FromBody] StudentModel newStudent)

        {
            if (ModelState.IsValid)
            {
                try
                {
                    _databaseService.InsertStudent(newStudent);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while inserting data. Please try again.");
                }
            }

            return View(newStudent);
        }

        // GET: /Student/Edit/{id}
        public IActionResult Edit(int id)
        {
            var student = _databaseService.GetStudentById(id);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: /Student/Edit/{id}
        [HttpPost]
        public IActionResult Edit(StudentModel updatedStudent)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _databaseService.UpdateStudent(updatedStudent);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while updating data. Please try again.");
                }
            }

            return View(updatedStudent);
        }

        // GET: /Student/Delete/{id}
        public IActionResult Delete(int id)
        {
            var student = _databaseService.GetStudentById(id);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: /Student/Delete/{id}
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                _databaseService.DeleteStudent(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Handle the exception as needed
                return RedirectToAction("Delete", new { id = id, error = true });
            }
        }
    }

    /* public class studentController : Controller
     {

         public IActionResult Index()
         {
             List<StudentModel> displaystudents = new List<StudentModel>();
             NpgsqlConnection conn = new NpgsqlConnection("Server=localhost;Port=5432;Database=postgres;user Id=postgres;Password=amnexdev;");
             conn.Open();
             NpgsqlCommand cmd = new NpgsqlCommand();
             cmd.Connection = conn;
             cmd.CommandType = System.Data.CommandType.Text;

             //For fetching data from database
             cmd.CommandText = "select * from student";


             NpgsqlDataReader sdr = cmd.ExecuteReader();
             while (sdr.Read())
             {
                 var stlist = new StudentModel();
                 stlist.stdid = Convert.ToInt32(sdr["stdid"]);
                 stlist.stdname = sdr["stdname"].ToString();
                 stlist.email = sdr["email"].ToString();
                 displaystudents.Add(stlist);



             }
             return View(displaystudents);

         }

         [HttpPost]
         public void Insert(StudentModel newStudent)

         {
             ViewBag.Message = "";
                       if(ModelState.IsValid)
             {
                 ViewBag.Message = "data inserted succesfully";
             }
             using (NpgsqlConnection conn = new NpgsqlConnection("Server=localhost;Port=5432;Database=postgres;user Id=postgres;Password=amnexdev;")) 

                 {
                     conn.Open();

                     using (NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO student (stdname, email) VALUES (@Name, @Email)", conn))
                     {
                         // Explicitly handle null values for parameters
                         cmd.Parameters.Add(new NpgsqlParameter("@Name", NpgsqlTypes.NpgsqlDbType.Varchar) { Value = newStudent.stdname });
                         cmd.Parameters.Add(new NpgsqlParameter("@Email", NpgsqlTypes.NpgsqlDbType.Varchar) { Value = newStudent.email  });

                         cmd.ExecuteNonQuery();
                     }
                 }
             }
         }
 */

}




