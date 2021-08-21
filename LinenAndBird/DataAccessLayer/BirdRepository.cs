using LinenAndBird.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinenAndBird.DataAccessLayer
{
  public class BirdRepository
  {

    static List<Bird> _birds = new List<Bird>
    {
      new Bird
      {
        Id = Guid.NewGuid(),
        Name = "Tweety",
        Color = "Yellow",
        Size = "Small",
        Type = BirdType.Dead,
        Accesories = new List<string>{ "Beanie", "Eye Patch" }
      },
      new Bird
      {
        Id = Guid.NewGuid(),
        Name = "Sam",
        Color = "Green",
        Size = "Large",
        Type = BirdType.Linen,
        Accesories = new List<string>{ "Necklace", "Cannon" }
      }
    };


    internal IEnumerable<Bird> GetAll()
    {
      return _birds;
    }
    internal void Add(Bird newBird)
    {
      newBird.Id = Guid.NewGuid();

      _birds.Add(newBird);
    }
  }
}
