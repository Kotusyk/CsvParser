using App.Data;
using App.Model;
using App.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Threading;

namespace AnnaMelnyk_TestTask.Services
{
    public class RequestService : IRequestService
    {
        private readonly TaskDBContext _context;
        public RequestService(TaskDBContext context)
        {
          _context = context;
        }
        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }
        public async Task<User> GetUserById(int Id)
        {
            return await _context.Users.FindAsync(Id);
        }

        public async Task<User> CreateUser(User UserForCreating)
        {
            _context.Users.Add(UserForCreating);
            await _context.SaveChangesAsync();
            return UserForCreating;

        }
        public async Task<User> UpdateUser(User UserForUpdating)
        {

            var data = _context.Users.Where(s => s.Id == UserForUpdating.Id)
                                                   .FirstOrDefault<User>();

            if (data != null)
            {
                //   data.Id = UserForUpdating.Id;
                data.Name = UserForUpdating.Name;
                data.DateOfBirth = UserForUpdating.DateOfBirth;
                data.Married = UserForUpdating.Married;
                data.Phone = UserForUpdating.Phone;
                data.Salary = UserForUpdating.Salary;
                await _context.SaveChangesAsync();
                return data;
            }

            //_context.Users.Update(UserForUpdating);
            //_context.SaveChanges();

            return UserForUpdating;


         
        }
        public async Task<User> DeleteUser(int id)
        {
            var User = await _context.Users!.FindAsync(id);
            if (User == null)
            {
                throw new ValidationException("Not found");
            }
            _context.Users.Remove(User);
            await _context.SaveChangesAsync();
            return User;
        

        }
        private bool UserExists(int id)
        {
            return _context.Users!.Any(e => e.Id == id);
        }
    }
}

