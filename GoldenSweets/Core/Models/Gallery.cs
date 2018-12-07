using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GoldenSweets.Core.Models
{
        public class Gallery
        {
            public int Id { get; set; }

            [Required]
            public DateTime GalleryTime { get; set; }

            [StringLength(255)]
            [Required]
            public string Name { get; set; }

            public Boolean Selected { get; set; }

            public List<GalleryImage> GalleryImages { get; set; }

            public Gallery()
            {
                GalleryImages = new List<GalleryImage>();
            }
        }
    }


