using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ImageSharingWithAuth.Models
{
    public class UserView
    {
        [Required]
        [RegularExpression(@"[a-zA-Z0-9_]+")]
        public string Userid { get; set; }

        [Required]
        public bool ADA { get; set; }
    }
}