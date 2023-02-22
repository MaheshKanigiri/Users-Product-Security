using Microsoft.EntityFrameworkCore;
using Users_Product_Security.DTO;
using Users_Product_Security.Models;
using Users_User_Security.Interfaces;
using Product = Users_Product_Security.Models.Product;
using User = Users_Product_Security.Models.User;

namespace Users_User_Security.Repository
{
    public class UserRepository:IUser
    {
        private readonly Practice1Context _context;

        public UserRepository(Practice1Context context)
        {
            _context = context;
        }
        //GET-IMP
        public List<User> CreateUsers(User User)
        {
            _context.Users.Add(User);
            _context.SaveChanges();
            return _context.Users.ToList();
        }

        public string DeleteUser(int id)
        {

            var User = _context.Users.Find(id);
            if (User == null)
            {
                throw new Exception("User with ID:" + id + " Not Found");
            }
            _context.Users.Remove(User);
            _context.SaveChanges();

            return "Deleted successfully";
        }

        public List<User> GetUsers()
        {
            return _context.Users.Include(x=>x.PidNavigation).ToList();
        }

        public User UserGetUserById(int id)
        {
            var prod = _context.Users.Find(id);
            if (prod == null)
            {
                throw new Exception("User with ID:" + id + " Not Found");
            }
            else { return (prod); };
        }

        public List<User> updateEmployee(User User)
        {
            var oldUser = _context.Users.Find(User.Pid);
            if (oldUser == null)
            {
                throw new Exception("User with ID:" + User.Pid + " Not Found");
            }

            oldUser.Name=User.Name;
            oldUser.Email=User.Email;   
            oldUser.Password=User.Password;
            oldUser.Role=User.Role;

            _context.SaveChanges();

            return _context.Users.ToList();

        }
    }
}
