using MediatR;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using TravelAccommodationBookingPlatform.Persistence;

namespace TravelAccommodationBookingPlatform.IntegrationTests.Shared;

public abstract class BaseIntegrationTest : IDisposable
{
    protected readonly IServiceScope Scope;
    protected readonly ISender Sender;
    protected readonly AppDbContext DbContext;
    private IDbContextTransaction? _transaction;

    protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
    {
        Scope = factory.Services.CreateScope();
        Sender = Scope.ServiceProvider.GetRequiredService<ISender>();
        DbContext = Scope.ServiceProvider.GetRequiredService<AppDbContext>();
        BeginTransaction();
    }

    private void BeginTransaction()
    {
        _transaction = DbContext.Database.BeginTransaction();
    }

    public void Dispose()
    {
        RollbackTransaction();
        Scope.Dispose();
    }

    private void RollbackTransaction()
    {
        _transaction?.Rollback();
        _transaction?.Dispose();
    }
}