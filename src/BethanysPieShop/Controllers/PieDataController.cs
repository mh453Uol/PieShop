using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BethanysPieShop.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BethanysPieShop.Controllers
{
    [Route("api/[controller]")]
    public class PieDataController : Controller
    {
        public readonly IPieRepository _pieRepository;

        public PieDataController(IPieRepository pieRepository)
        {
            _pieRepository = pieRepository;
        }

        [HttpGet]
        public IEnumerable<PieViewModel> LoadMorePies()
        {
            IEnumerable<Pie> pies = null;

            pies = _pieRepository.Pies
                    .OrderBy(p => p.PieId)
                    .Take(10);

            List<PieViewModel> piesToDisplay = new List<PieViewModel>();
            
            foreach(var pie in pies)
            {
                piesToDisplay.Add(MapToPieViewModel(pie));
            }

            return piesToDisplay;
        }

        private PieViewModel MapToPieViewModel(Pie pie)
        {
            return new PieViewModel()
            {
                PieId = pie.PieId,
                Name = pie.Name,
                Price = pie.Price,
                ShortDescription = pie.ShortDescription,
                ImageThumbnailUrl = pie.ImageThumbnailUrl
            };
        }
    }
}
