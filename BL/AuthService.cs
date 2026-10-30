namespace LMS.BL
{

    public class AuthService
    {
        public bool Login(string username, int password)
        {
            return username == "ADMIN" && password == 123;
        }
    }
}