using FluentAssertions;
using TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelSearch;
using TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelSearch.Specifications.Extensions;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.Enums;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;
using TravelAccommodationBookingPlatform.TestsCommon.Attributes;

namespace TravelAccommodationBookingPlatform.Application.UnitTests.Hotels.Queries.Specifications.HotelSearch.Extensions;

public class ApplyHotelSortingOptionTests
{
    [Theory, AutoMoqData(omitOnRecursion: true)]
    public void ApplyHotelSortingOption_ShouldSortByName(
        List<Hotel> hotels)
    {
        // Arrange
        var random = new Random();
        hotels.ForEach(h => h.Name = "Hotel " + random.Next(1000));

        // Act
        var spec = new TestSpec<Hotel>(q => q.ApplyHotelSortingOption(HotelSearchQuery.SortingOption.Name));
        var result = spec.Evaluate(hotels.AsQueryable()).ToList();

        // Assert
        result.Should().BeInAscendingOrder(h => h.Name);
    }

    [Theory, AutoMoqData(omitOnRecursion: true)]
    public void ApplyHotelSortingOption_ShouldFilterAndSortByFeatured(
        Hotel hotel1,
        Hotel hotel2,
        Hotel hotel3,
        Hotel hotel4,
        DiscountRate discountRate)
    {
        // Arrange
        var sortingOption = HotelSearchQuery.SortingOption.Featured;
        var hotels = new List<Hotel> { hotel1, hotel2, hotel3, hotel4 };

        // Assign discounts to some hotels
        hotel1.ActiveDiscount = new Discount
            { Rate = discountRate };
        hotel2.ActiveDiscount = new Discount
            { Rate = new DiscountRate { Percentage = discountRate.Percentage + 10 } };
        hotel3.ActiveDiscount = null; // This hotel has no discount
        hotel4.ActiveDiscount = new Discount
            { Rate = new DiscountRate { Percentage = discountRate.Percentage - 10 } };

        // Act
        var spec = new TestSpec<Hotel>(q => q.ApplyHotelSortingOption(sortingOption));
        var result = spec.Evaluate(hotels.AsQueryable()).ToList();

        // Assert
        result.Should().HaveCount(3);
        result.Should().NotContain(h => h.ActiveDiscount == null);
        result.Should().BeInDescendingOrder(h => h.ActiveDiscount!.Rate.Percentage);
    }

    [Theory, AutoMoqData(omitOnRecursion: true)]
    public void ApplyHotelSortingOption_ShouldSortByStarsThenName(
        List<Hotel> hotels)
    {
        // Arrange
        var random = new Random();
        hotels.ForEach(h =>
        {
            h.StarRate = (StarRate)random.Next(1, 5);
            h.Name = "Hotel " + random.Next(1000);
        });

        // Act
        var spec = new TestSpec<Hotel>(q => q.ApplyHotelSortingOption(HotelSearchQuery.SortingOption.StarsThenName));
        var result = spec.Evaluate(hotels.AsQueryable()).ToList();

        // Assert
        result.Should().BeInDescendingOrder(h => h.StarRate)
            .And.ThenBeInAscendingOrder(h => h.Name);
    }
}