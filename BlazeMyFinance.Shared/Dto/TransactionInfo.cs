namespace BlazeMyFinance.Shared.Dto
{
    /// <summary>
    /// TransactionInfo - class contains transaction info
    /// </summary>
    public class TransactionInfo
    {
        /// <summary>
        /// gets/sets a unique identifier
        /// </summary>
        public Guid Guid { get; set; }

        /// <summary>
        /// gets/sets a unique id for every account holder
        /// </summary>
        public long AccountId { get; set; }

        /// <summary>
        /// gets/sets transaction date time
        /// </summary>
        public DateTime? TransactionDateTime { get; set; }

        /// <summary>
        /// gets/sets an amount of this particular transaction
        /// </summary>
        public decimal? Amount { get; set; }

        /// <summary>
        /// gets/sets a remaining balance amount
        /// </summary>
        public decimal? RemainingBalance { get; set; }

        /// <summary>
        /// constructor
        /// </summary>
        public TransactionInfo()
        {
            Guid = Guid.NewGuid();
        }
    }
}
