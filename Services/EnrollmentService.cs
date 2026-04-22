using System;
using Microsoft.Data.SqlClient;
using SUEMS.Data;

namespace SUEMS.Services
{
    public class EnrollmentService
    {
        Database db = new Database();

        // ======================
        // 📌 ENROLL COURSE
        // ======================
        public void EnrollCourse(int studentId, int courseId)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();

                string query = "INSERT INTO Enrollments (StudentId, CourseId) VALUES (@sid, @cid)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@sid", studentId);
                cmd.Parameters.AddWithValue("@cid", courseId);

                try
                {
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("✔ Enrolled Successfully!");
                }
                catch (SqlException ex)
                {
                    // 🔥 Duplicate enrollment handle
                    if (ex.Number == 2627)
                        Console.WriteLine("❌ You are already enrolled in this course!");
                    else
                        Console.WriteLine("❌ Database error: " + ex.Message);
                }
            }
        }

        // ======================
        // 📌 VIEW MY COURSES
        // ======================
        public void GetStudentCourses(int studentId)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();

                string query = @"
                SELECT c.CourseName, c.CreditHours, t.Name AS TeacherName
                FROM Enrollments e
                INNER JOIN Courses c ON e.CourseId = c.Id
                LEFT JOIN Teachers t ON c.TeacherId = t.Id
                WHERE e.StudentId = @sid";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@sid", studentId);

                SqlDataReader reader = cmd.ExecuteReader();

                Console.WriteLine("\n--- MY COURSES ---");

                if (!reader.HasRows)
                {
                    Console.WriteLine("No enrolled courses.");
                    return;
                }

                while (reader.Read())
                {
                    Console.WriteLine(
                        $"Course: {reader["CourseName"]} | Credits: {reader["CreditHours"]} | Teacher: {reader["TeacherName"]}"
                    );
                }
            }
        }

        // ======================
        // 📌 AVAILABLE COURSES (NOT ENROLLED 🔥)
        // ======================
        public void GetAvailableCourses(int studentId)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();

                string query = @"
                SELECT c.Id, c.CourseName, c.CreditHours, t.Name AS TeacherName
                FROM Courses c
                LEFT JOIN Teachers t ON c.TeacherId = t.Id
                WHERE c.Id NOT IN (
                    SELECT CourseId FROM Enrollments WHERE StudentId = @sid
                )";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@sid", studentId);

                SqlDataReader reader = cmd.ExecuteReader();

                Console.WriteLine("\n--- AVAILABLE COURSES ---");

                if (!reader.HasRows)
                {
                    Console.WriteLine("All courses already enrolled!");
                    return;
                }

                while (reader.Read())
                {
                    Console.WriteLine(
                        $"ID: {reader["Id"]} | {reader["CourseName"]} | Credits: {reader["CreditHours"]} | Teacher: {reader["TeacherName"]}"
                    );
                }
            }
        }

        // ======================
        // 📌 DROP COURSE (BONUS 🔥)
        // ======================
        public void DropCourse(int studentId, int courseId)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();

                string query = "DELETE FROM Enrollments WHERE StudentId = @sid AND CourseId = @cid";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@sid", studentId);
                cmd.Parameters.AddWithValue("@cid", courseId);

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                    Console.WriteLine("✔ Course dropped successfully!");
                else
                    Console.WriteLine("❌ You are not enrolled in this course!");
            }
        }
    }
}