using FluentAssertions;
using TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelImages;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;
using TravelAccommodationBookingPlatform.TestsCommon.Attributes;

namespace TravelAccommodationBookingPlatform.Application.UnitTests.Hotels.Queries.Specifications;

public class HotelImagesSpecificationTests
{
    [Theory, AutoMoqData(omitOnRecursion: true)]
    public void HotelImagesSpecification_CreatesCorrectQuery(
        HotelImagesQuery query,
        List<Image> images,
        Hotel hotel)
    {
        hotel.Id = query.Id;
        hotel.Images = images;
        query.PaginationParameters.PageNumber = 1;
        query.PaginationParameters.PageSize = images.Count;
        var expectedImages = images.OrderBy(i => i.Url).ToList();

        var spec = new HotelImagesSpecification(query);
        var result = spec.Evaluate(new List<Hotel> { hotel }.AsQueryable()).ToList();

        result.Should().ContainSingle();
        result.First().Id.Should().Be(query.Id);
        result.First().TotalCount.Should().Be(images.Count);
        result.First().Items.Should().BeEquivalentTo(expectedImages);
    }

    [Theory, AutoMoqData(omitOnRecursion: true)]
    public void HotelImagesSpecification_PaginationWorksCorrectly(
        HotelImagesQuery query,
        List<Image> images,
        Hotel hotel)
    {
        hotel.Id = query.Id;
        hotel.Images = images;
        query.PaginationParameters.PageNumber = 2;
        query.PaginationParameters.PageSize = 2;
        var expectedImages = images
            .OrderBy(i => i.Url)
            .Skip((query.PaginationParameters.PageNumber - 1) * query.PaginationParameters.PageSize)
            .Take(query.PaginationParameters.PageSize)
            .ToList();

        var spec = new HotelImagesSpecification(query);
        var result = spec.Evaluate(new List<Hotel> { hotel }.AsQueryable()).ToList();

        result.Should().ContainSingle();
        result.First().Id.Should().Be(query.Id);
        result.First().TotalCount.Should().Be(images.Count);
        result.First().Items.Should().BeEquivalentTo(expectedImages);
    }
}