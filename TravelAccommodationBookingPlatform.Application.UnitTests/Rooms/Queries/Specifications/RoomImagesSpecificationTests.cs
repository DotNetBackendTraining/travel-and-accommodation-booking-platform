using FluentAssertions;
using TravelAccommodationBookingPlatform.Application.Rooms.Queries.RoomImages;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;
using TravelAccommodationBookingPlatform.TestsCommon.Attributes;

namespace TravelAccommodationBookingPlatform.Application.UnitTests.Rooms.Queries.Specifications;

public class RoomImagesSpecificationTests
{
    [Theory, AutoMoqData(omitOnRecursion: true)]
    public void RoomImagesSpecification_CreatesCorrectQuery(
        RoomImagesQuery query,
        List<Image> images,
        Room room)
    {
        room.Id = query.Id;
        room.Images = images;
        query.PaginationParameters.PageNumber = 1;
        query.PaginationParameters.PageSize = images.Count;
        var expectedImages = images.OrderBy(i => i.Url).ToList();

        var spec = new RoomImagesSpecification(query);
        var result = spec.Evaluate(new List<Room> { room }.AsQueryable()).ToList();

        result.Should().ContainSingle();
        result.First().Id.Should().Be(query.Id);
        result.First().TotalCount.Should().Be(images.Count);
        result.First().Items.Should().BeEquivalentTo(expectedImages);
    }

    [Theory, AutoMoqData(omitOnRecursion: true)]
    public void RoomImagesSpecification_PaginationWorksCorrectly(
        RoomImagesQuery query,
        List<Image> images,
        Room room)
    {
        room.Id = query.Id;
        room.Images = images;
        query.PaginationParameters.PageNumber = 2;
        query.PaginationParameters.PageSize = 2;
        var expectedImages = images
            .OrderBy(i => i.Url)
            .Skip((query.PaginationParameters.PageNumber - 1) * query.PaginationParameters.PageSize)
            .Take(query.PaginationParameters.PageSize)
            .ToList();

        var spec = new RoomImagesSpecification(query);
        var result = spec.Evaluate(new List<Room> { room }.AsQueryable()).ToList();

        result.Should().ContainSingle();
        result.First().Id.Should().Be(query.Id);
        result.First().TotalCount.Should().Be(images.Count);
        result.First().Items.Should().BeEquivalentTo(expectedImages);
    }
}