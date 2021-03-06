using LinenAndBird.DataAccessLayer;
using LinenAndBird.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinenAndBird.Controllers
{
  [Route("api/birds")]
  [ApiController]
  public class BirdsController : ControllerBase
  {

    BirdRepository _repo;
    public BirdsController(BirdRepository repo)
    {
      _repo = repo;
    }

    [HttpGet]
    public IEnumerable<Bird> GetAllBirds()
    {
      return _repo.GetAll();
    }

    [HttpGet("{id}")]
    public IActionResult GetBirdById(Guid id)
    {
      var bird =_repo.GetById(id);

      if (bird == null)
      {
        return NotFound($"No bird with the id {id} was found.");
      }

      return Ok(bird);
    }

    [HttpPost]
    public IActionResult AddBird(Bird newBird)
    {
      if (string.IsNullOrEmpty(newBird.Name) || string.IsNullOrEmpty(newBird.Color))
      {
        return BadRequest("Name and Color are required fields");
      }
      _repo.Add(newBird);
      return Ok();
    }
  }
}
