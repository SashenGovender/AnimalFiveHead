using System.Linq;
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

      CreateMap<BasePlayer, NpcPlayerResult>()
      .ForMember(dest => dest.PlayerType, option => option.MapFrom(src => src.PlayerId));

      CreateMap<BasePlayer, RealPlayerResult>();

      //CreateMap<NpcKeeperPlayer, NpcPlayerResult>()
      //  .ForMember(dest => dest.Score, option => option.MapFrom(src => src.Score))
      //  .ForMember(dest => dest.Cards, option => option.MapFrom(src => src.Cards.Select(x => new GameCard
      //  {
      //    CardId = x.CardId,
      //    Face = x.Face,
      //    Rank = x.Rank,
      //    Type = x.Type,
      //    Value = x.Value
      //  })));
    }
  }
}
