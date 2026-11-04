using LMS.BL;
using LMS.DAL;
using LMS.ML;
using System;
using System.Collections.Generic;
using System.Xml.Schema;

namespace LMS.UI
{
    public class ProfessorPortalUI
    {
        private StudentBL studentBL = new StudentBL();
        private AnnouncementBL announcementBL = new AnnouncementBL();
        private FeedbackBL feedbackBL = new FeedbackBL();
        private AuthService auth = new AuthService();

        public void Login()
        {
            Console.Clear();
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("           LMS PROFESSOR PORTAL             ");
            Console.WriteLine("--------------------------------------------");

            for (int attempt = 0; attempt < 3; attempt++)
            {
                Console.Write("Enter Username: ");
                string username = Console.ReadLine();
                Console.Write("Enter Password: ");

                if (!int.TryParse(Console.ReadLine(), out int password))
                {
                    Console.WriteLine("Invalid password format!");
                    continue;
                }

                if (auth.Login(username, password))
                {
                    Menu();
                    return;
                }
                else
                {
                    Console.WriteLine("Wrong credentials! Attempts left: " + (2 - attempt));
                }
            }
            Console.WriteLine("Too many attempts! Login failed.");
            Console.ReadKey();
        }

        private void Menu()
        {
            int choice = -1;
            while (choice != 0)
            {
                Console.Clear();
                Console.WriteLine("-------------------------------------------------------------");
                Console.WriteLine("1. Register New Student");
                Console.WriteLine("2. View All Students");
                Console.WriteLine("3. Update Subject Marks");
                Console.WriteLine("4. Sort Students by GPA");
                Console.WriteLine("5. Search Student Record");
                Console.WriteLine("6. Post Announcement");
                Console.WriteLine("7. View Class Performance");
                Console.WriteLine("8. Delete Student Record");
                Console.WriteLine("9. Read Students Feedback");
                Console.WriteLine("10. Paginated View");
                Console.WriteLine("11. QUICK FILTER");
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
                    case 1: RegisterStudent(); break;
                    case 2: ViewAllStudents(); break;
                    case 3: UpdateMarks(); break;
                    case 4: SortByGpa(); break;
                    case 5: SearchStudent(); break;
                    case 6: PostAnnouncement(); break;
                    case 7: ViewPerformance(); break;
                    case 8: DeleteStudent(); break;
                    case 9: ReadFeedback(); break;
                    case 10: pView(); break;
                    case 11: qFilter(); break;
                    case 0: Console.WriteLine("Logging out..."); break;
                    default: Console.WriteLine("Invalid choice!"); Console.ReadKey(); break;
                }
            }
        }

        private void RegisterStudent()
        {
            Console.Clear();
            Console.WriteLine("===== REGISTER NEW STUDENT =====");

            Console.Write("Enter Student ID: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Enter Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Subject 1: ");
            string sub1 = Console.ReadLine();
            Console.Write("Enter Subject 2: ");
            string sub2 = Console.ReadLine();
            Console.Write("Enter Subject 3: ");
            string sub3 = Console.ReadLine();

            Console.Write("Enter Marks for " + sub1 + ": ");
            int marks1 = int.Parse(Console.ReadLine());
            Console.Write("Enter Marks for " + sub2 + ": ");
            int marks2 = int.Parse(Console.ReadLine());
            Console.Write("Enter Marks for " + sub3 + ": ");
            int marks3 = int.Parse(Console.ReadLine());

            Console.Write("Enter Attendance (0-100): ");
            float attendance = float.Parse(Console.ReadLine());

            // GPA ab manually nahi lete -- marks se automatically calculate hota hai
            float gpa = studentBL.CalculateGpaFromMarks(marks1, marks2, marks3);

            Student s = new Student(id, name, sub1, sub2, sub3, marks1, marks2, marks3, attendance, gpa);
            bool ok = studentBL.RegisterStudent(s);

            Console.WriteLine(ok ? "Student Registered! GPA calculated as: " + gpa
                                  : "Failed -- ID already exists or invalid attendance.");
            Console.ReadKey();
        }

        private void ViewAllStudents()
        {
            Console.Clear();
            Console.WriteLine("NAME\t\tSUB1\tSUB2\tSUB3\tM1\tM2\tM3\tATTEND\tGPA");
            Console.WriteLine("----------------------------------------------------------------------------");
            foreach (Student s in studentBL.GetAllStudents())
            {
                Console.WriteLine($"{s.Name}\t{s.Subject1}\t{s.Subject2}\t{s.Subject3}\t" +
                                   $"{s.Marks1}\t{s.Marks2}\t{s.Marks3}\t{s.Attendance}\t{s.Gpa}");
            }
            Console.ReadKey();
        }

        private void UpdateMarks()
        {
            Console.Clear();
            Console.Write("Enter student name: ");
            string name = Console.ReadLine();

            Console.Write("New Marks 1: ");
            int m1 = int.Parse(Console.ReadLine());
            Console.Write("New Marks 2: ");
            int m2 = int.Parse(Console.ReadLine());
            Console.Write("New Marks 3: ");
            int m3 = int.Parse(Console.ReadLine());

            bool ok = studentBL.UpdateMarks(name, m1, m2, m3);
            Console.WriteLine(ok ? "Marks updated!" : "Student not found!");
            Console.ReadKey();
        }

        private void SortByGpa()
        {
            Console.Clear();
            studentBL.SortByGpa();
            foreach (Student s in studentBL.GetAllStudents())
            {
                Console.WriteLine(s.Name + " - GPA: " + s.Gpa);
            }
            Console.ReadKey();
        }

