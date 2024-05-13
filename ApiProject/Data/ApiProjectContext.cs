using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ApiProject.Model;

namespace ApiProject.Data
{
    public class ApiProjectContext : DbContext
    {
        public ApiProjectContext (DbContextOptions<ApiProjectContext> options)
            : base(options)
        {
        }

        public DbSet<ApiProject.Model.Usuario> Usuario { get; set; } = default!;
    }
}
