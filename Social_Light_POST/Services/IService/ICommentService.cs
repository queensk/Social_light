using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Social_Light_POST.Models;
using Social_Light_POST.Models.DTO;

namespace Social_Light_POST.Services.IService
{
    public interface ICommentService
    {
        Task<List<Comment>> GetAllCommentsData(string postId);
    }
}