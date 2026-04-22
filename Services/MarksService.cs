using System;
using Microsoft.Data.SqlClient;
using SUEMS.Data;

namespace SUEMS.Services
{
    public class MarksService
    {
        Database db = new Database();

        // =========================
        // ➕ ADD MARKS (TEACHER)
        // =========================
        public void AddMarks(int studentId, int courseId, int marks)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();

                string query = "INSERT INTO Marks (StudentId, CourseId, Marks) VALUES (@s, @c, @m)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@s", studentId);
                cmd.Parameters.AddWithValue("@c", courseId);
                cmd.Parameters.AddWithValue("@m", marks);

                cmd.ExecuteNonQuery();

                Console.WriteLine("✔ Marks Added Successfully");
            }
        }

        // =========================
        // 📄 VIEW STUDENT MARKS
        // =========================
        public void ViewMarks(int studentId)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();

                string query = @"
        SELECT c.CourseName, AVG(m.Marks) AS AvgMarks
        FROM Marks m
        INNER JOIN Courses c ON m.CourseId = c.Id
        WHERE m.StudentId = @sid
        GROUP BY c.CourseName";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@sid", studentId);

                SqlDataReader reader = cmd.ExecuteReader();

                Console.WriteLine("\n--- YOUR MARKS ---");

                if (!reader.HasRows)
                {
                    Console.WriteLine("No marks available yet.");
                    return;
                }

                while (reader.Read())
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Course: {reader["CourseName"]} | Avg Marks: {reader["AvgMarks"]}"
                    );
                    Console.ResetColor();
                }
            }
        }
        // =========================
        // 🧮 RESULT CALCULATION
        // =========================
        public void ViewResult(int studentId)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();

                string query = "SELECT AVG(Marks) FROM Marks WHERE StudentId = @id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", studentId);

                object result = cmd.ExecuteScalar();

                if (result != DBNull.Value)
                {
                    double avg = Convert.ToDouble(result);

                    Console.WriteLine("\n--- RESULT ---");
                    Console.WriteLine($"Average Marks: {avg}");

                    if (avg >= 80)
                        Console.WriteLine("Grade: A+");
                    else if (avg >= 70)
                        Console.WriteLine("Grade: A");
                    else if (avg >= 60)
                        Console.WriteLine("Grade: B");
                    else if (avg >= 50)
                        Console.WriteLine("Grade: C");
                    else
                        Console.WriteLine("Grade: Fail");
                }
                else
                {
                    Console.WriteLine("No marks found to calculate result.");
                }
            }
        }
    }
}