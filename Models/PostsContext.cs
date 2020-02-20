using System;
using Microsoft.EntityFrameworkCore;

namespace webapi_DependenInject.Models
{
    public class Posts
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Tags { get; set; }
        public bool Status { get; set; }
        public DateTime Create_Time { get; set; }
        public DateTime Update_Time { get; set; }
    }
}