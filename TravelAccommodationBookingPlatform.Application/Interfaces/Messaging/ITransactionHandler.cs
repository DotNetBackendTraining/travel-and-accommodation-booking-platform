using MediatR;
using TravelAccommodationBookingPlatform.Application.Shared.Notifications;

namespace TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;

/// <summary>
/// Implement this when you want to handle some logic within a unit of work transaction.
/// </summary>
public interface ITransactionHandler :
    INotificationHandler<TransactionStartNotification>,
    INotificationHandler<TransactionSuccessNotification>,
    INotificationHandler<TransactionFailureNotification>,
    INotificationHandler<TransactionCleanupNotification>;