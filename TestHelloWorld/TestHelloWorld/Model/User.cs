using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestHelloWorld.Model
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string VID { get; set; }
        public string Name { get; set; }
        public int BankId { get; set; }
        [ForeignKey("BankId")]
        public virtual Bank Bank { get; set; }
        public string AccountNo { get; set; }        
        public Decimal Balance { get; set; }
    }
}
