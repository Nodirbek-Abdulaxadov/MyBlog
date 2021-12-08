using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBlog.Models
{
    [Table("Postlar")]
    public class Post
    {
        [Required]
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Body { get; set; }
        [Required]
        public DateTime CreatedTime { get; set; }
        [Required]
        public string ImageFileName { get; set; }
    }
}