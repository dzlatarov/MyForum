﻿using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyForum.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyForum.Persistence
{
    public class MyForumDbContext : IdentityDbContext<ApplicationUser>
    {
        public MyForumDbContext(DbContextOptions<MyForumDbContext> options) 
            : base(options)
        {
        }
        
        public DbSet<Thread> Threads { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {           
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Thread>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<Thread>()
                .HasMany(t => t.Comments)
                .WithOne(c => c.Thread)
                .HasForeignKey(t => t.ThreadId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Category>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Category>()
                .HasMany(c => c.Threads)
                .WithOne(t => t.Category)
                .HasForeignKey(t => t.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}