        private void SearchStudent()
        {
            Console.Clear();
            Console.Write("Enter name to search: ");
            string name = Console.ReadLine();

            Student s = studentBL.SearchByName(name);
            if (s != null)
                Console.WriteLine($"{s.Name} | {s.Subject1},{s.Subject2},{s.Subject3} | GPA: {s.Gpa}");
            else
                Console.WriteLine("Not found!");
            Console.ReadKey();
        }

        private void PostAnnouncement()
        {
            Console.Clear();
            Console.Write("Enter announcement text: ");
            string text = Console.ReadLine();
            announcementBL.PostAnnouncement(text);
            Console.WriteLine("Announcement posted!");
            Console.ReadKey();
        }

        private void ViewPerformance()
        {
            Console.Clear();
            Console.WriteLine("Total Students: " + studentBL.GetTotalStudents());
            Console.WriteLine("Average GPA: " + studentBL.GetAverageGpa());
            Console.ReadKey();
        }

        private void DeleteStudent()
        {
            Console.Clear();
            Console.Write("Enter name to delete: ");
            string name = Console.ReadLine();

            bool ok = studentBL.DeleteStudent(name);
            Console.WriteLine(ok ? "Deleted!" : "Not found!");
            Console.ReadKey();
        }

        private void ReadFeedback()
        {
            Console.Clear();
            foreach (string f in feedbackBL.GetFeedback())
            {
                Console.WriteLine(f);
            }
            Console.ReadKey();
        }
        private void pView()
        {
            Console.WriteLine("HOW MANY RECORDS WANT ON A PAGE?:");
            int numberOfRecords = int.Parse(Console.ReadLine());
            char PageLoacation = 'Z';
            List<Student> stu = studentBL.GetAllStudents();
            int currentPageNumber = 0;
            while (PageLoacation != 'X')
            {
                Console.WriteLine("N for Next , P for previous , L for last , F for First , X for exit: ");
                PageLoacation = char.Parse(Console.ReadLine());
                if (PageLoacation == 'F')
                {
                    int i = 0;
                    foreach (Student s in stu)
                    {
                        Console.WriteLine($"ID: {s.Id}, NAME: {s.Name}");
                        currentPageNumber++;
                        i++;
                        if (i >= numberOfRecords) return;
                        Console.ReadKey();
                    }
                }
                else if (PageLoacation == 'L')
                {
                    int i = 0;
                    int LastStu = stu.Count - numberOfRecords;
                    foreach (Student s in stu)
                    {
                        i++;
                        if (i >= LastStu && i < stu.Count)
                        {
                            Console.WriteLine($"ID: {s.Id}, NAME: {s.Name}");
                            Console.ReadKey();
                        }
                    }
                }
                else if (PageLoacation == 'N')
                {
                    if (currentPageNumber >= stu.count)
                    {
                        Console.WriteLine("ALREADY ON LAST PAGE");
                    }
                    int i = 0;
                    foreach (Student s in stu)
                    {
                        i++;
                        int towhen = currentPageNumber + numberOfRecords;
                        if (i >= currentPageNumber && i < towhen)
                        {
                            currentPageNumber++;
                            Console.WriteLine($"ID: {s.Id}, NAME: {s.Name}");
                            Console.ReadKey();
                        }
                    }
                }
                else if (PageLoacation == 'P')
                {
                    if (currentPageNumber <= 0)
                    {
                        Console.WriteLine("ALREADY ON 1st PAGE");
                    }
                    int i = 0;
                    foreach (Student s in stu)
                    {
                        i++;
                        int towhen = currentPageNumber - numberOfRecords;
                        if (i <= currentPageNumber && i > towhen)
                        {
                            currentPageNumber--;
                            Console.WriteLine($"ID: {s.Id}, NAME: {s.Name}");
                            Console.ReadKey();
                        }
                    }
                }
                else
                {
                    Console.WriteLine("INVALID CHOICE !!");
                }
            }
        }
        private void qFilter()
        {
            // have to write in BL after 
            Console.WriteLine("ENTER THE GPA between 1 AND 4 for filtering: ");
            float GPA = float.Parse(Console.ReadLine());
            Console.WriteLine("ENTER SUB 1 NAme for filtering :");
            string SUB1Name = Console.ReadLine();
            Console.WriteLine("ABOVE OR BELOW");
            string action = Console.ReadLine();
            List<Student> stuu = studentBL.GetAllStudents();
            List<Student> filterStu = new List<Student>();
            if (action == "ABOVE")
            {
                foreach (Student s in stuu)
                {
                    if (s.Gpa >= GPA && s.Subject1 == SUB1Name)
                    {
                        filterStu.Add(s);
                    }
                }
            }
            else if (action == "BELOW")
            {
                foreach (Student s in stuu)
                {
                    if (s.Gpa <= GPA && s.Subject1 == SUB1Name)
                    {
                        filterStu.Add(s);
                    }
                }
            }
            else
            {
                Console.WriteLine("INAVLID INPUT!!");

            }
            int max = 0, min = 0;
            if (filterStu == null)
            {
                Console.WriteLine("NO RECORED FOUND !!");
                return;
            }
            else
            {
                foreach (Student s in filterStu)
                {
                    Console.WriteLine($"ID: {s.Id}, NAME: {s.Name} , GPA : {s.Gpa} , SUB 1 : {s.Subject1}");
                    if (max < s.Gpa)
                    {
                        max = s.Gpa;
                    }
                    if (min > s.Gpa)
                    {
                        min = s.Gpa;
                    }
                    Console.ReadKey();
                }
                Console.WriteLine($"total filtered data is : {filterStu.Count} , highest is : {max} , MIN IS : {min}")
            }
        }
    }
}