namespace LMS.ML
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Subject1 { get; set; }
        public string Subject2 { get; set; }
        public string Subject3 { get; set; }

        public int Marks1 { get; set; }
        public int Marks2 { get; set; }
        public int Marks3 { get; set; }

        public float Attendance { get; set; }
        public float Gpa { get; set; }

        public Student()
        {
            this.Name = "";
            this.Subject1 = "-";
            this.Subject2 = "-";
            this.Subject3 = "-";
        }

        public Student(
            int id,
            string name,
            string subject1,
            string subject2,
            string subject3,
            int marks1,
            int marks2,
            int marks3,
            float attendance,
            float gpa)
        {
            this.Id = id;
            this.Name = name;
            this.Subject1 = subject1;
            this.Subject2 = subject2;
            this.Subject3 = subject3;
            this.Marks1 = marks1;
            this.Marks2 = marks2;
            this.Marks3 = marks3;
            this.Attendance = attendance;
            this.Gpa = gpa;
        }
    }
}