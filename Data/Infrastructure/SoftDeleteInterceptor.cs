// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Diagnostics;
// using System.Linq;
// using System.Threading;
// using System.Threading.Tasks;
// using ShopProject.Models;
//
// namespace ShopProject.Infrastructure
// {
//     public class SoftDeleteInterceptor : SaveChangesInterceptor
//     {
//         public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
//         {
//             SoftDeleteEntities(eventData.Context);
//             return base.SavingChanges(eventData, result);
//         }
//
//         public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
//         {
//             SoftDeleteEntities(eventData.Context);
//             return base.SavingChangesAsync(eventData, result, cancellationToken);
//         }
//
//         private void SoftDeleteEntities(DbContext context)
//         {
//             foreach (var entry in context.ChangeTracker.Entries().Where(e => e.State == EntityState.Deleted))
//             {
//                 if (entry.Entity is Field)
//                 {
//                     entry.State = EntityState.Modified;
//                     entry.CurrentValues["IsDeleted"] = true;
//                 }
//             }
//         }
//     }
// }
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ShopProject.Models;

namespace ShopProject.Data.Infrastructure
{
    public class SoftDeleteInterceptor : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            SoftDeleteEntities(eventData.Context);
            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            SoftDeleteEntities(eventData.Context);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private void SoftDeleteEntities(DbContext context)
        {
            foreach (var entry in context.ChangeTracker.Entries().Where(e => e.State == EntityState.Deleted))
            {
                if (entry.Entity is Field)
                {
                    entry.State = EntityState.Modified;
                    entry.CurrentValues["IsDeleted"] = true;
                }
            }
        }
    }
}
