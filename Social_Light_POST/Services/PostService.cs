using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Social_Light_POST.Data;
using Social_Light_POST.Models;
using Social_Light_POST.Services.IService;

namespace Social_Light_POST.Services
{
    public class PostService : IPostService
    {
        private readonly AppDbContext _context;

        public PostService(AppDbContext context)
        {
            _context = context;
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

        public async Task<Post> GetPostByIdAsync(Guid id)
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
                _context.Posts.Update(post);
                await _context.SaveChangesAsync();
                return "Post Updated";
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }
    }
}