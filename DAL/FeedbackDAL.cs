using System.Collections.Generic;
using System.IO;

namespace LMS.DAL
{
    public class FeedbackDAL
    {
        private string filePath = "feedback.txt";

        public void SaveFeedback(List<string> feedback)
        {
            StreamWriter sw = new StreamWriter(filePath, false);
            foreach (string f in feedback)
            {
                sw.WriteLine(f);
            }
            sw.Flush();
            sw.Close();
        }

        public List<string> LoadFeedback()
        {
            List<string> feedback = new List<string>();

            if (File.Exists(filePath))
            {
                StreamReader sr = new StreamReader(filePath);
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    feedback.Add(line);
                }
                sr.Close();
            }

            return feedback;
        }
    }
}