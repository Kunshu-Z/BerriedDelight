using System.IO.Pipelines;

namespace BerriedDelight.Models
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public string UserId {  get; set; }
        public int BerryId { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public Berry Berry { get; set; } = default!;
        public Order Order { get; set; } = default!;
    }
}
