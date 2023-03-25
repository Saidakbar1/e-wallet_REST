using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_wallet.Data.Model
{
    public class Transaction
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public double Amount { get; set; }

        public DateTime Date { get; set; }
    }
}
