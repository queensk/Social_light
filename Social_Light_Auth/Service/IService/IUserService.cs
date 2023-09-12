using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Social_Light_Auth.Models.DTO;
using Social_Light_Auth.Models.DTO.Request;
using Social_Light_Auth.Models.DTO.Response;

namespace Social_Light_Auth.Service.IService
{
    public interface IUserService
    {
        // Register user
        Task<string> RegisterUser(UserDTO registerRequestDTO);

        // Login user
        Task<LoginResponseDto> LoginUser(LoginRequestDto loginRequestDto);

        // update user role
        Task<string> UpdateUserRole(UpdateUserRole updateUserRole);

        // get all user 
        Task<List<UserDTO>> GetAllUser();

        // get user by id
        Task<UserResponseDTO> GetUserById(string id);

        // get a users post
        Task<UserDTO> GetUsersPost(string id);
        
    }
}