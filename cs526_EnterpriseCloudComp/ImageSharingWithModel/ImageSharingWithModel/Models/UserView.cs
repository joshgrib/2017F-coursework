using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;

namespace ImageSharingWithModel.Models
{
    public class UserView
    {
        [Required]
        [RegularExpression(@"[a-zA-Z0-9_]+")]
        public string UserID;

        [Required]
        public bool ADA;
    }
}