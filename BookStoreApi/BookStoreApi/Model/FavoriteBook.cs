using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreApi.Model
{
    public class FavoriteBook
    {
        [Key]
        public string HolderId { get; set; }
    }
}
