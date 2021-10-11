using System;
using Microsoft.EntityFrameworkCore;
using VoyageExcercise.DAL;
using VoyageExcercise.DAL.Models;

namespace VoyegApiTest
{
    public class TestDataSeeder
    {

        protected  DbContextOptions<AppDBContext> _options;
        protected AppDBContext _context;

        protected TestDataSeeder(DbContextOptions<AppDBContext> options)
        {
            _options = options;
            SeedServices();
        }

        public void SeedServices()
        {
            _context = new AppDBContext(_options);
            
            //_context.Database.EnsureDeleted();
            //_context.Database.EnsureCreated();
            
        }
    }
}
