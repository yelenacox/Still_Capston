using System.ComponentModel.DataAnnotations;
using System;

namespace Still.Models
{
    public class PagePicture
    {
        public int Id { get; set; }

        [Required]
        public int PageId { get; set; }

        [Required]
        public int PictureId { get; set; }

        public string Description { get; set; }
        public Picture Picture { get; set; }
    }
}
