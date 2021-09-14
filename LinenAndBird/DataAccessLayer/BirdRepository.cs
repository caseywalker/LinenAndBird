using LinenAndBird.Models;
using Microsoft.Data.SqlClient;
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
      // connections are like the tunnel between our app and the database
      using var connection = new SqlConnection("Server=localhost;Database=LinenAndBird;Trusted_Connection=True;");
      //connections aren't open by default, we've gotta do that ourself
      connection.Open();

      //This is what tells SQL what we want to do. 
      var command = connection.CreateCommand();
      command.CommandText = @"SELECT *
                              FROM Birds";

      //execute reader is to get all results of the query
      var reader = command.ExecuteReader();

      var birds = new List<Bird>();

      // data readers only get one row from the results at a time, need to use a while statement to get all
      while (reader.Read())
      {
        //Mapping data from the relational model to the object model
        var bird = new Bird();
        bird.Id = reader.GetGuid(0);
        bird.Size = reader["Size"].ToString();
        bird.Type = (BirdType)reader["Type"];
        bird.Name = reader["Name"].ToString();

        //each bird goes into the list to return
        birds.Add(bird);
      }
      return birds;
      //return _birds;
    }
    internal void Add(Bird newBird)
    {
      newBird.Id = Guid.NewGuid();

      _birds.Add(newBird);
    }

    internal Bird GetById(Guid birdId)
    {
      return _birds.FirstOrDefault(bird => bird.Id == birdId);
    }
  }
}
