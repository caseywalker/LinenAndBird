using LinenAndBird.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinenAndBird.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class HatsController : ControllerBase
  {
    static List<Hat> _hats = new List<Hat>
    {
        new Hat
        {
          Color = "Blue",
          Designer = "Charlie",
          Style = HatStyle.OpenBack
        },
        new Hat
        {
          Color = "Brown",
          Designer = "Casey",
          Style = HatStyle.WideBrim
        },
        new Hat
        {
          Color = "Black",
          Designer = "Tom",
          Style = HatStyle.Normal
        }
    };

    [HttpGet]
    public List<Hat> GetAllHats()
    {
      return _hats;
    }

    //Paramater must match the argument in the Get method. Get method will utilize the URL to pass the style. 
    [HttpGet("styles/{style}")]
    public List<Hat> GetHatsByStyle(HatStyle style)
    {
      var matches = _hats.Where(hat => hat.Style == style).ToList();
      return matches;
    }

    [HttpPost]
    public void AddAHat(Hat newHat)
    {
      _hats.Add(newHat);
    }
  }
}
