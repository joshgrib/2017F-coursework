using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.Entity;
using ImageSharingWithModel.Models;

namespace ImageSharingWithModel.DAL
{
    public class ImageSharingDBInitializer : DropCreateDatabaseAlways<ImageSharingDB>
    {
        protected override void Seed(ImageSharingDB db)
        {
            db.Users.Add(new User { UserId = "jfk", ADA = false });
            db.Users.Add(new User { UserId = "nixon", ADA = false });

            db.Tags.Add(new Tag { Name = "portrait" });
            db.Tags.Add(new Tag { Name = "architecture" });
            db.SaveChanges();

            db.Images.Add(new Image
            {
                Caption = "Ingrid Bergman",
                Description = "Best remembered for her role in Casablanca, "
                + "even though she considered some of her "
                + "other films to be mroe important",
                DateTaken = new DateTime(1946, 12, 14),
                UserId = 1,
                TagId = 1
            });
            db.SaveChanges();

            base.Seed(db);
        }
    }
}