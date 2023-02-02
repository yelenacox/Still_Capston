using System;
using System.ComponentModel.DataAnnotations;

namespace Still.Models
{
    public class Picture
    {
        public int Id { get; set; }

        [Required]
        public int UserProfileId { get; set; }
        public string Description { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        [Required]
        public string PictureLocation { get; set; }
    }
}
