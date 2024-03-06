using Npgsql;

using System;
using System.Collections.Generic;
using MVCpostgres.Models;

namespace MVCpostgres.Models
{
    public class DBconfiguration
    {
       
public class YourDatabaseService
    {
        private readonly string connectionString;

        public YourDatabaseService(string connectionString)
        {
            this.connectionString = connectionString;
        }
            public StudentModel GetStudentById(int studentId)
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();

                    using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM student WHERE stdid = @Id", conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", studentId);

                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new StudentModel
                                {
                                    stdid = Convert.ToInt32(reader["stdid"]),
                                   stdname = reader["stdname"].ToString(),
                                    email = reader["email"].ToString()
                                };
                            }
                        }
                    }
                }

                return null; // Return null if the student with the given ID is not found
            }

            public IEnumerable<StudentModel> GetAllStudents()
        {
            var students = new List<StudentModel>();

            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();

                using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM stdu_sch.student", conn))
                {
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            students.Add(new StudentModel
                            {
                                stdid = Convert.ToInt32(reader["stdid"]),
                                stdname = reader["stdname"].ToString(),
                                email = reader["email"].ToString()
                            });
                        }
                    }
                }
            }

            return students;
        }

        public void InsertStudent( StudentModel newStudent)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();

                using (NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO student (stdname, email) VALUES (@Name, @Email)", conn))
                {
                    cmd.Parameters.AddWithValue("@Name", newStudent.stdname);
                    cmd.Parameters.AddWithValue("@Email", newStudent.email);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateStudent(StudentModel updatedStudent)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();

                using (NpgsqlCommand cmd = new NpgsqlCommand("UPDATE student SET stdname = @Name, email = @Email WHERE stdid = @Id", conn))
                {
                    cmd.Parameters.AddWithValue("@Name", updatedStudent.stdname);
                    cmd.Parameters.AddWithValue("@Email", updatedStudent.email);
                    cmd.Parameters.AddWithValue("@Id", updatedStudent.stdid);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteStudent(int studentId)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();

                using (NpgsqlCommand cmd = new NpgsqlCommand("DELETE FROM student WHERE stdid = @Id", conn))
                {
                    cmd.Parameters.AddWithValue("@Id", studentId);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }

}
}
