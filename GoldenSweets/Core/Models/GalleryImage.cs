using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GoldenSweets.Core.Models
{
   
        public class GalleryImage
        {
            public int Id { get; set; }

            [StringLength(255)]
            [Required]
            public string Label { get; set; }

            [StringLength(255)]
            [Required]
            public string Url { get; set; }

            public Boolean Selected { get; set; }

            public int? GalleryId { get; set; }
            public Gallery Gallery { get; set; }

    }
    }


