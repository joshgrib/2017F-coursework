using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImageSharingWithAuth.Models
{
    public class SelectItemView
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool Checked { get; set; }
        public SelectItemView(string id, string name, bool isChecked)
        {
            Id = id;
            Name = name;
            Checked = isChecked;
        }

        public SelectItemView()
        {

        }
    }
}