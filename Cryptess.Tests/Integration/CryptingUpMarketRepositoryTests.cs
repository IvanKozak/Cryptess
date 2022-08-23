using Cryptess.Core.Repositories;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Cryptess.Tests.Integration;

public class CryptingUpMarketRepositoryTests
{
    private readonly IMarketRepository _marketRepo;
    private Mock<IConfiguration> _config;

    public CryptingUpMarketRepositoryTests()
    {
        _config = new Mock<IConfiguration>();
        _config.Setup(c => c["CryptingUp:MarketsSize"]).Returns("3");

        _marketRepo = new CryptingUpMarketRepository(_config.Object);
    }

    [Fact]
    public async Task GetMarketsByAssetIdAsync_ShouldReturnRightAmountOfObjects()
    {
        var markets = await _marketRepo.GetMarketsByAssetIdAsync("BTC");

        markets.Count.Should().Be(3);
    }
}