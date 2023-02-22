using Users_Product_Security.Models;

namespace Users_User_Security.Interfaces
{
    public interface IUser
    {
        //GET-UserS
        List<User> GetUsers();
        //GET-UserS-BY-ID
        User UserGetUserById(int id);
        //CREATE[POST]-NEW-User
        List<User> CreateUsers(User User);
        //DELETE-User
        string DeleteUser(int id);
        //UPDATE-[PUT] User
        List<User> updateEmployee(User User);
    }
}
