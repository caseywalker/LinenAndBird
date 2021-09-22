using LinenAndBird.DataAccessLayer;
using LinenAndBird.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinenAndBird.Controllers
{
  [Route("api/orders")]
  [ApiController]
  public class OrdersControllercs : ControllerBase
  {
    BirdRepository _birdRepo;
    HatRepository _hatRepo;
    OrderRepository _orderRepo;

    public OrdersControllercs(BirdRepository birdRepo, HatRepository hatRepo, OrderRepository orderRepo)
    {
      _birdRepo = birdRepo;
      _hatRepo = hatRepo;
      _orderRepo = orderRepo;
    }

    [HttpGet]
    public IActionResult GetAllOrders()
    {
      return Ok(_orderRepo.GetAll());
    }

    [HttpGet("{id}")]
    public IActionResult GetOrderById(Guid id)
    {
      Order order = _orderRepo.Get(id);

      if (order == null)
      {
        return NotFound("No order exists with that id");
      }

      return Ok(order);
    }


    //One way of handling below, is one way of getting props from URL
    // api/orders/bird/23523521/hat/123512/28.73 [HttpPost("bird/{birdId}/hat/{hatId}/{price}")]
    //Better way here is to create a model to handle creating an order command from user
    [HttpPost]
    public IActionResult CreateOrder(CreateOrderCommand command)
    {
      var hatToOrder = _hatRepo.GetById(command.HatId);
      var birdToOrder = _birdRepo.GetById(command.BirdId);

      if(hatToOrder == null)
      {
        return NotFound("No matching hat found in the database.");
      }

      //You may remove the brackets from a single line return statement
      if(birdToOrder == null)
        return NotFound("No matching bird found in the database.");
   

      var order = new Order
      {
        Bird = birdToOrder,
        Hat = hatToOrder,
        Price = command.Price
      };

      _orderRepo.Add(order);
      return Created($"/api/orders/{order.Id}", order);
    }
  }
}
