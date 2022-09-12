﻿using Microsoft.EntityFrameworkCore;
using RestWithASPNET.Models.Entites;

namespace RestWithASPNET.Models.Context
{
    public class MySQLContext : DbContext 
    {
        public MySQLContext()
        {
        }
        public MySQLContext( DbContextOptions<MySQLContext> options) : base(options)
        {
        }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
