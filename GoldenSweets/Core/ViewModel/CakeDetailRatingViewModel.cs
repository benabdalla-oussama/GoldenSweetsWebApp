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

        public async Task<Cake> LoadCake(ICakeRepository cakeRepository)
        {
            return await cakeRepository.GetCakeById(CakeId);
        }
    }
}
