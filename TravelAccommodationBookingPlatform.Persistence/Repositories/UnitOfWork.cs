using MediatR;
using TravelAccommodationBookingPlatform.Application.Interfaces;
using TravelAccommodationBookingPlatform.Application.Shared.Notifications;

namespace TravelAccommodationBookingPlatform.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private readonly IMediator _mediator;

    public UnitOfWork(AppDbContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task CommitAsync(CancellationToken cancellationToken)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            await _mediator.Publish(new TransactionStartNotification(), cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            await _mediator.Publish(new TransactionSuccessNotification(), cancellationToken);
        }
        catch (Exception)
        {
            await transaction.RollbackAsync(cancellationToken);
            await _mediator.Publish(new TransactionFailureNotification(), cancellationToken);

            throw;
        }
        finally
        {
            await _mediator.Publish(new TransactionCleanupNotification(), cancellationToken);
        }
    }
}