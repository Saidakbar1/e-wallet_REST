using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using e_wallet.Data.Model;

namespace e_wallet.REST_API.DataContexts
{
    public class EWalletContext : DbContext
    {
        public EWalletContext(DbContextOptions<EWalletContext> options)
            : base(options)
        {
        }

        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
