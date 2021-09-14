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
    const string _connectionString = "Server=localhost;Database=LinenAndBird;Trusted_Connection=True;";

    internal IEnumerable<Bird> GetAll()
    {
      // connections are like the tunnel between our app and the database
      using var connection = new SqlConnection(_connectionString);
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
        bird.Color = reader["Color"].ToString();

        //each bird goes into the list to return
        birds.Add(bird);
      }
      return birds;
      //return _birds;
    }

    internal void Add(Bird newBird)
    {
      using var connection = new SqlConnection(_connectionString);

      connection.Open();

      var command = connection.CreateCommand();
      command.CommandText = @"insert into birds(Type, Color, Size, Name)
                              output inserted.Id
                              values (@Type, @Color, @Size, @Name)";

      command.Parameters.AddWithValue("Type", newBird.Type);
      command.Parameters.AddWithValue("Color", newBird.Color);
      command.Parameters.AddWithValue("Size", newBird.Size);
      command.Parameters.AddWithValue("Name", newBird.Name);

      var newId = (Guid)command.ExecuteScalar();

      newBird.Id = newId;

      //newBird.Id = Guid.NewGuid();

      //_birds.Add(newBird);
    }

    internal Bird GetById(Guid birdId)
    {
      using var connection = new SqlConnection(_connectionString);

      connection.Open();

      var command = connection.CreateCommand();
      command.CommandText = $@"SELECT *
                               FROM Birds
                                WHERE id= @id";

      //parameterization prevents sql injection
      command.Parameters.AddWithValue("id", birdId);

      //execute reader is to get all results of the query
      var reader = command.ExecuteReader();

      // data readers only get one row from the results at a time, need to use a while statement to get all
      if(reader.Read())
      {
        //Mapping data from the relational model to the object model
        var bird = new Bird();
        bird.Id = reader.GetGuid(0);
        bird.Size = reader["Size"].ToString();
        bird.Type = (BirdType)reader["Type"];
        bird.Name = reader["Name"].ToString();
        bird.Color = reader["Color"].ToString();

        //Here we are just returning a single bird
        return bird;
      }
      return null;

      //return _birds.FirstOrDefault(bird => bird.Id == birdId);
    }
  }
}
