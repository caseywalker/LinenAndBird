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

    internal List<Hat> GetAll()
    {
      return _hats;
    }

    internal void Add(Hat newHat)
    {
      _hats.Add(newHat);
    }

    internal List<Hat> GetByStyle(HatStyle style)
    {
      var matches = _hats.Where(hat => hat.Style == style).ToList();
      return matches;
    }
  }
}
