using System;
using LMS.UI;

namespace LMS
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("|       UNIVERSITY LEARNING MANAGEMENT SYSTEM             |");
            Console.WriteLine("------------------------------------------------------------");

            int choice = -1;
            while (choice != 0)
            {
                Console.WriteLine("\n1. Professor\n2. Student\n0. Exit");
                Console.Write("Choice: ");

                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid input!");
                    continue;
                }

                if (choice == 1)
                {
                    ProfessorPortalUI prof = new ProfessorPortalUI();
                    prof.Login();
                }
                else if (choice == 2)
                {
                    StudentPortalUI stud = new StudentPortalUI();
                    stud.Login();
                }
            }
        }
    }
}