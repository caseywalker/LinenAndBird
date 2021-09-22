using LinenAndBird.DataAccessLayer;
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
    HatRepository _repo;
    public HatsController(HatRepository hatRepo)
    {
      _repo = hatRepo;
    }

    [HttpGet]
    public List<Hat> GetAllHats()
    {
      return _repo.GetAll();
    }

    //Paramater must match the argument in the Get method. Get method will utilize the URL to pass the style. 
    [HttpGet("styles/{style}")]
    public List<Hat> GetHatsByStyle(HatStyle style)
    {
      return _repo.GetByStyle(style);
    }

    [HttpPost]
    public void AddAHat(Hat newHat)
    {
      _repo.Add(newHat);
    }
  }
}
