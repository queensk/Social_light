using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure;
using Social_light_Frontend.Models;
using Social_light_Frontend.Models.Comment;

namespace Social_light_Frontend.Services.Comment
{
    public interface ICommentService
    {
        Task<List<CommentDto>> GetPostComments(Guid postId);

        // create comment
        Task<ResponseDto> CreateComment(CommentCreateDto commentCreateDto);
    
    }
}