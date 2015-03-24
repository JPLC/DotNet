using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrelloModel
{
    public class TrelloContext : DbContext
    {
        public TrelloContext() : base("TrelloContext")
        {
        }

        public DbSet<Board> Boards { get; set; }

        public DbSet<List> Lists { get; set; }

        public DbSet<Card> Cards { get; set; }
    }
}
