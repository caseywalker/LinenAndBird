﻿using LinenAndBird.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace LinenAndBird.DataAccessLayer
{
  public class BirdRepository
  {
    const string _connectionString = "Server=localhost;Database=LinenAndBird;Trusted_Connection=True;";

    internal IEnumerable<Bird> GetAll()
    {
      // connections are like the tunnel between our app and the database
      //using var connection = new SqlConnection(_connectionString);

      //Dapper still requires a connection to the DB
      using var db = new SqlConnection(_connectionString);

      //Query<T> is for getting results from the database and putting them into a C# type
      var birds = db.Query<Bird>(@"SELECT * 
                    FROM Birds");

      return birds;
      //connections aren't open by default, we've gotta do that ourself
      //connection.Open();

      //This is what tells SQL what we want to do. 
      //var command = connection.CreateCommand();
      //command.CommandText = @"SELECT *
                              //FROM Birds";

      //execute reader is to get all results of the query
      //var reader = command.ExecuteReader();

      //var birds = new List<Bird>();

      // data readers only get one row from the results at a time, need to use a while statement to get all
      //while (reader.Read())
      //{
        //Mapping data from the relational model to the object model
        //var bird = MapFromReader(reader);

        //each bird goes into the list to return
        //birds.Add(bird);
      //}
      //return birds;
      //return _birds;
    }

    internal void Remove(Guid id)
    {
      //using var connection = new SqlConnection(_connectionString);
      using var db = new SqlConnection(_connectionString);
      var sql = @"Delete
                  FROM Birds
                  WHERE Id = @id";

      db.Execute(sql, new { id });

      //connection.Open();

      //var command = connection.CreateCommand();
      //command.CommandText = @"Delete
      //                        FROM Birds
      //                        WHERE Id = @id";

      //command.Parameters.AddWithValue("id", id);
      //command.ExecuteReader();
    }

    internal Bird Update(Guid id, Bird bird)
    {
      //using var connection = new SqlConnection(_connectionString);
      using var db = new SqlConnection(_connectionString);
      var sql = @"UPDATE Birds
                SET Type = @type,
                 Color = @color,
                 Size = @size,
                 Name = @name
                 OUTPUT inserted.*
                 WHERE Id = @id";
      bird.Id = id;

      var updatedBird = db.QuerySingleOrDefault(sql, bird);

      return updatedBird;

      //connection.Open();

      //var command = connection.CreateCommand();
      //command.CommandText = @"UPDATE Birds
      //                      SET Type = @type,
      //                     Color = @color,
      //                     Size = @size,
      //                     Name = @name
      //                      OUTPUT inserted.*
      //                      WHERE Id = @id";

      //command.Parameters.AddWithValue("color", bird.Color);
      //command.Parameters.AddWithValue("size", bird.Size);
      //command.Parameters.AddWithValue("name", bird.Name);
      //command.Parameters.AddWithValue("type", bird.Type);
      //command.Parameters.AddWithValue("id", id);

      //var reader = command.ExecuteReader();

      //if (reader.Read())
      //{
      //  return MapFromReader(reader);
      //}

      //return null;
    }

    internal void Add(Bird newBird)
    {
      var db = new SqlConnection(_connectionString);

      var sql = @"insert into birds(Type, Color, Size, Name)
                            output inserted.Id
                            values (@Type, @Color, @Size, @Name)";


      var id = db.ExecuteScalar<Guid>(sql, newBird);
      newBird.Id = id;
      //using var connection = new SqlConnection(_connectionString);

      //connection.Open();

      //var command = connection.CreateCommand();
      //command.CommandText = @"insert into birds(Type, Color, Size, Name)
      //                        output inserted.Id
      //                        values (@Type, @Color, @Size, @Name)";

      //command.Parameters.AddWithValue("Type", newBird.Type);
      //command.Parameters.AddWithValue("Color", newBird.Color);
      //command.Parameters.AddWithValue("Size", newBird.Size);
      //command.Parameters.AddWithValue("Name", newBird.Name);

      //var newId = (Guid)command.ExecuteScalar();

      //newBird.Id = newId;

      //newBird.Id = Guid.NewGuid();

      //_birds.Add(newBird);
    }

    internal Bird GetById(Guid birdId)
    {
      //using var connection = new SqlConnection(_connectionString);
      using var db = new SqlConnection(_connectionString);

      var sql = @"SELECT *
                  FROM Birds
                  WHERE id= @id";

      var bird = db.QuerySingleOrDefault<Bird>(sql, new { id = birdId });

      return bird;
      //connection.Open();

      //var command = connection.CreateCommand();
      //command.CommandText = $@"SELECT *
      //                         FROM Birds
      //                          WHERE id= @id";

      ////parameterization prevents sql injection
      //command.Parameters.AddWithValue("id", birdId);

      ////execute reader is to get all results of the query
      //var reader = command.ExecuteReader();

      //// data readers only get one row from the results at a time, need to use a while statement to get all
      //if(reader.Read())
      //{
      //  return MapFromReader(reader);
      //}
      //return null;

      ////return _birds.FirstOrDefault(bird => bird.Id == birdId);
    }

    Bird MapFromReader(SqlDataReader reader)
    {
      var bird = new Bird();
      bird.Id = reader.GetGuid(0);
      bird.Size = reader["Size"].ToString();
      bird.Type = (BirdType)reader["Type"];
      bird.Name = reader["Name"].ToString();
      bird.Color = reader["Color"].ToString();

      //Here we are just returning a single bird
      return bird;
    }
  }
}
