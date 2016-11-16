using Autofac;
using AutoMapper;

namespace Monoka.Server.Startup
{
    internal static class AutoMapperBootstrapper
    {
        public static void Wire(IContainer container)
        {
            var config = new MapperConfiguration(Configure);

            config.AssertConfigurationIsValid();

            var mapper = config.CreateMapper();
            
            var builder = new ContainerBuilder();

            builder.RegisterInstance(mapper)
                .AsSelf()
                .AsImplementedInterfaces()
                .SingleInstance();
            
            builder.Update(container);
        }

        private static void Configure(IMapperConfigurationExpression config)
        {
            //MapNetworkMessages(config);
            //MapDtos(config);
        }

        //private static void MapNetworkMessages(IMapperConfigurationExpression config)
        //{
        //    config.CreateMap<FromClient.JoinGameLobby, GameLobbyManager.JoinGameLobby>()
        //        .ForMember(dest => dest.LobbyId, opt => opt.MapFrom(src => src.LobbyId))
        //        .ForMember(dest => dest.PlayerId, opt => opt.MapFrom(src => src.PlayerId))
        //        .ForMember(dest => dest.AsAdmin, opt => opt.Ignore());

        //    config.CreateMap<GameLobbyManager.JoinGameLobbyResult, FromServer.JoinGameLobbyResult>();
        //    config.CreateMap<FromClient.PlayerReady, GameLobbyManager.PlayerReady>();
        //    config.CreateMap<FromClient.IsAnyLobbyLookingForPlayers, GameLobbyManager.IsAnyLobbyLookingForPlayers>();
        //    config.CreateMap<FromClient.DonePreparing, GameSession.PlayerFinishedLoading>()
        //        .ForSourceMember(src => src.GameSessionId, opt => opt.Ignore());
        //}

        //private static void MapDtos(IMapperConfiguration config)
        //{
        //    config.CreateMap<GameLobby, GameLobbyDto>()
        //        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
        //        .ForMember(dest => dest.Players, opt => opt.MapFrom(src => src.Players))
        //        .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
        //        .ReverseMap();

        //    config.CreateMap<PlayerDto, GameLobbyPlayer>().ReverseMap();

        //    config.CreateMap<PlayerDto, Player>()
        //        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
        //        .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
        //        .ForSourceMember(src => src.IsReady, opt => opt.Ignore())
        //        .ForSourceMember(src => src.IsAdmin, opt => opt.Ignore())
        //        .ReverseMap();

        //    config.CreateMap<GameLobbyPlayer, Player>()
        //        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
        //        .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
        //        .ForSourceMember(src => src.IsAdmin, opt => opt.Ignore())
        //        .ForSourceMember(src => src.IsReady, opt => opt.Ignore())
        //        .ReverseMap();

        //    config.CreateMap<GameSessionPlayer, Player>()
        //        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
        //        .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
        //        .ForSourceMember(src => src.FinishedLoading, opt => opt.Ignore())
        //        .ReverseMap();
        //}
    }
}
