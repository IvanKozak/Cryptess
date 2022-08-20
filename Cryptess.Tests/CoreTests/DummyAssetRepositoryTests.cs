
using Cryptess.Core.Models;
using Cryptess.Core.Repositories;
using FluentAssertions;

namespace Cryptess.Tests.CoreTests
{
    public class DummyAssetRepositoryTests
    {
        private readonly Asset _asset;
        private readonly IAssetRepository _assetRepository;

        public DummyAssetRepositoryTests()
        {
            _asset = new Asset
            {
                AssetId = "USDT",
                Name = "Tether",
                Price = 0.997003745398937M,
                Volume24h = 12302009051.89854M,
                Change1h = -0.4859812960071203M,
                Change24h = 0.05212026929493508M,
                Change7d = -0.34939799249835474M,
                Status = "recent",
                CreatedAt = new DateTime(2021, 9, 21, 1, 53, 50),
                UpdatedAt = new DateTime(2022, 8, 20, 11, 7, 17)
            };

            _assetRepository = new DummyAssetRepository();
        }

        [Fact]
        public async Task GetAssets_ShouldReturnRightAmountOfAssets()
        {
            var result = await _assetRepository.GetAssets();

            result.Count.Should().Be(100);
        }
    }
}
