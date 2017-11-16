using ImageSharingWithAuth.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ImageSharingWithAuth.DAL
{
    public class ImageSharingDBInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext db)
        {
            //db.Users.Add(new User { userId = "jfk", ADA = false });
            //db.Users.Add(new User { userId = "nixon", ADA = false });

            RoleStore<IdentityRole> roleStore = new RoleStore<IdentityRole>(db);
            UserStore<ApplicationUser> userStore = new UserStore<ApplicationUser>(db);

            RoleManager<IdentityRole> rm = new RoleManager<IdentityRole>(roleStore);
            UserManager<ApplicationUser> um = new UserManager<ApplicationUser>(userStore);

            IdentityResult ir;

            ApplicationUser nobody = createUser("nobody@example.org");
            ApplicationUser jfk = createUser("jfk@example.org");
            ApplicationUser nixon = createUser("nixon@example.org");

            ir = um.Create(nobody, "nobody1234");
            //nobody = um.FindByName(nobody.UserName);
            ir = um.Create(jfk, "jfk1234");
            //jfk = um.FindByName(jfk.UserName);
            ir = um.Create(nixon, "nixon1234");
            //nixon = um.FindByName(nixon.UserName);

            rm.Create(new IdentityRole("User"));
            if (!um.IsInRole(nobody.Id, "User"))
            {
                um.AddToRole(nobody.Id, "User");
            }
            if (!um.IsInRole(jfk.Id, "User"))
            {
                um.AddToRole(jfk.Id, "User");
            }
            if (!um.IsInRole(nixon.Id, "User"))
            {
                um.AddToRole(nixon.Id, "User");
            }

            rm.Create(new IdentityRole("Admin"));
            if (!um.IsInRole(nixon.Id, "Admin"))
            {
                um.AddToRole(nixon.Id, "Admin");
            }

            rm.Create(new IdentityRole("Approver"));
            if (!um.IsInRole(jfk.Id, "Approver"))
            {
                um.AddToRole(jfk.Id, "Approver");
            }

            db.Tags.Add(new Tag { Name = "portrait" });
            db.Tags.Add(new Tag { Name = "architecture" });
            db.SaveChanges();

            db.Images.Add(new Image
            {
                Caption = "Ingrid Bergman",
                Description = "Actor from Casablanca.",
                Date = new DateTime(1946, 12, 14),
                UserID = jfk.Id,
                TagID = 1,
                Approved = true
            });
            db.SaveChanges();

            base.Seed(db);

        }

        private ApplicationUser createUser(string userName)
        {
            return new ApplicationUser { UserName = userName, Email = userName, ADA = false, Active = true };
        }
    }
}