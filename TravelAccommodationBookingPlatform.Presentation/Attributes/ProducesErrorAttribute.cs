using Microsoft.AspNetCore.Mvc;
using TravelAccommodationBookingPlatform.Presentation.Shared;

namespace TravelAccommodationBookingPlatform.Presentation.Attributes;

public class ProducesErrorAttribute : ProducesResponseTypeAttribute
{
    public ProducesErrorAttribute(int statusCode) : base(typeof(ErrorResponse), statusCode)
    {
        if (statusCode < 400)
        {
            throw new InvalidOperationException($"{statusCode} is not an error status code");
        }
    }
}