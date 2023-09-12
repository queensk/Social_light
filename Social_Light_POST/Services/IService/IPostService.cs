using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Social_Light_POST.Models;
using Social_Light_POST.Models.DTO;

namespace Social_Light_POST.Services.IService
{
    public interface IPostService
    {
        Task<IEnumerable<Post>> GetPostsAsync();
        Task<Post?> GetPostByIdAsync(Guid id);
        Task<string> AddPostAsync(Post post);
        Task<string> DeletePostAsync(Post post);
        Task<string> UpdatePostAsync(Post post);
        Task<IEnumerable<UserPostsAndCommentsDto>> GetUserPostsAndCommentsAsync(string userId);
    }
}