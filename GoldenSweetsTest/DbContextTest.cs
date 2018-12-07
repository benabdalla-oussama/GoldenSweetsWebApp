using GoldenSweets.Controllers;
using GoldenSweets.Core.Models;
using GoldenSweets.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;


namespace GoldenSweetsTest
{
    public class DbContextTest
    {
        
        [Fact(DisplayName = "Test_Gallery-GalleryImages_RelationShip")]
        public async void Test_Gallery_GalleryImages_RelationShip()
        {
            using (var context = GetContextWithData())
            {
                GalleryRepository _galleryRepository = new GalleryRepository(context);
                var GalleryImages = await _galleryRepository.GetSelectedImages();
                Assert.Equal(3,GalleryImages.Count);
            }
        }
        private GoldenSweetsDbContext GetContextWithData()
        {
            var options = new DbContextOptionsBuilder<GoldenSweetsDbContext>()
                              .UseInMemoryDatabase(Guid.NewGuid().ToString())
                              .Options;
            var context = new GoldenSweetsDbContext(options);

            var gallery1 = new Gallery { Id = 1, Name = "Gallery1" ,GalleryTime=DateTime.Now,Selected=true};
            context.Galleries.Add(gallery1);

            context.GalleryImages.Add(new GalleryImage { Id = 1, Label = "Cake 1", GalleryId = 1 , Url= "~/img/carousel1.jpg", Selected = true });
            context.GalleryImages.Add(new GalleryImage { Id = 2, Label = "Cake 2", GalleryId = 1, Url = "~/img/carousel2.jpg", Selected = true });
            context.GalleryImages.Add(new GalleryImage { Id = 3, Label = "Cake 3", GalleryId = 1, Url = "~/img/carousel3.jpg", Selected = true });

            context.SaveChanges();

            return context;
        }
    }
}
