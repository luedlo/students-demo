using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentsApi.Models;
using MySqlConnector;
using Microsoft.AspNetCore.Cors;

namespace StudentsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class StudentsController : ControllerBase
    {

        private MySqlConnection connection;

        public StudentsController(IConfiguration iConfiguration)
        { 
            string? connectionString = iConfiguration.GetConnectionString("MySqlDB");
            connection = new MySqlConnection(connectionString);
        }

        // GET: api/Students
        [HttpGet]
        [EnableCors]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            await connection.OpenAsync();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "SELECT * FROM Students";
            var reader = await cmd.ExecuteReaderAsync();
            var studentsList = new List<Student>();
            
            while (await reader.ReadAsync())
                studentsList.Add(new Student(reader));
            
            if (studentsList.ToArray().Length == 0)
                return NotFound();

            await connection.CloseAsync();
            return Ok(studentsList);
        }

        // GET: api/Students/5
        [HttpGet("{matricula}")]
        [EnableCors]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudent(string matricula)
        {
            await connection.OpenAsync();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = $"SELECT * FROM Students WHERE Matricula = ?matricula";
            cmd.Parameters.Add("?matricula", MySqlDbType.String).Value = matricula;
            var reader = await cmd.ExecuteReaderAsync();
            var studentsList = new List<Student>();
            
            while (await reader.ReadAsync())
                studentsList.Add(new Student(reader));

            if (studentsList.ToArray().Length == 0)
                return NotFound();

            await connection.CloseAsync();
            return Ok(studentsList);
        }

        // // PUT: api/Students
        [HttpPut]
        public async Task<ActionResult<IEnumerable<Student>>> PutStudent(Student student)
        {
            await connection.OpenAsync();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = $"UPDATE Students SET TipoPago = ?tipoPago, Institucion = ?institucion WHERE Matricula = ?matricula";
            cmd.Parameters.Add("?matricula", MySqlDbType.String).Value = student.Matricula;
            cmd.Parameters.Add("?tipoPago", MySqlDbType.String).Value = student.TipoPago;
            cmd.Parameters.Add("?institucion", MySqlDbType.String).Value = student.Institucion;
            var reader = cmd.ExecuteNonQueryAsync();
            
            if (reader.Result == 0)
                return NotFound();
            
            return Ok(student);
        }

        // // POST: api/Students
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
            await connection.OpenAsync();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = $"INSERT INTO Students VALUES(?matricula, ?tipoPago, ?institucion)";
            cmd.Parameters.Add("?matricula", MySqlDbType.String).Value = student.Matricula;
            cmd.Parameters.Add("?tipoPago", MySqlDbType.String).Value = student.TipoPago;
            cmd.Parameters.Add("?institucion", MySqlDbType.String).Value = student.Institucion;

            try 
            {
                var reader = cmd.ExecuteNonQueryAsync();
                if (reader.Result == 0)
                    return StatusCode(500);
            } 
            catch (AggregateException ae)
            {
                return Conflict(ae.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
            
            return Ok(student);
        }

        // DELETE: api/Students/5
        [HttpDelete("{matricula}")]
        public async Task<ActionResult<IEnumerable<Student>>> DeleteStudent(string matricula)
        {
            await connection.OpenAsync();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = $"DELETE FROM Students WHERE Matricula = ?matricula";
            cmd.Parameters.Add("?matricula", MySqlDbType.String).Value = matricula;
            
            try 
            {
                var reader = cmd.ExecuteNonQueryAsync();
                if (reader.Result == 0)
                    return NotFound();
            } 
            catch (Exception e)
            {
                return Conflict(e.Message);
            }
            
            return NoContent();
        }
    }
}
