using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Social_Light_Auth.Models;
using Social_Light_Auth.Models.DTO;
using Social_Light_Auth.Models.DTO.Request;
using Social_Light_Auth.Models.DTO.Response;
using Social_Light_Auth.Service.IService;
using Social_light_Message_Bus.MessageBus;

namespace Social_Light_Auth.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ResponseDTO  _responseDTO;
        private readonly IConfiguration _configuration;
        private readonly IMessageBus _messageBus;
        // private readonly IMapper _mapper;
        public UserController(IUserService userService, IConfiguration configuration, IMessageBus messageBus)
        {
            _userService = userService;
            _responseDTO = new ResponseDTO();
            _configuration = configuration;
            _messageBus = messageBus;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ResponseDTO>> Register(UserDTO userDTO)
        {
            // user.UserName = registerRequestDTO.Email;
            try
            {
                var response = await _userService.RegisterUser(userDTO);
                if (response == "User created successfully")
                {
                    _responseDTO.Message = response;
                    _responseDTO.IsSuccess = true;
                    _responseDTO.Data = null;
                    //send a message to our ServiceBus --Queue
                    var queueName = _configuration.GetSection("QueuesandTopics:RegisterUser").Get<string>();
                    var message = new UserMessage()
                    {
                        Email = userDTO.Email,
                        Name = userDTO.Name,
                    };
                    await _messageBus.PublishMessage(message, queueName);
                    return Ok(_responseDTO);  
                }          
                else{
                    _responseDTO.Message = response;
                    _responseDTO.IsSuccess = false;
                    _responseDTO.Data = null;
                    return BadRequest(_responseDTO);
                }
            }
            catch(Exception ex){
                _responseDTO.Message = ex.Message;
                _responseDTO.IsSuccess = false;
                _responseDTO.Data = null;
                return BadRequest(_responseDTO);

            }
        }

        [HttpPost("role")]
        public async Task<ActionResult<ResponseDTO>> UpdateRole(UpdateUserRole updateUserRole)
        {
            try
            {
                var response = await _userService.UpdateUserRole(updateUserRole);
                _responseDTO.Message = response;
                _responseDTO.IsSuccess = true;
                _responseDTO.Data = null;
                return Ok(_responseDTO);            
            }
            catch(Exception ex){
                _responseDTO.Message = ex.Message;
                _responseDTO.IsSuccess = false;
                _responseDTO.Data = null;
                return Ok(_responseDTO);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<ResponseDTO>> Login(LoginRequestDto loginRequestDTO)
        {
            try
            {
                var response = await _userService.LoginUser(loginRequestDTO);
                _responseDTO.Message = "Success Login";
                _responseDTO.IsSuccess = true;
                _responseDTO.Data = response;
                return Ok(_responseDTO);            
            }
            catch(Exception ex){
                _responseDTO.Message = ex.Message;
                _responseDTO.IsSuccess = false;
                _responseDTO.Data = null;
                return Ok(_responseDTO);
            }
        }
        // get user data by id 
        [HttpGet("/{UserId}")]
        [Authorize]
        public async Task<ActionResult<ResponseDTO>> GetUserById(string UserId)
        {
            try
            {
                var response = await _userService.GetUserById(UserId);
                _responseDTO.Message = "Success";
                _responseDTO.IsSuccess = true;
                _responseDTO.Data = response;
                return Ok(_responseDTO);            
            }
            catch(Exception ex){
                _responseDTO.Message = ex.Message;
                _responseDTO.IsSuccess = false;
                _responseDTO.Data = null;
                return Ok(_responseDTO);
            }
        }
    } 
}