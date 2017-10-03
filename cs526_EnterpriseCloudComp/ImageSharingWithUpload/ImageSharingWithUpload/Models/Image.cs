﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;

namespace ImageSharingWithUpload.Models
{
    public class Image
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
        public DateTime DateTaken { get; set; }
        public String UserID { get; set; }

        public Image() {}
    }
}