using FluentAssertions;
using TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelSearch.Specifications.Extensions;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.TestsCommon.Attributes;

namespace TravelAccommodationBookingPlatform.Application.UnitTests.Hotels.Queries.Specifications.HotelSearch.Extensions;

public class ApplySearchTermTests
{
    [Theory, AutoMoqData(omitOnRecursion: true)]
    public void ApplySearchTerm_ShouldFilterCorrectly_WhenNameMatches(
        string searchTerm,
        Hotel hotel)
    {
        // Arrange
        hotel.Name = searchTerm;

        // Act
        var spec = new TestSpec<Hotel>(q => q.ApplySearchTermFilter(searchTerm));
        var result = spec.Evaluate(new List<Hotel> { hotel }.AsQueryable()).ToList();

        // Assert
        result.Should().ContainSingle();
        result.First().Name.Should().Be(hotel.Name);
    }

    [Theory, AutoMoqData(omitOnRecursion: true)]
    public void ApplySearchTerm_ShouldFilterCorrectly_WhenCityNameMatches(
        string searchTerm,
        Hotel hotel)
    {
        // Arrange
        hotel.City = new City { Name = searchTerm };

        // Act
        var spec = new TestSpec<Hotel>(q => q.ApplySearchTermFilter(searchTerm));
        var result = spec.Evaluate(new List<Hotel> { hotel }.AsQueryable()).ToList();

        // Assert
        result.Should().ContainSingle();
        result.First().City.Name.Should().Be(hotel.City.Name);
    }

    [Theory, AutoMoqData(omitOnRecursion: true)]
    public void ApplySearchTerm_ShouldFilterCorrectly_WhenNoMatch(
        Hotel hotel)
    {
        // Arrange
        const string searchTerm = "NonExistentSearchTerm";

        // Act
        var spec = new TestSpec<Hotel>(q => q.ApplySearchTermFilter(searchTerm));
        var result = spec.Evaluate(new List<Hotel> { hotel }.AsQueryable()).ToList();

        // Assert
        result.Should().BeEmpty();
    }
}