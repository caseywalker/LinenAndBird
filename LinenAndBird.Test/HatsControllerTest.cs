using LinenAndBird.Controllers;
using LinenAndBird.DataAccessLayer;
using LinenAndBird.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using Xunit;

namespace LinenAndBird.Test
{
  public class HatsControllerTest
  {
    [Fact]
    public void requesting_all_hats_returns_all_hats()
    {
      //Arrange
      IConfiguration config = new ConfigurationSection();
      var repo = new HatRepository();
      var controller = new HatsController()
      //Act
      
      //Assert

    }
  }

  public class FakeHatRepository : IHatRepository
  {
    void IHatRepository.Add(Hat newHat)
    {
      throw new NotImplementedException();
    }

    List<Hat> IHatRepository.GetAll()
    {
      throw new NotImplementedException();
    }

    Hat IHatRepository.GetById(Guid id)
    {
      throw new NotImplementedException();
    }

    List<Hat> IHatRepository.GetByStyle(HatStyle style)
    {
      throw new NotImplementedException();
    }
  }
}
