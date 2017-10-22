using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;

namespace ImageSharingWithModel.Models
{
    public class ImageView
        /*
         * View model for an image
         */
    {
        public int Id;
        [Required]
        [StringLength(40)]
        public String Caption { get; set; }
        [Required]
        public int TagId { get; set; }
        [Required]
        [StringLength(200)]
        public String Description { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime DateTaken { get; set; }
        public String UserId { get; set; }

        public ImageView() { }
    }
}