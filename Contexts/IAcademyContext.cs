using CSharpAcademyBot.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpAcademyBot.Contexts
{
    public interface IAcademyContext : IDisposable
    {
        public DbSet<User> Users { get; set; }

        int SaveChanges();
    }
}
