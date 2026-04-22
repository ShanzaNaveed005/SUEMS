using System;
using Microsoft.Data.SqlClient;
using SUEMS.Data;
using SUEMS.Models;

namespace SUEMS.Services
{
    public class CourseService
    {
        Database db = new Database();

        // =====================
        // ➕ ADD COURSE
        // =====================
        public void AddCourse(Course c)
        {
            try
            {
                using (SqlConnection conn = db.GetConnection())
                {
                    conn.Open();

                    string query = "INSERT INTO Courses (CourseName, CreditHours, TeacherName) " +
                                   "VALUES (@Name, @Credits, @Teacher)";

                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@Name", c.CourseName);
                    cmd.Parameters.AddWithValue("@Credits", c.CreditHours);
                    cmd.Parameters.AddWithValue("@Teacher",
                        c.AssignedTeacher != null ? c.AssignedTeacher.Name : "Not Assigned");

                    cmd.ExecuteNonQuery();

                    Console.WriteLine("✔ Course Added Successfully!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Error: " + ex.Message);
            }
        }

        // =====================
        // 📄 VIEW ALL COURSES
        // =====================
        public void GetCourses()
        {
            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();

                // 🔥 UPDATED JOIN QUERY
                string query = @"
        SELECT c.CourseName, c.CreditHours, t.Name AS TeacherName
        FROM Courses c
        LEFT JOIN Teachers t ON c.TeacherId = t.Id";

                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataReader reader = cmd.ExecuteReader();

                Console.WriteLine("\n--- ALL COURSES ---");

                while (reader.Read())
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine ($"Course: {reader["CourseName"]} | Credits: {reader["CreditHours"]} | Teacher: {reader["TeacherName"]}"
                    );
                    Console.ResetColor();
                }
            }
        }
        // =====================
        // 👨‍🏫 VIEW COURSES BY TEACHER (ADVANCED)
        // =====================
        public void GetCoursesByTeacherId(int teacherId)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();

                string query = "SELECT * FROM Courses WHERE TeacherId = @id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", teacherId);

                SqlDataReader reader = cmd.ExecuteReader();

                Console.WriteLine("\n--- ASSIGNED COURSES ---");

                while (reader.Read())
                {
                    Console.WriteLine(
                        $"ID: {reader["Id"]} | Course: {reader["CourseName"]} | Credits: {reader["CreditHours"]}"
                    );
                }
            }
        }
    }

}