using System.ComponentModel.DataAnnotations;

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
        /// gets/sets a unique id for every account holder
        /// </summary>
        public long AccountId { get; set; }

        /// <summary>
        /// gets/sets a customer full name
        /// </summary>
        [StringLength(200)]
        public string CustomerName { get; set; }

        /// <summary>
        /// gets/sets a balance amount
        /// </summary>
        public decimal? Balance { get; set; }
        
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
