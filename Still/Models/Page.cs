using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Permissions;

namespace Still.Models
{
    public class Page
    {
        public int Id { get; set; }

        [Required]
        public int UserProfileId { get; set; }

        [Required]
        public string Title { get; set; }
        public string Description { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }
    }
}
