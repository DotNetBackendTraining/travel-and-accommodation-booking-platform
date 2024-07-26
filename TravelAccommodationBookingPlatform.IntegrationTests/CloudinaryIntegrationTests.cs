using System.Text;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TravelAccommodationBookingPlatform.Application.Interfaces.Files;
using TravelAccommodationBookingPlatform.Infrastructure.Services.Files;
using TravelAccommodationBookingPlatform.Infrastructure.Settings;
using TravelAccommodationBookingPlatform.IntegrationTests.Shared;
using TravelAccommodationBookingPlatform.Presentation.Shared;

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
        // Open test image
        var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "test_image.png");
        await using var fileStream = File.OpenRead(filePath);
        var formFile = new FormFile(fileStream, 0, fileStream.Length, "test_image", "test_image.png");
        var fileWrapper = new FormFileWrapper(formFile);

        // Upload file
        var resultUrls = await _service.SaveAllAsync(new List<IFile> { fileWrapper });
        resultUrls.Should().NotBeEmpty();

        // Verify uploaded file
        foreach (var url in resultUrls)
        {
            Uri.IsWellFormedUriString(url, UriKind.Absolute)
                .Should().BeTrue();
        }

        // Cleanup
        foreach (var url in resultUrls)
        {
            await _service.DeleteAsync(url);
        }
    }
}