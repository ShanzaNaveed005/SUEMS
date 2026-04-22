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
    public class StudentService
    {
        Database db = new Database();

        // =========================
        // ➕ ADD STUDENT
        // =========================
        public void AddStudent(Student s)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();

                string query = "INSERT INTO Students (Name, Age, RollNumber, Department) " +
                               "VALUES (@Name, @Age, @RollNumber, @Department)";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@Name", s.Name);
                cmd.Parameters.AddWithValue("@Age", s.Age);
                cmd.Parameters.AddWithValue("@RollNumber", s.RollNumber);
                cmd.Parameters.AddWithValue("@Department", s.Department);

                cmd.ExecuteNonQuery();
                Console.WriteLine("✔ Student Added Successfully!");
            }
        }

        // =========================
        // 📄 VIEW STUDENTS
        // =========================
        public void GetStudents()
        {
            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();

                string query = "SELECT * FROM Students";
                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataReader reader = cmd.ExecuteReader();

                Console.WriteLine("\n--- STUDENTS LIST ---");

                while (reader.Read())
                {
                    Console.WriteLine(
                        $"ID: {reader["Id"]} | Name: {reader["Name"]} | Roll: {reader["RollNumber"]} | Dept: {reader["Department"]}"
                    );
                }
            }
        }

        // =========================
        // ❌ DELETE STUDENT
        // =========================
        public void DeleteStudent(int id)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();

                string query = "DELETE FROM Students WHERE Id = @Id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                    Console.WriteLine("✔ Student Deleted");
                else
                    Console.WriteLine("❌ Student Not Found");
            }
        }
    }
}