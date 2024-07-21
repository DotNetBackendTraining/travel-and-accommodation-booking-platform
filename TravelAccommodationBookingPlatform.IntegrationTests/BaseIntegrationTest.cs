using MediatR;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using TravelAccommodationBookingPlatform.Persistence;

namespace TravelAccommodationBookingPlatform.IntegrationTests;

public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>, IDisposable
{
    private readonly IServiceScope _scope;
    protected readonly ISender Sender;
    protected readonly AppDbContext DbContext;
    private IDbContextTransaction? _transaction;

    protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
    {
        _scope = factory.Services.CreateScope();
        Sender = _scope.ServiceProvider.GetRequiredService<ISender>();
        DbContext = _scope.ServiceProvider.GetRequiredService<AppDbContext>();
        BeginTransaction();
    }

    private void BeginTransaction()
    {
        _transaction = DbContext.Database.BeginTransaction();
    }

    public void Dispose()
    {
        RollbackTransaction();
        _scope.Dispose();
    }

    private void RollbackTransaction()
    {
        _transaction?.Rollback();
        _transaction?.Dispose();
    }
}