using FluentValidation;
using TravelAccommodationBookingPlatform.Application.Validators;

namespace TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelRooms;

public class HotelRoomsQueryValidator : AbstractValidator<HotelRoomsQuery>
{
    public HotelRoomsQueryValidator()
    {
        RuleFor(q => q.PaginationParameters)
            .SetValidator(new PaginationParametersValidator());
    }
}