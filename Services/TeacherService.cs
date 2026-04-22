using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Data.SqlClient;
using SUEMS.Data;
using SUEMS.Models;


namespace SUEMS.Services
{
    public class TeacherService
    {
        Database db = new Database();

        public void AddTeacher(Teacher t)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();

                string query = "INSERT INTO Teachers (Name, Age, EmployeeId, Specialization) " +
                               "VALUES (@Name, @Age, @EmployeeId, @Specialization)";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@Name", t.Name);
                cmd.Parameters.AddWithValue("@Age", t.Age);
                cmd.Parameters.AddWithValue("@EmployeeId", t.EmployeeId);
                cmd.Parameters.AddWithValue("@Specialization", t.Specialization);

                cmd.ExecuteNonQuery();
                Console.WriteLine("✔ Teacher Added!");
            }
        }

        public void GetTeachers()
        {
            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();

                string query = "SELECT * FROM Teachers";
                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataReader reader = cmd.ExecuteReader();

                Console.WriteLine("\n--- TEACHERS LIST ---");

                while (reader.Read())
                {
                    Console.WriteLine(
                        $"ID: {reader["Id"]} | Name: {reader["Name"]} | Spec: {reader["Specialization"]}"
                    );
                }
            }
        }
    }
}