using API_Tutorial.Models.DTO;
using API_Tutorial.Models.Entities;

namespace API_Tutorial.Interfaces
{
    public interface IAuthService
    {
        public  Task<User?> RegisterUser(UserDTO userDTO);
        public  Task<string> login(UserDTO userDTO);
    }
}
