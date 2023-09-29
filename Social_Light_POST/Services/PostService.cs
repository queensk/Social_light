using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Social_Light_POST.Data;
using Social_Light_POST.Models;
using Social_Light_POST.Models.DTO;
using Social_Light_POST.Services.IService;

namespace Social_Light_POST.Services
{
    public class PostService : IPostService
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        private readonly ICommentService _commentsService;

        public PostService(AppDbContext context, ICommentService commentsService, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
            _commentsService = commentsService;
        }
        public async Task<string> AddPostAsync(Post post)
        {
            try
            {
                _context.Posts.Add(post);
                await _context.SaveChangesAsync();
                return "Post Added";
            }catch(Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> DeletePostAsync(Post post)
        {
            try
            {
                _context.Posts.Remove(post);
                await _context.SaveChangesAsync();
                return "Post Deleted";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public async Task<Post?> GetPostByIdAsync(Guid id)
        {
            return await _context.Posts.FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<IEnumerable<Post>> GetPostsAsync()
        {
            return await _context.Posts.ToListAsync();
        }
        public async Task<string> UpdatePostAsync(Post post)
        {
            try
            {
                // get the post with this id
               var postData = _context.Posts.FirstOrDefault(p => p.Id == post.Id);
               postData.Title = post.Title;
               postData.Content = post.Content;
               postData.PostImage = post.PostImage;
                await _context.SaveChangesAsync();
                return "Post Updated";
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }
        public async Task<IEnumerable<Post>> GetUserPostsAndCommentsAsync(string userId)
        {
            var userPosts = await _context.Posts.Where(p => p.UserId == userId).ToListAsync();

            foreach (Post post in userPosts)
            {
                var postComments = await _commentsService.GetAllCommentsData(post.Id.ToString());

                post.Comments = postComments;
            }

            return userPosts;
        }
    }
}