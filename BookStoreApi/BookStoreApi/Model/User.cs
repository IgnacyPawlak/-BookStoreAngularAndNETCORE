using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreApi.Model
{
    public class User : IdentityUser
    {
        [Key]
        public override string Id { get => base.Id; set => base.Id = value; }

        public UserType UserType { get; set; }

        public string UserFullName { get; set; }

        public virtual ICollection<Node> BookList { get; set; }
    }
}
