using System;
using System.Collections.Generic;
using System.Linq;
using LMS.ML;
using LMS.DAL;

namespace LMS.BL
{
    public class StudentBL
    {
        private List<Student> students;
        private StudentDAL dal = new StudentDAL();

        public StudentBL()
        {
            students = dal.LoadStudents();
        }

        // - REGISTER NEW STUDENT
        public bool RegisterStudent(Student s)
        {
            foreach (Student existing in students)
            {
                if (existing.Id == s.Id)
                {
                    return false;   // ID already exists
                }
            }

            if (s.Attendance < 0 || s.Attendance > 100) return false;
            if (s.Gpa < 0.0 || s.Gpa > 4.0) return false;

            students.Add(s);
            dal.SaveStudents(students);
            return true;
        }

        // VIEW ALL STUDENTS
        public List<Student> GetAllStudents()
        {
            return students;
        }





        //   SEARCH STUDENT BY NAME
        public Student SearchByName(string name)
        {
            foreach (Student s in students)
            {
                if (s.Name == name)
                {
                    return s;
                }
            }
            return null;
        }

        public bool UpdateMarks(string name, int marks1, int marks2, int marks3)
        {
            Student s = SearchByName(name);
            if (s == null) return false;

            s.Marks1 = marks1;
            s.Marks2 = marks2;
            s.Marks3 = marks3;
            s.Gpa = CalculateGpaFromMarks(marks1, marks2, marks3);
            dal.SaveStudents(students);
            return true;
        }

        //SORT BY GPA 
        public void SortByGpa()
        { 
            students.Sort((a, b) => b.Gpa.CompareTo(a.Gpa));
        }


        //  DELETE STUDENT
        public bool DeleteStudent(string name)
        {
            Student s = SearchByName(name);
            if (s == null) return false;

            students.Remove(s);
            dal.SaveStudents(students);
            return true;
        }

        public int GetTotalStudents()
        {
            return students.Count;
        }
        //  GET AVRAGE GPA
        public float GetAverageGpa()
        {
            if (students.Count == 0) return 0;

            float sum = 0;
            foreach (Student s in students)
            {
                sum += s.Gpa;
            }
            return sum / students.Count;
        }

        // UNREGISTER A SUBJECT -------
        public bool UnregisterSubject(int studentId, int subjectNumber)
        {
            Student s = null;
            foreach (Student st in students)
            {
                if (st.Id == studentId)
                {
                    s = st;
                    break;
                }
            }
            if (s == null) return false;

            if (subjectNumber == 1) s.Subject1 = "-";
            else if (subjectNumber == 2) s.Subject2 = "-";
            else if (subjectNumber == 3) s.Subject3 = "-";
            else return false;

            dal.SaveStudents(students);
            return true;
        }

        // - CALCULATE GPA FROM MARKS 
        public float CalculateGpaFromMarks(float marks1, float marks2, float marks3)
        {
            float weighted = (marks1 * 0.35f) + (marks2 * 0.3f) + (marks3 * 0.35f);

            if (weighted > 75) return 4.0f;
            else if (weighted > 60) return 3.5f;
            else if (weighted > 50) return 3.0f;
            else if (weighted > 40) return 2.5f;
            else if (weighted > 30) return 2.0f;
            else return 0.0f;
        }

        
        public Student FindById(int id)
        {
            foreach (Student s in students)
            {
                if (s.Id == id)
                {
                    return s;
                }
            }
            return null;
        }
    }
}