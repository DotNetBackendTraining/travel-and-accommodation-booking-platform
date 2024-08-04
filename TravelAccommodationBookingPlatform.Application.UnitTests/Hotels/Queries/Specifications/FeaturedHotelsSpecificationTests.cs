using AutoMapper;
using FluentAssertions;
using TravelAccommodationBookingPlatform.Application.Hotels.Queries.FeaturedHotels;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.TestsCommon.Attributes;

namespace TravelAccommodationBookingPlatform.Application.UnitTests.Hotels.Queries.Specifications;

public class FeaturedHotelsSpecificationTests
{
    private readonly IMapper _mapper;

    public FeaturedHotelsSpecificationTests()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile(new FeaturedHotelsProfile()));
        _mapper = config.CreateMapper();
    }

    [Theory, AutoMoqData(omitOnRecursion: true)]
    public void FeaturedHotelsSpecification_CalculatesPricesCorrectly(
        List<Hotel> hotels)
    {
        // Arrange
        var spec = new FeaturedHotelsSpecification(_mapper);

        // Act
        var result = spec.Evaluate(hotels.AsQueryable()).ToList();

        foreach (var deal in result)
        {
            var hotel = hotels.First(h => h.Id == deal.Hotel.Id);
            var minPrice = hotel.Rooms.Min(r => r.Price.Value);
            var maxPrice = hotel.Rooms.Max(r => r.Price.Value);
            var discountRate = hotel.ActiveDiscount?.Rate.Percentage ?? 0;
            var expectedSummary = _mapper.Map<FeaturedHotelsResponse.FeaturedHotelSummary>(hotel);

            // Assert
            deal.MinimumPriceDeal.OriginalPrice.Value.Should().Be(minPrice);
            deal.MinimumPriceDeal.DiscountRate.Percentage.Should().Be(discountRate);
            deal.MaximumPriceDeal.OriginalPrice.Value.Should().Be(maxPrice);
            deal.MaximumPriceDeal.DiscountRate.Percentage.Should().Be(discountRate);
            deal.Hotel.Should().BeEquivalentTo(expectedSummary, options => options.ExcludingMissingMembers());
        }
    }

    [Theory, AutoMoqData(omitOnRecursion: true)]
    public void FeaturedHotelsSpecification_OrdersResultsCorrectly(
        List<Hotel> hotels)
    {
        // Arrange
        var spec = new FeaturedHotelsSpecification(_mapper);

        // Act
        var result = spec.Evaluate(hotels.AsQueryable()).ToList();

        // Assert
        // Ordered by discount rates
        var discountRates = result.Select(deal => deal.MinimumPriceDeal.DiscountRate.Percentage).ToList();
        discountRates.Should().BeInDescendingOrder();

        // If similar discount rate, then by star rate
        var groupedByDiscount = result
            .GroupBy(deal => deal.MinimumPriceDeal.DiscountRate.Percentage)
            .Where(group => group.Count() > 1)
            .ToList();

        foreach (var group in groupedByDiscount)
        {
            var starRatesInGroup = group.Select(deal => deal.Hotel.StarRate).ToList();
            starRatesInGroup.Should().BeInDescendingOrder();
        }
    }
}