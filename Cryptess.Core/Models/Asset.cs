using System;

namespace Cryptess.Core.Models
{
    public class Asset
    {
        public string AssetId { get; set; }
        public string Name { get; set; }
        public decimal price { get; set; }
        public decimal Volume24h { get; set; }
        public decimal Change1h { get; set; }
        public decimal Change24h { get; set; }
        public decimal Change7d { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
