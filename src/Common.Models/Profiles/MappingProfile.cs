using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Common.Models.Contract;
using Common.Models.Contract.AnimalFiveHead;
using Common.PlayingCards.Models;
using Game.AnimalFiveHead.Player;

namespace Common.Models.Profiles
{
  // https://docs.automapper.org/en/stable/Getting-started.html
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<PlayCard, GameCard>();
      CreateMap<GameCard, PlayCard>();

      CreateMap<BasePlayer, NpcPlayerResult>();
      CreateMap<BasePlayer, RealPlayerResult>();
    }
  }
}
