using System.ComponentModel.DataAnnotations;

namespace BlazeMyFinance.Shared.Dto
{
    /// <summary>
    /// CustomerInfo - class contains customer info
    /// </summary>
    public class CustomerInfo
    {
        /// <summary>
        /// gets/sets a unique identifier
        /// </summary>
        public Guid Guid { get; set; }

        /// <summary>
        /// gets/sets a unique id for every customer
        /// </summary>
        public long CustomerId { get; set; }

        /// <summary>
        /// gets/sets a customer full name
        /// </summary>
        [StringLength(200)]
        public string CustomerName { get; set; }

        /// <summary>
        /// gets/sets account info
        /// </summary>
        public AccountInfo? AccountInfo { get; set; }

        /// <summary>
        /// gets/sets transactions
        /// </summary>
        public List<TransactionInfo>? Transactions { get; set; }

        /// <summary>
        /// constructor
        /// </summary>
        public CustomerInfo()
        {
            Guid = Guid.NewGuid();
        }
    }

    /// <summary>
    /// DashboardInfo - class contains dashboard record 
    /// </summary>
    public class DashboardInfo
    {
        public List<CustomerInfo>? Customers { get; set; }
    }

}
