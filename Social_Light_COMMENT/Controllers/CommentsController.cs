using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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

        public CommentsController(ICommentsService commentInterface, IMapper mapper, ResponseDto responseDto, AppDbContext appContext)
        {
            _commentInterface = commentInterface;
            _mapper = mapper;
            _responseDto = responseDto;
            _Context = appContext;
        }

        // crude
        [HttpPost]
        public async Task<ActionResult<ResponseDto>> CreateComments(CommentDto commentDto)
        {
            try
            {
                var Comment = _mapper.Map<Comment>(commentDto);
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

        [HttpGet("{userId}")]
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

        [HttpGet("posts/comments")]
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

    }
}