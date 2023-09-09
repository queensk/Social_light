using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Social_Light_COMMENT.Data;
using Social_Light_COMMENT.Models;
using Social_Light_COMMENT.Models.DTO;
using Social_Light_COMMENT.Services.IServices;

namespace Social_Light_COMMENT.Services
{
    public class CommentsService : ICommentsService
    {
        private readonly AppDbContext _Context;
        private readonly IMapper _mapper;
        private readonly ICommentsService _CommentsService;

        public CommentsService(AppDbContext appDbContext, IMapper mapper)
        {
            _Context = appDbContext;
            _mapper = mapper;
        }
        public async Task<string> CreateComments(Comment CommentsDto)
        {
            try
            {
                _Context.Comments.Add(CommentsDto);
                await _Context.SaveChangesAsync();
                return "Comment Added Successfully";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public async Task<string> DeleteUserComments(Comment Comment)
        {
            try
            {
                _Context.Comments.Remove(Comment);
                await _Context.SaveChangesAsync();
                return "Comment Deleted Successfully";
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<Comment> GetUserComments(string userId)
        {
            return _Context.Comments.FirstOrDefault(c => c.UserId == userId);
        }

        public async Task<string> UpdateUserComments(Comment CommentsDto)
        {
            try
            {
                _Context.Comments.Update(CommentsDto);
                await _Context.SaveChangesAsync();
                return "Comment Updated Successfully";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public async Task<List<Comment>> GetPostComments(string postId)
        {
            return _Context.Comments.Where(c => c.PostId == postId).ToList();
        }
    }
}