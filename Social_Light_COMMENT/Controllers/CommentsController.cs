using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Social_Light_COMMENT.Data;
using Social_Light_COMMENT.Models;
using Social_Light_COMMENT.Models.DTO;
using Social_Light_COMMENT.Services.IServices;

namespace Social_Light_COMMENT.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICommentsService _commentInterface;
        private readonly ResponseDto _responseDto;
        private readonly AppDbContext _Context;

        public CommentsController(ICommentsService commentInterface, IMapper mapper, AppDbContext appContext)
        {
            _commentInterface = commentInterface;
            _mapper = mapper;
            _responseDto = new ResponseDto();
            _Context = appContext;
        }

        // crud
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ResponseDto>> CreateComments(CommentCreateDto commentDto)
        {
            try
            {
                // get token form header 
                var token = Request.Headers["Authorization"].FirstOrDefault();
                //  decode token
                var Splittoken = token.Split(" ")[1];
                var decodetoken = new JwtSecurityTokenHandler();
                var jwtToken = decodetoken.ReadJwtToken(Splittoken);
                var userId = jwtToken.Claims.FirstOrDefault(x => x.Type == "sub").Value;

                // get user id from token

                var Comment = _mapper.Map<Comment>(commentDto);
                Comment.UserId = userId;
                var CommentResults = await _commentInterface.CreateComments(Comment);
                if (!string.IsNullOrWhiteSpace(CommentResults))
                {
                    _responseDto.Message = CommentResults;
                    _responseDto.IsSuccess = false;
                    return BadRequest(_responseDto);
                }

                _responseDto.Message = CommentResults;
                _responseDto.IsSuccess = true;

                return Ok(_responseDto);
            }
            catch (Exception e)
            {
                _responseDto.Message = e.Message;
                _responseDto.IsSuccess = false;
                return BadRequest(_responseDto);
            }
        }

        [HttpGet("/{userId}")]
        [Authorize]
        public async Task<ActionResult<ResponseDto>> GetComments(string userId)
        {
            try
            {
                var CommentResults = await _commentInterface.GetUserComments(userId);
                if (CommentResults == null)
                {
                    _responseDto.Message = "No Comments Found";
                    _responseDto.IsSuccess = false;
                    return BadRequest(_responseDto);
                }

                _responseDto.Message = "";
                _responseDto.IsSuccess = true;
                _responseDto.Result = CommentResults;

                return Ok(_responseDto);
            }
            catch (Exception e)
            {
                _responseDto.Message = e.Message;
                _responseDto.IsSuccess = false;
                return BadRequest(_responseDto);
            }
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<ResponseDto>> UpdateComments(CommentDto commentDto)
        {
            try
            {
                var Comment = _mapper.Map<Comment>(commentDto);
                var CommentResults = await _commentInterface.UpdateUserComments(Comment);
                if (!string.IsNullOrWhiteSpace(CommentResults))
                {
                    _responseDto.Message = CommentResults;
                    _responseDto.IsSuccess = false;
                    return BadRequest(_responseDto);
                }

                _responseDto.Message = CommentResults;
                _responseDto.IsSuccess = true;

                return Ok(_responseDto);
            }
            catch (Exception e)
            {
                _responseDto.Message = e.Message;
                _responseDto.IsSuccess = false;
                return BadRequest(_responseDto);
            }
        }

        [HttpDelete("{userId}")]
        [Authorize]
        public async Task<ActionResult<ResponseDto>> DeleteComments(UserCommentsDTO userCommentsDTO)
        {
            try
            {
                var comment = await _Context.Comments
                    .Where(c => c.UserId == userCommentsDTO.UserId && c.PostId == userCommentsDTO.postId && c.Id == userCommentsDTO.CommentId)
                    .FirstOrDefaultAsync();

                if (comment == null)
                {
                    _responseDto.Message = "No Comments Found";
                    _responseDto.IsSuccess = false;
                    return BadRequest(_responseDto);
                }

                var deleteCommentResults = await _commentInterface.DeleteUserComments(comment);

                if (!string.IsNullOrWhiteSpace(deleteCommentResults))
                {
                    _responseDto.Message = deleteCommentResults;
                    _responseDto.IsSuccess = false;
                    return BadRequest(_responseDto);
                }

                _responseDto.Message = deleteCommentResults;
                _responseDto.IsSuccess = true;
                return Ok(_responseDto);
            }
            catch (Exception e)
            {
                _responseDto.Message = e.Message;
                _responseDto.IsSuccess = false;
                return BadRequest(_responseDto);
            }
        }

        [HttpGet("posts/comments/{postId}")]
        [Authorize]
        public async Task<ActionResult<ResponseDto>> GetPostComments(string postId)
        {
            try
            {
                var comments = await _commentInterface.GetPostComments(postId);
                if (comments == null)
                {
                    _responseDto.Message = "No Comments Found";
                    _responseDto.IsSuccess = false;
                    return BadRequest(_responseDto);
                }
                _responseDto.Message = "";
                _responseDto.IsSuccess = true;
                _responseDto.Result = comments;
                return Ok(_responseDto);
            }
            catch (Exception e)
            {
                _responseDto.Message = e.Message;
                _responseDto.IsSuccess = false;
                return BadRequest(_responseDto);
            }
        }
        // get all comments
        [HttpGet]
        public async Task<ActionResult<ResponseDto>> GetAllComments()
        {
            try
            {
                var comments = await _commentInterface.GetAllComments();
                if (comments == null)
                {
                    _responseDto.Message = "No Comments Found";
                    _responseDto.IsSuccess = false;
                    return BadRequest(_responseDto);
                }
                _responseDto.Message = "";
                _responseDto.IsSuccess = true;
                _responseDto.Result = comments;
                return Ok(_responseDto);
            }
            catch (Exception e)
            {
                _responseDto.Message = e.Message;
                _responseDto.IsSuccess = false;
                return BadRequest(_responseDto);
            }
        
        }
    }
}