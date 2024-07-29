using TravelAccommodationBookingPlatform.Domain.Enums;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Persistence.Data.Utility;

internal static class SharedDataUtility
{
    internal static string ExampleCityImageUrl() =>
        "https://res.cloudinary.com/dsn0xwnmc/image/upload/v1722287332/xvjejm3t69ydjsljlbmw.jpg";

    internal static List<Image> GenerateRandomImages(string baseImageUrl)
    {
        var random = new Random();
        var randomNumberOfImages = random.Next(1, 10);

        var images = new List<Image>();
        for (var i = 0; i < randomNumberOfImages; i++)
        {
            images.Add(new Image { Url = baseImageUrl });
        }

        return images;
    }

    internal static List<Image> GenerateRandomRoomImages(RoomType roomType)
    {
        var baseUrl = roomType switch
        {
            RoomType.Luxury => "https://res.cloudinary.com/dsn0xwnmc/image/upload/v1721672685/fqiyf91ayociwkwaroop.jpg",
            RoomType.Budget => "https://res.cloudinary.com/dsn0xwnmc/image/upload/v1721687500/p5gc3jvshaaoa1a1aaqf.jpg",
            RoomType.Boutique =>
                "https://res.cloudinary.com/dsn0xwnmc/image/upload/v1721687498/zzdyswkyzxhezsams1x2.jpg",
            _ => "https://res.cloudinary.com/dsn0xwnmc/image/upload/v1721672685/fqiyf91ayociwkwaroop.jpg"
        };

        return GenerateRandomImages(baseUrl);
    }

    internal static List<Amenity> GenerateRandomAmenities()
    {
        var random = new Random();
        var allAmenities = Enum.GetValues(typeof(Amenity)).Cast<Amenity>().ToList();
        var randomNumberOfAmenities = random.Next(1, allAmenities.Count);
        return allAmenities.OrderBy(_ => random.Next()).Take(randomNumberOfAmenities).ToList();
    }

    private static readonly List<string> Texts =
    [
        "Vestibulum ac est lacinia nisi venenatis tristique. Fusce congue, diam id ornare imperdiet, sapien urna pretium nisl, ut volutpat sapien arcu sed augue. Aliquam erat volutpat.",
        "Curabitur in libero ut massa volutpat convallis. Morbi odio odio, elementum eu, interdum eu, tincidunt in, leo. Maecenas pulvinar lobortis est.\n\nPhasellus sit amet erat. Nulla tempus. Vivamus in felis eu sapien cursus vestibulum.",
        "Integer tincidunt ante vel ipsum. Praesent blandit lacinia erat. Vestibulum sed magna at nunc commodo placerat.\n\nPraesent blandit. Nam nulla. Integer pede justo, lacinia eget, tincidunt eget, tempus vel, pede.",
        "Nam ultrices, libero non mattis pulvinar, nulla pede ullamcorper augue, a suscipit nulla elit ac nulla. Sed vel enim sit amet nunc viverra dapibus. Nulla suscipit ligula in lacus.\n\nCurabitur at ipsum ac tellus semper interdum. Mauris ullamcorper purus sit amet nulla. Quisque arcu libero, rutrum ac, lobortis vel, dapibus at, diam.",
        "Nullam porttitor lacus at turpis. Donec posuere metus vitae ipsum. Aliquam non mauris.",
        "Maecenas ut massa quis augue luctus tincidunt. Nulla mollis molestie lorem. Quisque ut erat.",
        "Quisque id justo sit amet sapien dignissim vestibulum. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Nulla dapibus dolor vel est. Donec odio justo, sollicitudin ut, suscipit a, feugiat et, eros.\n\nVestibulum ac est lacinia nisi venenatis tristique. Fusce congue, diam id ornare imperdiet, sapien urna pretium nisl, ut volutpat sapien arcu sed augue. Aliquam erat volutpat.",
        "Fusce posuere felis sed lacus. Morbi sem mauris, laoreet ut, rhoncus aliquet, pulvinar sed, nisl. Nunc rhoncus dui vel sem.",
        "Morbi porttitor lorem id ligula. Suspendisse ornare consequat lectus. In est risus, auctor sed, tristique in, tempus sit amet, sem.",
        "In congue. Etiam justo. Etiam pretium iaculis justo.\n\nIn hac habitasse platea dictumst. Etiam faucibus cursus urna. Ut tellus.",
        "Phasellus in felis. Donec semper sapien a libero. Nam dui."
    ];

    internal static string GenerateRandomText()
    {
        return Texts[new Random().Next(Texts.Count)];
    }

    internal static List<string> GenerateRandomTexts()
    {
        var random = new Random();
        var randomNumberOfTexts = random.Next(1, Texts.Count);
        return Texts.OrderBy(_ => random.Next()).Take(randomNumberOfTexts).ToList();
    }

    internal static NumberOfGuests GenerateRandomNumberOfGuests()
    {
        var random = new Random();
        return new NumberOfGuests
        {
            Adults = random.Next(1, 9),
            Children = random.Next(0, 5)
        };
    }
}