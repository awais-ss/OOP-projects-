using System.Collections.Generic;
using System.IO;

namespace LMS.DAL
{
    public class AnnouncementDAL
    {
        private string filePath = "announcements.txt";

        public void SaveAnnouncements(List<string> announcements)
        {
            StreamWriter sw = new StreamWriter(filePath, false);
            foreach (string a in announcements)
            {
                sw.WriteLine(a);
            }
            sw.Flush();
            sw.Close();
        }

        public List<string> LoadAnnouncements()
        {
            List<string> announcements = new List<string>();

            if (File.Exists(filePath))
            {
                StreamReader sr = new StreamReader(filePath);
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    announcements.Add(line);
                }
                sr.Close();
            }

            return announcements;
        }
    }
}