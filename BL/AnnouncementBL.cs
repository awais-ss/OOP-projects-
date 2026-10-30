using System.Collections.Generic;
using LMS.DAL;

namespace LMS.BL
{
    public class AnnouncementBL
    {
        private List<string> announcements;
        private AnnouncementDAL dal = new AnnouncementDAL();

        public AnnouncementBL()
        {
            announcements = dal.LoadAnnouncements();
        }

        public void PostAnnouncement(string text)
        {
            announcements.Add(text);
            dal.SaveAnnouncements(announcements);
        }

        public List<string> GetAnnouncements()
        {
            return announcements;
        }
    }
}