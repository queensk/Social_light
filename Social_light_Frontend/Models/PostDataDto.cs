using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Social_light_Frontend.Models
{
    public class PostDataDto
    {
        public string ProfileImageUrl { get; set; }
        public string OwnerName { get; set; }
        public string ImageUrl { get; set; }
        public string Text { get; set; }
    }

    public class PostMetadataDto
    {
        // Add any metadata properties you need
        public bool IsLiked { get; set; }
        public int LikeCount { get; set; }
        public List<CommentModel> Comments { get; set; } = new List<CommentModel>();
    }

    public class CommentModel
    {
        public string ProfileImageUrl { get; set; }
        public string Text { get; set; }
    }

}