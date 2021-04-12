using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TrailAPI.Models;
using TrailAPI.Respository;
using TrailAPI.Dtos;

namespace TrailAPI.AddControllers
{
    
    [ApiController]
    public class TrailController:ControllerBase
    {
      private readonly ICommandRepo _commandRepo;
      private readonly IMapper _mapper;
      public TrailController(ICommandRepo commandRepo, IMapper mapper){
        _commandRepo=commandRepo;
        _mapper=mapper;
      }

        [HttpGet("api/trail")]
      public ActionResult<IEnumerable<CommandReadDto>> GetAllCommands()
      {
          var commandItems= _commandRepo.GetAllCommands();
          return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandItems));
      }

      [HttpGet("api/trail/{id}", Name="GetCommandById")]
      public ActionResult<CommandModel> GetCommandById(int id){
        var commandItem= _commandRepo.GetCommandById(id);
        if (commandItem==null){
          return NotFound();
        }
        return Ok(_mapper.Map<CommandReadDto>(commandItem));
      }

      [HttpPost("api/Create")]
      public ActionResult <CommandReadDto> CreateCommand(CommandCreateDto createDto){
        var commandModel = _mapper.Map<CommandModel>(createDto);
      _commandRepo.CreateCommand(commandModel);
      _commandRepo.SaveChanges();
       var commandReadDto = _mapper.Map<CommandReadDto>(commandModel);
       return CreatedAtRoute(nameof(GetCommandById),
       new {Id = commandReadDto.Id}, commandReadDto);

      }

      [HttpPut("api/update/{id}")]
      public ActionResult UpdateCommand(int id, CommandUpdateDto commandUpdate){
        var commandModelFromRepo= _commandRepo.GetCommandById(id);
        if(commandModelFromRepo==null){
           return NotFound();
        }
        _mapper.Map(commandUpdate,commandModelFromRepo);
        //_commandRepo.UpdateCommand(commandModelFromRepo);
        _commandRepo.SaveChanges();
        return NoContent();
      }
    }
}