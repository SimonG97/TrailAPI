using AutoMapper;
using TrailAPI.Dtos;
using TrailAPI.Models;

namespace TrailAPI.Profiles{
    public class CommandsProfile : Profile
    {
        public CommandsProfile()
      {
         CreateMap<CommandModel, CommandReadDto>();
         CreateMap<CommandCreateDto,CommandModel>();
         CreateMap<CommandUpdateDto,CommandModel>();
         CreateMap<CommandModel,CommandUpdateDto>();
      }

    }
}