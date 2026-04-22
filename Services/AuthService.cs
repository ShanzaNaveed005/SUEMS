using SUEMS.Core;
using SUEMS.Data;
using SUEMS.Models;
using System;
using Microsoft.Data.SqlClient;

namespace SUEMS.Services
{
    public class AuthService
    {
        Database db = new Database();

        // ======================
        // 🔐 LOGIN
        // ======================
        public void StartLogin()
        {
            Console.WriteLine("===== DATABASE LOGIN SYSTEM =====");

            Console.Write("Username: ");
            string username = Console.ReadLine();

            Console.Write("Password: ");
            string password = Console.ReadLine();

            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();

                string query = "SELECT Role FROM Users WHERE Username = @u AND Password = @p";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@u", username);
                cmd.Parameters.AddWithValue("@p", password);

                object result = cmd.ExecuteScalar();

                if (result != null)
                {
                    string role = result.ToString();

                    UIHelper.Success($"\nLogin Successful! Role: {role}");

                    LoadDashboard(role, username);
                }
                else
                {
                    UIHelper.Error("Invalid Username or Password!");
                }
            }
        }

        // ======================
        // 🎯 ROLE ROUTER
        // ======================
        private void LoadDashboard(string role, string username)
        {
            switch (role)
            {
                case "Admin":
                    AdminDashboard(username);
                    break;

                case "Student":
                    StudentDashboard(username);
                    break;

                case "Teacher":
                    TeacherDashboard(username);
                    break;

                default:
                    Console.WriteLine("Unknown Role");
                    break;
            }
        }

