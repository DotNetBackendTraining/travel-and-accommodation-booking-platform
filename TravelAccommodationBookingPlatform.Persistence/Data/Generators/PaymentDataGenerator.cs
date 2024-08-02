using Bogus;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.Enums;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Persistence.Data.Generators;

internal static class PaymentDataGenerator
{
    public static List<Payment> GeneratePayments(IReadOnlyList<Booking> bookingsWithRooms)
    {
        var faker = new Faker();
        var payments = new List<Payment>();

        foreach (var booking in bookingsWithRooms)
        {
            // Most bookings have payment
            if (faker.Random.Bool(0.8f))
            {
                var payment = new Payment
                {
                    BookingId = booking.Id,
                    ConfirmationNumber = faker.Random.AlphaNumeric(10),
                    Status = PaymentStatus.Pending,
                    PersonalInformation = new PersonalInformation
                    {
                        FullName = faker.Name.FullName(),
                        PhoneNumber = faker.Phone.PhoneNumber("###-###-####")
                    }
                };

                // Most payments received the hotel active discount and are completed
                if (faker.Random.Bool(0.8f))
                {
                    payment.Status = PaymentStatus.Completed;
                    payment.AppliedDiscountId = booking.Rooms.First().Hotel.ActiveDiscountId;
                }

                payments.Add(payment);
                booking.Payment = payment;
            }
        }

        return payments;
    }
}