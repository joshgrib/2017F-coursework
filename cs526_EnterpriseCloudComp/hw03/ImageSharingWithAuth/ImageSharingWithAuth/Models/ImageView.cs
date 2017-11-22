using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ImageSharingWithAuth.Models
{
    public class ImageView
    {

        [Required]
        [StringLength(40)]
        public string Caption { get; set; }
        [Required]
        public int TagId { get; set; }
        [Required]
        [StringLength(200)]
        public string Description { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "0:d", ApplyFormatInEditMode=true)]
        public DateTime DateTaken { get; set; }


        [ScaffoldColumn(false)]
        public int Id { get; set; }
        [ScaffoldColumn(false)]
        public string TagName { get; set; }
        [ScaffoldColumn(false)]
        public string Userid { get; set; }

        public ImageView()
        {

        }
    }
}