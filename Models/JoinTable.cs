namespace FURNITURE.Models
{
    public class JoinTable
    {

        public UserAccountZ userAccountZ { get; set; }
        public ProductZ productZ { get; set; }
        public CategoryZ categoryZ { get; set; }

        public PaymentZ paymentZ { get; set; }

        public OrderZ orderZ { get; set; }

        public ProductOrderZ productOrderZ { get; set; }
    }
}
