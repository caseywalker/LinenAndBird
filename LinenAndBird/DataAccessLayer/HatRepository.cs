using LinenAndBird.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
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
    const string _connectionString = "Server=localhost;Database=LinenAndBird;Trusted_Connection=True;";
    internal List<Hat> GetAll()
    {
      using var db = new SqlConnection(_connectionString);
      var sql = @"SELECT * FROM Hats";
      var hats = db.Query<Hat>(sql);
      
      return hats.ToList();
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

    internal Hat GetById(Guid hatId)
    {
      using var db = new SqlConnection(_connectionString);

      var sql = @"SELECT *
                 FROM Hats 
                 WHERE Id = @id";
      var parameters = new
      {
        id = hatId
      };
      var hat = db.QueryFirstOrDefault<Hat>(sql, parameters);
      return hat;
    }
  }
}
