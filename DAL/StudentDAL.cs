using System.Collections.Generic;
using System.IO;
using LMS.ML;

namespace LMS.DAL
{
    public class StudentDAL
    {
        private string filePath = "students.txt";

        public void SaveStudents(List<Student> students)
        {
            StreamWriter sw = new StreamWriter(filePath, false); // OWERWRITE
            foreach (Student s in students)
            {
                string line = $"{s.Id}|{s.Name}|{s.Subject1}|{s.Subject2}|{s.Subject3}|" +
                              $"{s.Marks1}|{s.Marks2}|{s.Marks3}|{s.Attendance}|{s.Gpa}";
                sw.WriteLine(line);
            }
            sw.Flush();
            sw.Close();
        }

        public List<Student> LoadStudents()
        {
            List<Student> students = new List<Student>();

            if (File.Exists(filePath))
            {
                StreamReader sr = new StreamReader(filePath);
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] p = line.Split('|');
                    Student s = new Student(
                        int.Parse(p[0]), p[1], p[2], p[3], p[4],
                        int.Parse(p[5]), int.Parse(p[6]), int.Parse(p[7]),
                        float.Parse(p[8]), float.Parse(p[9])
                    );
                    students.Add(s);
                }
                sr.Close();
            }

            return students;
        }
    }
}