        // ======================
        // 👑 ADMIN DASHBOARD
        // ======================
        private void AdminDashboard(string username)
        {
            StudentService studentService = new StudentService();
            TeacherService teacherService = new TeacherService();

            while (true)
            {
                UIHelper.Header("ADMIN DASHBOARD ");
                UIHelper.Menu("1. Add Student");
                UIHelper.Menu("2. View Students");
                UIHelper.Menu("3. Delete Student");
                UIHelper.Menu("4. Add Teacher");
                UIHelper.Menu("5. View Teachers");
                UIHelper.Menu("6. Logout");

                Console.Write("Select Option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Name: ");
                        string name = Console.ReadLine();

                        Console.Write("Age: ");
                        if (!int.TryParse(Console.ReadLine(), out int age))
                        {
                            Console.WriteLine("Invalid Age!");
                            break;
                        }

                        Console.Write("Roll: ");
                        string roll = Console.ReadLine();

                        Console.Write("Dept: ");
                        string dept = Console.ReadLine();

                        Student s = new Student(name, age, roll, dept);
                        studentService.AddStudent(s);
                        break;

                    case "2":
                        studentService.GetStudents();
                        break;

                    case "3":
                        Console.Write("Enter Student ID: ");
                        if (int.TryParse(Console.ReadLine(), out int id))
                            studentService.DeleteStudent(id);
                        else
                            Console.WriteLine("Invalid ID!");
                        break;

                    case "4":
                        Console.Write("Name: ");
                        string tname = Console.ReadLine();

                        Console.Write("Age: ");
                        if (!int.TryParse(Console.ReadLine(), out int tage))
                        {
                            Console.WriteLine("Invalid Age!");
                            break;
                        }

                        Console.Write("Employee ID: ");
                        string emp = Console.ReadLine();

                        Console.Write("Specialization: ");
                        string spec = Console.ReadLine();

                        Teacher t = new Teacher(tname, tage, emp, spec);
                        teacherService.AddTeacher(t);
                        break;

                    case "5":
                        teacherService.GetTeachers();
                        break;

                    case "6":
                        Console.WriteLine("Logging out...");
                        return;

                    default:
                        Console.WriteLine("Invalid Option");
                        break;
                }
            }
        }

        // ======================
        // 👨‍🎓 STUDENT DASHBOARD
        // ======================
        private void StudentDashboard(string username)
        {
            CourseService courseService = new CourseService();
            MarksService marksService = new MarksService();
            EnrollmentService enrollmentService = new EnrollmentService();

            while (true)
            {
                UIHelper.Header("\nSTUDENT DASHBOARD ");
                UIHelper.Menu($"Welcome {username}");
                UIHelper.Menu("1. View Courses");
                UIHelper.Menu("2. View Marks");
                UIHelper.Menu("3. View Result");
                UIHelper.Menu("4. Enroll Course");
                UIHelper.Menu("5. My Courses");
                UIHelper.Menu("6. Available Courses");
                UIHelper.Menu("7.  Drop Course");
                UIHelper.Menu("8. Logout");

                Console.Write("Select Option: ");
                string choice = Console.ReadLine();

                // 🔥 declare once outside switch (IMPORTANT FIX)
                int studentId = GetStudentId(username);

                switch (choice)
                {
                    case "1":
                        courseService.GetCourses();
                        break;

                    case "2":
                        // =========================
                        // 📌 VIEW MARKS
                        // =========================
                        if (studentId == -1)
                        {
                            Console.WriteLine("Student not found!");
                            break;
                        }

                        marksService.ViewMarks(studentId);
                        break;

                    case "3":
                        // =========================
                        // 📌 VIEW RESULT
                        // =========================
                        if (studentId == -1)
                        {
                            Console.WriteLine("Student not found!");
                            break;
                        }

                        marksService.ViewResult(studentId);
                        break;

                    case "4":
                        enrollmentService.GetAvailableCourses(studentId);

                        Console.Write("Enter Course ID: ");
                        if (int.TryParse(Console.ReadLine(), out int cid))
                            enrollmentService.EnrollCourse(studentId, cid);
                        else
                            Console.WriteLine("Invalid ID!");
                        break;

                    case "5":
                        enrollmentService.GetStudentCourses(studentId);
                        break;

                    case "6":
                        enrollmentService.GetAvailableCourses(studentId);
                        break;

                    case "7":
                        Console.Write("Enter Course ID to drop: ");
                        if (int.TryParse(Console.ReadLine(), out int dropId))
                            enrollmentService.DropCourse(studentId, dropId);
                        else
                            Console.WriteLine("Invalid ID!");
                        break;

                    case "8":
                        Console.WriteLine("Logging out...");
                        return;
                }
            }
        }
        // ======================
        // 👨‍🏫 TEACHER DASHBOARD (ID BASED 🔥)
        // ======================
        private void TeacherDashboard(string username)
        {
            CourseService courseService = new CourseService();
            AttendanceService attendanceService = new AttendanceService();
            MarksService marksService = new MarksService();

            int teacherId = GetTeacherId(username);

            if (teacherId == -1)
            {
                Console.WriteLine("Teacher not found!");
                return;
            }


            while (true)
            {
                UIHelper.Header("TEACHER DASHBOARD ");
                UIHelper.Menu($"Welcome {username}");
                UIHelper.Menu("1. View Assigned Courses");
                UIHelper.Menu("2. Mark Attendance");
                UIHelper.Menu("3. Add Marks");
                UIHelper.Menu("4. Logout");

                Console.Write("Select Option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        courseService.GetCoursesByTeacherId(teacherId);
                        break;

                    case "2":
                        // ======================
                        // 📌 ATTENDANCE
                        // ======================
                        Console.Write("Student ID: ");
                        if (!int.TryParse(Console.ReadLine(), out int sid))
                        {
                            Console.WriteLine("Invalid Student ID!");
                            break;
                        }

                        Console.Write("Course ID: ");
                        if (!int.TryParse(Console.ReadLine(), out int cid))
                        {
                            Console.WriteLine("Invalid Course ID!");
                            break;
                        }

                        attendanceService.MarkAttendance(sid, cid);
                        break;

                    case "3":
                        // ======================
                        // 📌 MARKS (YOUR CODE GOES HERE)
                        // ======================
                        Console.Write("Student ID: ");
                        if (!int.TryParse(Console.ReadLine(), out int studentId))
                        {
                            Console.WriteLine("Invalid Student ID!");
                            break;
                        }

                        Console.Write("Course ID: ");
                        if (!int.TryParse(Console.ReadLine(), out int courseId))
                        {
                            Console.WriteLine("Invalid Course ID!");
                            break;
                        }

                        Console.Write("Marks: ");
                        if (!int.TryParse(Console.ReadLine(), out int marks))
                        {
                            Console.WriteLine("Invalid Marks!");
                            break;
                        }

                        marksService.AddMarks(studentId, courseId, marks);
                        break;

                    case "4":
                        Console.WriteLine("Logging out...");
                        return;

                    default:
                        Console.WriteLine("Invalid option!");
                        break;
                }
            }
        }
        // ======================
        // 🔎 GET TEACHER ID
        // ======================
        private int GetTeacherId(string username)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();

                string query = @"
        SELECT TeacherId 
        FROM Users 
        WHERE Username = @username";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", username);

                object result = cmd.ExecuteScalar();

                return result != null ? Convert.ToInt32(result) : -1;
            }
        }

        // ======================
        // 🔎 GET STUDENT ID
        // ======================
        private int GetStudentId(string username)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();

                string query = "SELECT Id FROM Students WHERE Name = @name";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", username);

                object result = cmd.ExecuteScalar();

                return result != null ? Convert.ToInt32(result) : -1;
            }
        }


    }
}