using LinenAndBird.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinenAndBird.DataAccessLayer
{
  public interface IHatRepository
  {
    Hat GetById(Guid id);
    List<Hat> GetAll();
    List<Hat> GetByStyle(HatStyle style);
    void Add(Hat newHat);
   
  }
}
