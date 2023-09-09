using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Social_Light_COMMENT.Models;
using Social_Light_COMMENT.Models.DTO;

namespace Social_Light_COMMENT.Services.IServices
{
    public interface ICommentsService
    {
        Task<string> CreateComments(Comment CommentsDto);

        Task<Comment> GetUserComments(string userId);

        Task<string> UpdateUserComments(Comment CommentsDto);

        Task<string> DeleteUserComments(Comment Comment);
        Task<List<Comment>> GetPostComments(string postId);
    }

}