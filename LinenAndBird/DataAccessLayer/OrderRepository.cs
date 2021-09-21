using LinenAndBird.Models;
using Microsoft.Data.SqlClient;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinenAndBird.DataAccessLayer
{
  public class OrderRepository
  {
    const string _connectionString = "Server=localhost;Database=LinenAndBird;Trusted_Connection=True;";


    internal void Add(Order order)
    {
      using var db = new SqlConnection(_connectionString);

      var sql = @"INSERT INTO [dbo].[orders]
           ([BirdId]
           ,[HatId]
           ,[Price])
     OUTPUT INSERTED.Id
     VALUES
           (@BirdId
           ,@HatId
           ,@Price)";

      var parameters = new
      {
        BirdId = order.Bird.Id,
        HatId = order.Hat.Id,
        Price = order.Price
      };
      var id = db.ExecuteScalar<Guid>(sql, parameters);

      order.Id = id;
    }

    internal IEnumerable<Order> GetAll()
    {
      using var db = new SqlConnection(_connectionString);

      var sql = @"SELECT *
              FROM Orders o
	          JOIN Birds b
		      ON B.id = o.BirdId
	          JOIN Hats h
		      ON H.id = o.HatId";
      // On query below, we re telling dapper that we are getting an order, bird and hat from our join
      // The fourth and final param of the query is what we expect the query to return, an order
      // We have to create a function to take those 3 types and turning it into an order
      // The split on is telling Dapper where each of the 3 objects start and end
      var orders = db.Query<Order, Bird, Hat, Order>(sql, (order, bird, hat) =>
         {
           order.Bird = bird;
           order.Hat = hat;
           return order;
         }, splitOn: "Id");

      return orders;
    }

    internal Order Get(Guid id)
    {
      using var db = new SqlConnection(_connectionString);

      var sql = @"SELECT *
              FROM Orders o
	          JOIN Birds b
		      ON B.id = o.BirdId
	          JOIN Hats h
		      ON H.id = o.HatId
              WHERE o.id = @id";

      var order = db.Query<Order, Bird, Hat, Order>(sql, (order, bird, hat) =>
      {
        order.Bird = bird;
        order.Hat = hat;
        return order;
      }, new {id}, splitOn: "Id");

      return order.FirstOrDefault();
    }
  }
}
