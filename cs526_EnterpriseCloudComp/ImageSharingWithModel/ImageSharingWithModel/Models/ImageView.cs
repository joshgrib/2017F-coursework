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
        [Required]
        [RegularExpression(@"[a-zA-z0-9_]+")]
        public String ID { get; set; }
        [Required]
        [StringLength(40)]
        public String Caption { get; set; }
        [Required]
        [StringLength(200)]
        public String Description { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime DateTaken { get; set; }
        public String UserID { get; set; }

        public ImageView() { }
    }
}