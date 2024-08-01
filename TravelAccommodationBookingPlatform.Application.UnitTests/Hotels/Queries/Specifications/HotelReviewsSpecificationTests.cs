using FluentAssertions;
using TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelReviews;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;
using TravelAccommodationBookingPlatform.TestsCommon.Attributes;

namespace TravelAccommodationBookingPlatform.Application.UnitTests.Hotels.Queries.Specifications;

public class HotelReviewsSpecificationTests
{
    [Theory, AutoMoqData(omitOnRecursion: true)]
    public void HotelReviewsSpecification_CreatesCorrectQuery(
        HotelReviewsQuery query,
        List<Review> reviews,
        Hotel hotel)
    {
        hotel.Id = query.Id;
        hotel.Reviews = reviews;
        query.PaginationParameters.PageNumber = 1;
        query.PaginationParameters.PageSize = reviews.Count;
        var expectedReviews = reviews.OrderBy(r => r.Text).ToList();

        var spec = new HotelReviewsSpecification(query);
        var result = spec.Evaluate(new List<Hotel> { hotel }.AsQueryable()).ToList();

        result.Should().ContainSingle();
        result.First().Id.Should().Be(query.Id);
        result.First().Results.TotalCount.Should().Be(reviews.Count);
        result.First().Results.Items.Should().BeEquivalentTo(expectedReviews);
    }

    [Theory, AutoMoqData(omitOnRecursion: true)]
    public void HotelReviewsSpecification_PaginationWorksCorrectly(
        HotelReviewsQuery query,
        List<Review> reviews,
        Hotel hotel)
    {
        hotel.Id = query.Id;
        hotel.Reviews = reviews;
        query.PaginationParameters.PageNumber = 2;
        query.PaginationParameters.PageSize = 2;
        var expectedReviews = reviews
            .OrderBy(r => r.Text)
            .Skip((query.PaginationParameters.PageNumber - 1) * query.PaginationParameters.PageSize)
            .Take(query.PaginationParameters.PageSize)
            .ToList();

        var spec = new HotelReviewsSpecification(query);
        var result = spec.Evaluate(new List<Hotel> { hotel }.AsQueryable()).ToList();

        result.Should().ContainSingle();
        result.First().Id.Should().Be(query.Id);
        result.First().Results.TotalCount.Should().Be(reviews.Count);
        result.First().Results.Items.Should().BeEquivalentTo(expectedReviews);
    }
}