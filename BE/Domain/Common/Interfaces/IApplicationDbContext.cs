using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain.Common.Interfaces
{
    public interface IApplicationDbContext
    {

        DbSet<Contact> Contacts { get; }


        DbSet<Course> Courses { get; }

        DbSet<Language> Languages { get; }

        DbSet<DeliveryMethod> DeliveryMethods { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
