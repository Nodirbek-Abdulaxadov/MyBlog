using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using MyBlog.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace MyBlog.Services
{
    public class PostService : IPostService
    {
        List<Post> allPosts;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public PostService(IWebHostEnvironment webHostEnvironment)
        {
            allPosts = new List<Post>()
            {
                new Post 
                {
                    Id = Guid.Parse("0557b821-c20b-4c9f-8560-553c9fe558ad"),
                    Title = "My First Post",
                    Body = "Some words",
                    CreatedTime = DateTime.Now,
                    ImageFileName = "3b247caa-54e7-4349-97a7-7d2b4b73bc8a_Frame 2.jpg"
                },
                new Post 
                {
                    Id = Guid.Parse("19da52ec-e28c-4ce6-bcef-851b1bef3b17"),
                    Title = "\"Roadmap kerak\" loyihasi",
                    Body = "Assalomu alaykum barchaga do'stlar! Dotnet Academy jamoasi yangi \"Roadmap kerak\" loyihasini ishga tushurish arafasida. Xabaringiz bor, .NET texnologiyasini biladigan insonlarga talab O'zbekistonda ortib bormoqda. Texnologiyani rivoji uchun oz bo'lsada xissa qo'shish maqsadida, hozirda akademiyamizda \"foundation\" va \"chuqurlashtirilgan\" kurslar tashkil qilingan. Lekin hamma ham bu kurslarda o'qishga imkon topa olmayabdi. Shuni xisobga olgan holda, yangi bir loyihaga qo'l urmoqchimiz. Loyiha haqida qisqacha: Albatta har qanday ilmni puxta egallanishi uchun, ustoz kerak bo'ladi. Chunki o'zingiz bormoqchi bo'lgan joyni aniq va ravshan ko'ra olsangiz, unga erishish ham tezlashib boradi. Dasturlashni kurslarga qatnamasdan ham o'rgansa bo'ladi, lekin bu sizga ilk qadamlarda biroz qiyin bo'ladi. Internet olamida, duch kelishingiz mumkin bo'lgan aksariyat savollarga javob bor. Hammasidan to'g'ri foydalana olsangiz bo'lgani. Biz ishlab chiqqan loyiha, yo'lingiz aniq bo'lishligi uchun xizmat qiladi. ",
                    CreatedTime = DateTime.Now,
                    ImageFileName = "51508511-ed7f-433b-8ea0-cc78d4711d33_b.png"
                }
            };

            _webHostEnvironment = webHostEnvironment;
        }

        public Post AddPost(Post newPost)
        {
            newPost.Id = Guid.NewGuid();
            newPost.CreatedTime = DateTime.Now;
            allPosts.Add(newPost);

            return newPost;
        }

        public void DeletePost(Guid id)
        {
            Post post = allPosts.FirstOrDefault(p => p.Id == id);
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

            allPosts.Remove(post);
        }

        public List<Post> GetAllPosts()
        {
            return allPosts;
        }

        public Post GetById(Guid id)
        {
            return allPosts.FirstOrDefault(post => post.Id == id);
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
            foreach(var p in allPosts)
            {
                if (p.Id == post.Id)
                {
                    p.Title = post.Title;
                    p.Body = post.Body;
                    p.ImageFileName = post.ImageFileName;
                }
            }

            return post;
        }
    }
}