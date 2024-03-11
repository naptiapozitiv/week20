using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

public class Student
{
    public int StudentID { get; set; }
    public string StudentName { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public byte[]? Photo { get; set; }
    public decimal Height { get; set; }
    public float Weight { get; set; }
    public Grade Grade { get; set; }
}
public class Grade
{
    public int GradeId { get; set; }
    public string GradeName { get; set; }
    public string Section { get; set; }
    public ICollection<Student> Students { get; set; }
}
public class SchoolContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=LAPTOP-2QTJECIO\\MSSQLSERVER01;Initial Catalog=Adventure;Integrated Security=True; TrustServerCertificate=True");
    }
    public DbSet<Student> Students { get; set; }
    public DbSet<Grade> Grades { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        base.OnModelCreating(modelBuilder);
    }
}
public class Program
{
    static void Main(string[] args)
    {

        using (var ctx = new SchoolContext())
        {
            var stud = new Student()
            {
                StudentName = "Bill",Height = 2,Weight = 187 , Grade = new Grade() { GradeName = "Second", Section = "Low" }
            };
            ctx.Students.Add(stud);
            ctx.SaveChanges();
            var students = ctx.Students.Include(s => s.Grade).ToList();
            foreach (var student in students)
            {
                Console.WriteLine($"Student name: {student.StudentName}, Grade = {student.Grade.GradeName}, Grade section = {student.Grade.Section}");
            }
        }
    }
}