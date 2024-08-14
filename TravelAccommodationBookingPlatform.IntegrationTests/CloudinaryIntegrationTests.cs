using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TravelAccommodationBookingPlatform.Infrastructure.IO;
using TravelAccommodationBookingPlatform.Infrastructure.Settings;
using TravelAccommodationBookingPlatform.IntegrationTests.Shared;

namespace TravelAccommodationBookingPlatform.IntegrationTests;

[Collection("IntegrationTests")]
public class CloudinaryIntegrationTests : BaseIntegrationTest
{
    private readonly CloudinaryImageStorageService _service;

    public CloudinaryIntegrationTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
        var options = Scope.ServiceProvider.GetRequiredService<IOptions<CloudinarySettings>>();
        _service = new CloudinaryImageStorageService(options);
    }

    [Fact]
    public async Task UploadAndDeleteImageTest()
    {
        // Upload file
        var result = await _service.SaveAllAsync([DataUtility.GetTestImageFile()]);
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeEmpty();

        // Verify uploaded file
        foreach (var url in result.Value)
        {
            Uri.IsWellFormedUriString(url, UriKind.Absolute)
                .Should().BeTrue();
        }

        // Cleanup
        foreach (var url in result.Value)
        {
            var deleteResult = await _service.DeleteAsync(url);
            deleteResult.IsSuccess.Should().BeTrue();
        }
    }
}