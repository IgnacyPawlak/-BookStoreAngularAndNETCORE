using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreApi.Model
{
    public class Node
    {
        public string UserId { get; set; }
        public int BookId { get; set; }
        public string Note { get; set; }

        public virtual User User { get; set; }
        public virtual Book Book { get; set; }
    }
}
