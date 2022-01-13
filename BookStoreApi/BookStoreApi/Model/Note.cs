using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreApi.Model
{
    public class Note
    {
        [Key]
        public int noteId { get; set; }

        public int userId { get; set; }

        public int bookId { get; set; }

        public string comment { get; set; }

    }
}
