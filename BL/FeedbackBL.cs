using System.Collections.Generic;
using LMS.DAL;

namespace LMS.BL
{
    public class FeedbackBL
    {
        private List<string> feedback;
        private FeedbackDAL dal = new FeedbackDAL();

        public FeedbackBL()
        {
            feedback = dal.LoadFeedback();
        }

        public void SubmitFeedback(string text)
        {
            feedback.Add(text);
            dal.SaveFeedback(feedback);
        }

        public List<string> GetFeedback()
        {
            return feedback;
        }
    }
}