using Cryptess.Core.Repositories;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Cryptess.Tests.Integration;

public class CryptingUpAssetRepositoryTests
{
    private readonly IAssetRepository _assetRepo;
    private Mock<IConfiguration> _config;

    public CryptingUpAssetRepositoryTests()
    {
        _config = new Mock<IConfiguration>();
        _config.Setup(c => c["CryptingUp:AssetsSize"])
            .Returns("10");
        _assetRepo = new CryptingUpAssetRepository(_config.Object);
    }

    [Fact]
    public void GetAssetsOverview_ShouldReturnRightAmountOfObjects()
    {
        var assets = _assetRepo.GetAssetsOverview();

        assets.Count.Should().Be(10);
    }

    [Fact]
    public async Task GetAssetsOverviewAsync_ShouldReturnRightAmountOfObjects()
    {
        var assets = await _assetRepo.GetAssetsOverviewAsync();

        assets.Count.Should().Be(10);
    }

    [Fact]
    public void GetAssets_ShouldReturnRightAmountOfObjects()
    {
        var assets = _assetRepo.GetAssets();

        assets.Count.Should().Be(10);
    }

    [Fact]
    public async Task GetAssetsAsync_ShouldReturnRightAmountOfObjects()
    {
        var assets = await _assetRepo.GetAssetsAsync();

        assets.Count.Should().Be(10);
    }

    [Fact]
    public async Task GetAssetByIdAsync_ShouldReturnRightObject()
    {
        var asset = await _assetRepo.GetAssetByIdAsync("BTC");

        asset.Name.Should().Be("Bitcoin");
    }

    [Fact]
    public void GetAssetById_ShouldReturnRightObject()
    {
        var asset = _assetRepo.GetAssetById("BTC");

        asset.Name.Should().Be("Bitcoin");
    }
}