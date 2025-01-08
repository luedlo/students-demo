using System.ComponentModel.DataAnnotations;
using MySqlConnector;

namespace StudentsApi.Models;

public class Student
{
    public string? Matricula { get; set; }
    public string? TipoPago { get; set; }
    public string? Institucion{ get; set; }
    public Student () {}
    public Student (MySqlDataReader student) {
        this.Matricula = student.GetString(0);
        this.TipoPago = student.GetString(1);
        this.Institucion = student.GetString(2);
    }
}