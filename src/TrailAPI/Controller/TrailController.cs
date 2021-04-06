using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TrailAPI.Models;
using TrailAPI.Respository;

namespace TrailAPI.AddControllers
{
    
    [ApiController]
    public class TrailController:ControllerBase
    {
      private readonly ICommandRepo _commandRepo;
      public TrailController(ICommandRepo commandRepo){
        _commandRepo=commandRepo;
      }

        [HttpGet("api/trail")]
      public ActionResult<IEnumerable<CommandModel>> GetAllCommands()
      {
          var commandItems= _commandRepo.GetAllCommands();
          return Ok(commandItems);
      }

      [HttpGet("api/trail/{id}")]
      public ActionResult<CommandModel> GetCommandById(int id){
        var commandItem= _commandRepo.GetCommandById(id);
        if (commandItem==null){
          return NotFound();
        }
        return Ok(commandItem);
      }
    }
}