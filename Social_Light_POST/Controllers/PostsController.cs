using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Social_Light_POST.Models;
using Social_Light_POST.Models.DTO;
using Social_Light_POST.Services.IService;

namespace Social_Light_POST.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPostService _postInterface;
        private readonly ICommentService _commentInterface;
        private readonly ResponseDto _responseDto;

        public PostsController(IMapper mapper, IPostService postInterface, ICommentService commentService)
        {
            _mapper = mapper;
            _postInterface = postInterface;
            _responseDto = new ResponseDto();
            _commentInterface = commentService;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDto>> GetAllPosts()
        {
            try
            {
                var posts = await _postInterface.GetPostsAsync();
                if (posts == null)
                {
                    _responseDto.IsSuccess = false;
                    _responseDto.Message = "No posts found";
                    return NotFound(_responseDto);
                }
                _responseDto.IsSuccess = true;
                _responseDto.Result = posts;
                return Ok(_responseDto);
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
                return BadRequest(_responseDto);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ResponseDto>> CreatePost(PostRequestDto postDto)
        {
            try
            {
                var newPost = _mapper.Map<Post>(postDto);
                var response = await _postInterface.AddPostAsync(newPost);
                if (string.IsNullOrWhiteSpace(response))
                {
                    _responseDto.IsSuccess = false;
                    _responseDto.Message = "Post not created";
                    return BadRequest(_responseDto);
                }
                _responseDto.IsSuccess = true;
                _responseDto.Message = response;
                return Ok(_responseDto);
            }
            catch(Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
                return BadRequest(_responseDto);
            }
        }

        [HttpGet("GetById({Id})")]
        public async Task<ActionResult<ResponseDto>> GetPostById(Guid Id)
        {
            try
            {
                var post = await _postInterface.GetPostByIdAsync(Id);
                if (post == null)
                {
                    _responseDto.IsSuccess = false;
                    _responseDto.Message = "No post found";
                    return NotFound(_responseDto);
                }
                _responseDto.IsSuccess = true;
                _responseDto.Result = post;
                return Ok(_responseDto);
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
                return BadRequest(_responseDto);
            }
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<ResponseDto>> UpdatePost(PostRequestDto postDto)
        {
            try
            {
                var post = _mapper.Map<Post>(postDto);
                var response = await _postInterface.UpdatePostAsync(post);
                if (string.IsNullOrWhiteSpace(response))
                {
                    _responseDto.IsSuccess = false;
                    _responseDto.Message = "Post not updated";
                    return BadRequest(_responseDto);
                }
                _responseDto.IsSuccess = true;
                _responseDto.Message = response;
                return Ok(_responseDto);
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
                return BadRequest(_responseDto);
            }
        }
        [HttpDelete]
        [Authorize]
        public async Task<ActionResult<ResponseDto>> DeletePost(Guid Id)
        {
            try
            {
                var post = await _postInterface.GetPostByIdAsync(Id);
                if (post == null)
                {
                    _responseDto.IsSuccess = false;
                    _responseDto.Message = "No post found";
                    return NotFound(_responseDto);
                }
                var response = await _postInterface.DeletePostAsync(post);
                _responseDto.IsSuccess = true;
                _responseDto.Result = post;
                _responseDto.Message = response;
                return Ok(_responseDto);
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
                return BadRequest(_responseDto);
            }
        }
        [HttpGet("GetPostAndComments/{Id}")]
        public async Task<ActionResult<ResponseDto>> GetUserPostsAndComments(string userId)
        {
            try{
                var userPostsAndCommentsDtoList = await _postInterface.GetUserPostsAndCommentsAsync(userId);
                _responseDto.Result = userPostsAndCommentsDtoList;
                _responseDto.IsSuccess = true;
                return Ok(_responseDto);
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
                return BadRequest(_responseDto);
            }
        } 
    }
}
