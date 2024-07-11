// // using System;
// // using System.Collections.Generic;
// // using System.Linq;
// // using System.Threading.Tasks;
// // using Microsoft.EntityFrameworkCore;
// // using ShopProject.Models;
// //
// // namespace ShopProject.Data
// // {
// //     public class ShopProjectContext : DbContext
// //     {
// //         public ShopProjectContext (DbContextOptions<ShopProjectContext> options)
// //             : base(options)
// //         {
// //         }
// //
// //         protected override void OnModelCreating(ModelBuilder modelBuilder)
// //         {
// //             base.OnModelCreating(modelBuilder);
// //             
// //             // Add global query filter for soft delete
// //             modelBuilder.Entity<Field>().HasQueryFilter(f => !f.IsDeleted);
// //             modelBuilder.Entity<Category>().HasData(
// //                 new Category
// //                 {
// //                     Id = 1,
// //                     Name = "پوشیدنی",
// //                     ParentId = null
// //                 },
// //                 new Category()
// //                 {
// //                     Id = 3,
// //                     Name = "لوازم خانگی",
// //                     ParentId = null
// //                 }
// //                 );
// //             modelBuilder.Entity<Product>().HasData(
// //                 new Product
// //                 {
// //                     Id = 1,
// //                     Name = "ساعت",
// //                     Price = 2000M,
// //                     CategoryId = 1
// //                 },
// //                 new Product
// //                     {
// //                         Id = 2,
// //                         Name = "شلوار کتان",
// //                         Price = 100M,
// //                         CategoryId = 1
// //                     }
// //                 );
// //             modelBuilder.Entity<Field>().HasData(
// //                 new Field
// //                 {
// //                     Id = 1,
// //                     Name = "رنگ",
// //                     CategoryId = 1
// //                 },
// //                 new Field
// //                 {
// //                     Id = 2,
// //                     Name = "سایز",
// //                     CategoryId = 1
// //                 }
// //                 );
// //             modelBuilder.Entity<FieldValue>().HasData(
// //             
// //                 new FieldValue
// //                 {
// //                     Id = 1,
// //                     ProductId = 2,
// //                     FieldId = 1,
// //                     Value = "38"
// //                 }
// //             );
// //         }
// //         protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
// //         {
// //             optionsBuilder.AddInterceptors(new SoftDeleteInterceptor());
// //             base.OnConfiguring(optionsBuilder);
// //         }
// //         public DbSet<Product> Product { get; set; } = default!;
// //         public DbSet<Category> Category { get; set; } = default!;
// //         public DbSet<Field> Field { get; set; }
// //     }
// // }

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopProject.Models;
using ShopProject.Data.Infrastructure;

namespace ShopProject.Data
{
    public class ShopProjectContext : DbContext
    {
        public ShopProjectContext(DbContextOptions<ShopProjectContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Add global query filter for soft delete
            modelBuilder.Entity<Field>().HasQueryFilter(f => !f.IsDeleted);

            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    Name = "پوشیدنی",
                    ParentId = null
                },
                new Category()
                {
                    Id = 3,
                    Name = "لوازم خانگی",
                    ParentId = null
                }
            );
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "ساعت",
                    Price = 2000M,
                    CategoryId = 1
                },
                new Product
                {
                    Id = 2,
                    Name = "شلوار کتان",
                    Price = 100M,
                    CategoryId = 1
                }
            );
            modelBuilder.Entity<Field>().HasData(
                new Field
                {
                    Id = 1,
                    Name = "رنگ",
                    CategoryId = 1
                },
                new Field
                {
                    Id = 2,
                    Name = "سایز",
                    CategoryId = 1
                }
            );
            modelBuilder.Entity<FieldValue>().HasData(
                new FieldValue
                {
                    Id = 1,
                    ProductId = 2,
                    FieldId = 1,
                    Value = "38"
                }
            );
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(new SoftDeleteInterceptor());
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Product> Product { get; set; } = default!;
        public DbSet<Category> Category { get; set; } = default!;
        public DbSet<Field> Field { get; set; }
    }
}
