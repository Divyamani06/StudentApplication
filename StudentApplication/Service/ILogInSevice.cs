using StudentApplication.Model;

namespace StudentApplication.Service
{
    public interface ILogInSevice
    {
        Task<string> LogInAsync(UserModel model);
        Task<string> SignUp(UserModel model);
    }
}