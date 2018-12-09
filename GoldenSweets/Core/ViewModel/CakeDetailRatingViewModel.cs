using GoldenSweets.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldenSweets.Core.ViewModel
{
    public class CakeDetailRatingViewModel
    {
        [HiddenInput]
        public int Value { get; set; }
        public int CakeId { get; set; }
        public Cake cake { get; set; }

        public async void LoadCake(ICakeRepository cakeRepository)
        {
            var mycake = await cakeRepository.GetCakeById(CakeId);
            cake = mycake ;
        }
    }
}
