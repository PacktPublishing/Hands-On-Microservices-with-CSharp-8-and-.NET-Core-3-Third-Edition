using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FlixOne.BookStore.CustomerService.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Customer : BaseEntity
    {
        [Required]
        [Column("FirstName")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        [Column("JoinedOn")]
        public DateTime MemberSince { get; set; }
        [Column("WalletBalance")]
        public decimal Wallet { get; set; }

        [Display(Name = "Full Name")]
        public string FullName => LastName + " " + FirstName;
    }
}
