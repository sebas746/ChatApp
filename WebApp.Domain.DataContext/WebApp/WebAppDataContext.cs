using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Domain.DataContext.WebApp
{
    public partial class WebAppDataContext : DbContext
    {
        public WebAppDataContext()
            : base("name=WebAppDataContext")
        {
        }

        public virtual DbSet<Users> Users { get; set; }

        public virtual DbSet<Stock> Stock { get; set; }
        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>()
               .Property(e => e.FullName)
               .IsUnicode(false);
        }
    }
}
