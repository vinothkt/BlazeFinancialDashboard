namespace BlazeMyFinance.Shared.Dto
{
    /// <summary>
    /// AccountInfo - object holds customer account info
    /// </summary>
    public class AccountInfo
    {
        /// <summary>
        /// gets/sets a unique identifier
        /// </summary>
        public Guid Guid { get; set; }

        /// <summary>
        /// gets/sets a customer id
        /// </summary>
        public long CustomerId { get; set; }

        /// <summary>
        /// gets/sets a unique number for every account holder
        /// </summary>
        public long AccountNumber { get; set; }

        /// <summary>
        /// gets/sets a balance amount
        /// </summary>
        public decimal? RemainingBalance { get; set; }
        
        /// <summary>
        /// gets/sets a status of account
        /// </summary>
        public bool? IsActive { get; set; }
        
        /// <summary>
        /// constructor
        /// </summary>
        public AccountInfo()
        {
            Guid = Guid.NewGuid();
        }
    }

}
