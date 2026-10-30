using LMS.BL;
using LMS.DAL;
using LMS.ML;
using System;

namespace LMS.UI
{
    public class StudentPortalUI
    {
        private StudentBL studentBL = new StudentBL();
        private AnnouncementBL announcementBL = new AnnouncementBL();
        private FeedbackBL feedbackBL = new FeedbackBL();

        public void Login()
        {
            Console.Clear();
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("            LMS STUDENT PORTAL              ");
            Console.WriteLine("--------------------------------------------");

            Console.Write("Enter your ID: ");
            int id = int.Parse(Console.ReadLine());

            Student s = studentBL.FindById(id);
            if (s == null)
            {
                Console.WriteLine("Wrong ID!");
                Console.ReadKey();
                return;
            }

            Menu(s);
        }

        private void Menu(Student s)
        {
            int choice = -1;
            while (choice != 0)
            {
                Console.Clear();
                Console.WriteLine("-------------------------------------------------------------");
                Console.WriteLine("1. View Profile");
                Console.WriteLine("2. View Registered Courses");
                Console.WriteLine("3. View My Grades");
                Console.WriteLine("4. Check Attendance");
                Console.WriteLine("5. View Teacher Announcements");
                Console.WriteLine("6. Unregister a Subject");
                Console.WriteLine("7. Submit Feedback");
                Console.WriteLine("8. Calculate GPA (standalone)");
                Console.WriteLine("9. View Contact Directory");
                Console.WriteLine("0. Logout");
                Console.Write("Choice: ");

                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid input!");
                    Console.ReadKey();
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine($"{s.Name} | {s.Subject1},{s.Subject2},{s.Subject3} | GPA: {s.Gpa}");
                        Console.ReadKey();
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine(s.Subject1 + "\n" + s.Subject2 + "\n" + s.Subject3);
                        Console.ReadKey();
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine(s.Subject1 + ": " + s.Marks1);
                        Console.WriteLine(s.Subject2 + ": " + s.Marks2);
                        Console.WriteLine(s.Subject3 + ": " + s.Marks3);
                        Console.ReadKey();
                        break;
                    case 4:
                        Console.Clear();
                        Console.WriteLine("Attendance: " + s.Attendance);
                        Console.ReadKey();
                        break;
                    case 5:
                        Console.Clear();
                        foreach (string a in announcementBL.GetAnnouncements())
                            Console.WriteLine(a);
                        Console.ReadKey();
                        break;
                    case 6:
                        Console.Clear();
                        Console.WriteLine("1. " + s.Subject1 + "  2. " + s.Subject2 + "  3. " + s.Subject3);
                        Console.Write("Which subject to unregister: ");
                        int subNum = int.Parse(Console.ReadLine());
                        bool ok = studentBL.UnregisterSubject(s.Id, subNum);
                        Console.WriteLine(ok ? "Unregistered!" : "Invalid choice!");
                        Console.ReadKey();
                        break;
                    case 7:
                        Console.Clear();
                        Console.Write("Enter feedback: ");
                        string fb = Console.ReadLine();
                        feedbackBL.SubmitFeedback(fb);
                        Console.WriteLine("Feedback submitted!");
                        Console.ReadKey();
                        break;
                    case 8:
                        Console.Clear();
                        Console.Write("Marks PF: ");
                        float m1 = float.Parse(Console.ReadLine());
                        Console.Write("Marks AP: ");
                        float m2 = float.Parse(Console.ReadLine());
                        Console.Write("Marks DM: ");
                        float m3 = float.Parse(Console.ReadLine());
                        float gpa = studentBL.CalculateGpaFromMarks(m1, m2, m3);
                        Console.WriteLine("Calculated GPA: " + gpa);
                        Console.ReadKey();
                        break;
                    case 9:
                        Console.Clear();
                        Console.WriteLine("Admission Office: 042-111-222");
                        Console.WriteLine("Admin Help Desk: admin@uet.edu.pk");
                        Console.WriteLine("CS Department: cs_department@uet.edu.pk");
                        Console.ReadKey();
                        break;
                    case 0:
                        Console.WriteLine("Logging out...");
                        break;
                    default:
                        Console.WriteLine("Invalid choice!");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}