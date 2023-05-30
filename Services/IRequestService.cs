using App.Model;

namespace App.Services
{
    public interface IRequestService
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUserById(int Id);
        //Task<User> UploadFile(IFormFile file);
        Task<User> CreateUser(User assignmentForCreating);
        Task<User> UpdateUser(User assignmentForUpdating);
        Task<User> DeleteUser(int Id);
    }
}
