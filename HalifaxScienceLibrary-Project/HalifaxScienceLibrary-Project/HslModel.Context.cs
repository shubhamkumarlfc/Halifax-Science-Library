﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HalifaxScienceLibrary_Project
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class HSLEntities : DbContext
    {
        public HSLEntities()
            : base("name=HSLEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<book> books { get; set; }
        public virtual DbSet<employee> employees { get; set; }
        public virtual DbSet<item> items { get; set; }
        public virtual DbSet<monthly_expense> monthly_expense { get; set; }
        public virtual DbSet<mothlyexpense_employee> mothlyexpense_employee { get; set; }
        public virtual DbSet<rent> rents { get; set; }
        public virtual DbSet<buy_items> buy_items { get; set; }
        public virtual DbSet<author> authors { get; set; }
        public virtual DbSet<author_articles> author_articles { get; set; }
        public virtual DbSet<book_author> book_author { get; set; }
        public virtual DbSet<customer> customers { get; set; }
        public virtual DbSet<article> articles { get; set; }
        public virtual DbSet<magazine> magazines { get; set; }
        public virtual DbSet<volume> volumes { get; set; }
        public virtual DbSet<transaction> transactions { get; set; }
    }
}
