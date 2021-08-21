using LinenAndBird.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinenAndBird.DataAccessLayer
{
  public class HatRepository
  {
    static List<Hat> _hats = new List<Hat>
    {
        new Hat
        {
          Id = Guid.NewGuid(),
          Color = "Blue",
          Designer = "Charlie",
          Style = HatStyle.OpenBack
        },
        new Hat
        {
          Id = Guid.NewGuid(),
          Color = "Brown",
          Designer = "Casey",
          Style = HatStyle.WideBrim
        },
        new Hat
        {
          Id = Guid.NewGuid(),
          Color = "Black",
          Designer = "Tom",
          Style = HatStyle.Normal
        }
    };

    internal List<Hat> GetAll()
    {
      return _hats;
    }

    internal void Add(Hat newHat)
    {
      newHat.Id = Guid.NewGuid();
      _hats.Add(newHat);
    }

    internal List<Hat> GetByStyle(HatStyle style)
    {
      return _hats.Where(hat => hat.Style == style).ToList();
    }
  }
}
