using FluentAssertions;
using TravelAccommodationBookingPlatform.Application.Cities.Queries.CitySearch.SpecificationExtensions;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.TestsCommon.Attributes;

namespace TravelAccommodationBookingPlatform.Application.UnitTests.Cities.Queries.Specifications;

public class CitySearchSpecificationExtensionsTests
{
    [Theory, AutoMoqData(omitOnRecursion: true)]
    public void SortByVisitsDescending_ShouldSortCitiesCorrectly(
        City cityWithNoVisits,
        City cityWithOneVisit,
        City cityWithManyVisits,
        Hotel hotel,
        Room room,
        Booking booking)
    {
        // Arrange
        cityWithNoVisits.Hotels = [];
        room.Bookings = [booking];
        hotel.Rooms = [room];
        cityWithOneVisit.Hotels = [hotel];

        var cities = new List<City> { cityWithNoVisits, cityWithOneVisit, cityWithManyVisits };
        var spec = new TestSpec<City>(q => q.SortByVisitsDescending());

        // Act
        var result = spec.Evaluate(cities.AsQueryable()).ToList();

        // Assert
        result.Should().ContainInOrder(
            cityWithManyVisits,
            cityWithOneVisit,
            cityWithNoVisits);
    }
}