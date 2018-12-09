using GoldenSweets.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GoldenSweets.Core.Models
{
    public class Rating
    {
        public int Id { get; set; }

        [HiddenInput]
        public int Value { get; set; }

        [Required]
        public string UserId { get; set; }
        public IdentityUser User { get; set; }

        [Required]
        public int CakeId { get; set; }
        public Cake Cake { get; set; }

    }
}
