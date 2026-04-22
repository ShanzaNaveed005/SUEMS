using System;
using Microsoft.Data.SqlClient;
using SUEMS.Data;


namespace SUEMS.Services
{
    public class AttendanceService
    {
        Database db = new Database();

        public void MarkAttendance(int studentId, int courseId)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();

                string query = "INSERT INTO Attendance (StudentId, CourseId, Date, Status) " +
                               "VALUES (@s, @c, @d, @st)";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@s", studentId);
                cmd.Parameters.AddWithValue("@c", courseId);
                cmd.Parameters.AddWithValue("@d", DateTime.Now);
                cmd.Parameters.AddWithValue("@st", "Present");

                cmd.ExecuteNonQuery();

                Console.WriteLine("✔ Attendance Marked");
            }
        }
    }
}