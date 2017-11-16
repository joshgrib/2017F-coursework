﻿using ImageSharingWithAuth.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;



namespace ImageSharingWithAuth.DAL
{
    //public class ImageSharingDB : DbContext
    //{
    //    public DbSet<Image> Images { get; set; }
    //    public DbSet<User> Users { get; set; }
    //    public DbSet<Tag> Tags { get; set; }
    //}

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<Image> Images { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}