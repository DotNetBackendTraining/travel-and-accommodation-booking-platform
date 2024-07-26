using AutoMapper;
using FluentAssertions;
using TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelRooms;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.TestsCommon.Attributes;

namespace TravelAccommodationBookingPlatform.Application.UnitTests.Hotels.Queries.Specifications;

public class HotelRoomsSpecificationTests
{
    private readonly IMapper _mapper;

    public HotelRoomsSpecificationTests()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile(new HotelRoomsProfile()));
        _mapper = config.CreateMapper();
    }

    [Theory, AutoMoqData(omitOnRecursion: true)]
    public void HotelRoomsSpecification_CreatesCorrectQuery(
        HotelRoomsQuery query,
        List<Room> rooms,
        Hotel hotel)
    {
        hotel.Id = query.Id;
        hotel.Rooms = rooms;
        query.PaginationParameters.PageNumber = 1;
        query.PaginationParameters.PageSize = rooms.Count;
        var expectedRooms = rooms
            .Where(r => query.RoomType == null || r.RoomType == query.RoomType)
            .OrderBy(r => r.RoomNumber)
            .Select(_mapper.Map<HotelRoomsResponse.Room>)
            .ToList();

        var spec = new HotelRoomsSpecification(query, _mapper);
        var result = spec.Evaluate(new List<Hotel> { hotel }.AsQueryable()).ToList();

        result.Should().ContainSingle();
        result.First().Id.Should().Be(query.Id);
        result.First().TotalCount.Should().Be(rooms.Count);
        result.First().Items.Should().BeEquivalentTo(expectedRooms);
    }

    [Theory, AutoMoqData(omitOnRecursion: true)]
    public void HotelRoomsSpecification_PaginationWorksCorrectly(
        HotelRoomsQuery query,
        List<Room> rooms,
        Hotel hotel)
    {
        hotel.Id = query.Id;
        hotel.Rooms = rooms;
        query.PaginationParameters.PageNumber = 2;
        query.PaginationParameters.PageSize = 2;
        var expectedRooms = rooms
            .Where(r => query.RoomType == null || r.RoomType == query.RoomType)
            .OrderBy(r => r.RoomNumber)
            .Skip((query.PaginationParameters.PageNumber - 1) * query.PaginationParameters.PageSize)
            .Take(query.PaginationParameters.PageSize)
            .Select(_mapper.Map<HotelRoomsResponse.Room>)
            .ToList();

        var spec = new HotelRoomsSpecification(query, _mapper);
        var result = spec.Evaluate(new List<Hotel> { hotel }.AsQueryable()).ToList();

        result.Should().ContainSingle();
        result.First().Id.Should().Be(query.Id);
        result.First().TotalCount.Should().Be(rooms.Count);
        result.First().Items.Should().BeEquivalentTo(expectedRooms);
    }
}