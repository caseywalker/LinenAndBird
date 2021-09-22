using LinenAndBird.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace LinenAndBird.DataAccessLayer
{
  public class HatRepository
  {
    readonly string _connectionString;

    public HatRepository(IConfiguration config)
    {
      _connectionString = config.GetConnectionString("LinenAndBird");
    }

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
