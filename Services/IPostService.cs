using System.Collections.Generic;
using MyBlog.Models;
using System;
using Microsoft.AspNetCore.Http;

namespace MyBlog.Services
{
    public interface IPostService
    {
        List<Post> GetAllPosts();
        Post GetById(Guid id);
        Post AddPost(Post newPost);
        Post UpdatePost(Post post);
        void DeletePost(Guid id);
        string SaveImage(IFormFile newFile);
    }
}