using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using StudentManagementAPIs.Models;


namespace StudentManagementAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public StudentsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(
                _configuration.GetConnectionString("DefaultConnection"));
        }

        // GET: api/students
        [HttpGet]
        public IActionResult GetStudents()
        {
            List<Student> students = new List<Student>();

            using SqlConnection con = GetConnection();

            string query = "SELECT * FROM Students";

            using SqlCommand cmd = new SqlCommand(query, con);

            con.Open();

            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                students.Add(new Student
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString()!,
                    Email = reader["Email"].ToString()!,
                    Course = reader["Course"].ToString()!
                });
            }

            return Ok(students);
        }

        // GET: api/students/1
        [HttpGet("{id}")]
        public IActionResult GetStudent(int id)
        {
            using SqlConnection con = GetConnection();

            string query = "SELECT * FROM Students WHERE Id = @Id";

            using SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@Id", id);

            con.Open();

            using SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                Student student = new Student
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString()!,
                    Email = reader["Email"].ToString()!,
                    Course = reader["Course"].ToString()!
                };

                return Ok(student);
            }

            return NotFound();
        }

        // POST: api/students
        [HttpPost]
        public IActionResult AddStudent(Student student)
        {
            using SqlConnection con = GetConnection();

            string query = @"INSERT INTO Students (Name, Email, Course)
                             VALUES (@Name, @Email, @Course);
                             SELECT CAST(SCOPE_IDENTITY() AS INT);";

            using SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@Name", student.Name);
            cmd.Parameters.AddWithValue("@Email", student.Email);
            cmd.Parameters.AddWithValue("@Course", student.Course);

            con.Open();

            student.Id = Convert.ToInt32(cmd.ExecuteScalar());

            return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, student);
        }

        // PUT: api/students/1
        [HttpPut("{id}")]
        public IActionResult UpdateStudent(int id, Student student)
        {
            using SqlConnection con = GetConnection();

            string query = @"UPDATE Students
                             SET Name = @Name,
                                 Email = @Email,
                                 Course = @Course
                             WHERE Id = @Id";

            using SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@Name", student.Name);
            cmd.Parameters.AddWithValue("@Email", student.Email);
            cmd.Parameters.AddWithValue("@Course", student.Course);

            con.Open();

            int rows = cmd.ExecuteNonQuery();

            if (rows == 0)
                return NotFound();

            return Ok("Student updated successfully.");
        }

        // DELETE: api/students/1
        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            using SqlConnection con = GetConnection();

            string query = "DELETE FROM Students WHERE Id = @Id";

            using SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@Id", id);

            con.Open();

            int rows = cmd.ExecuteNonQuery();

            if (rows == 0)
                return NotFound();

            return Ok("Student deleted successfully.");
        }
    }
}
