using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using MyBlog.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using MyBlog.DB_Context;

namespace MyBlog.Services
{
    public class PostService : IPostService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly AppDbContext _dbContext;
        public PostService(IWebHostEnvironment webHostEnvironment, AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;
        }

        public Post AddPost(Post newPost)
        {
            newPost.Id = Guid.NewGuid();
            newPost.CreatedTime = DateTime.Now;
            _dbContext.Posts.Add(newPost);
            _dbContext.SaveChanges();

            return newPost;
        }

        public void DeletePost(Guid id)
        {
            Post post = _dbContext.Posts.FirstOrDefault(p => p.Id == id);
            if (post.ImageFileName is not null)
            {
                string uplodFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
                string filePath = Path.Combine(uplodFolder, post.ImageFileName);
                FileInfo fileInfo = new FileInfo(filePath);
                if(fileInfo.Exists)
                {
                    fileInfo.Delete();
                }
            }

            _dbContext.Posts.Remove(post);
            _dbContext.SaveChanges();
        }

        public List<Post> GetAllPosts()
        {
            return _dbContext.Posts.ToList();
        }

        public Post GetById(Guid id)
        {
            return _dbContext.Posts.FirstOrDefault(post => post.Id == id);
        }

        public string SaveImage(IFormFile newFile)
        {
            string uniqueName = string.Empty;
            if (newFile.FileName != null)
            {
                string uplodFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
                uniqueName = Guid.NewGuid().ToString() + "_" + newFile.FileName;
                string filePath = Path.Combine(uplodFolder, uniqueName);
                FileStream fileStream = new FileStream(filePath, FileMode.Create);
                newFile.CopyTo(fileStream);
                fileStream.Close();
            }

            return uniqueName;
        }

        public Post UpdatePost(Post post)
        {
            _dbContext.Posts.Update(post);
            _dbContext.SaveChanges();

            return post;
        }
    }
}