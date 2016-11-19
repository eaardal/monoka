using System;
using Autofac;
using AutoMapper;
using Monoka.Common.Dto;
using Monoka.Common.Network.Messages;
using Monoka.Server.GameLobby;
using Monoka.Server.GameSession;
using Monoka.Server.Model;

namespace Monoka.Server.Startup
{
    internal static class AutoMapperBootstrapper
    {
        public static void Wire(IContainer container, Action<IMapperConfigurationExpression> configureMapping)
        {
            var config = new MapperConfiguration(cfg =>
            {
                Configure(cfg);
                configureMapping?.Invoke(cfg);
            });
            
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
            MapNetworkMessages(config);
            MapDtos(config);
        }

        private static void MapNetworkMessages(IMapperConfigurationExpression config)
        {
            config.CreateMap<FromClient.JoinGameLobby, GameLobbyManager.JoinGameLobby>()
                .ForMember(dest => dest.LobbyId, opt => opt.MapFrom(src => src.LobbyId))
                .ForMember(dest => dest.PlayerId, opt => opt.MapFrom(src => src.PlayerId))
                .ForMember(dest => dest.AsAdmin, opt => opt.Ignore());

            config.CreateMap<GameLobbyManager.JoinGameLobbyResult, FromServer.JoinGameLobbyResult>();
            config.CreateMap<FromClient.PlayerReady, GameLobbyManager.PlayerReady>();
            config.CreateMap<FromClient.IsAnyLobbyLookingForPlayers, GameLobbyManager.IsAnyLobbyLookingForPlayers>();
            config.CreateMap<FromClient.DonePreparing, GameSession.GameSession.PlayerFinishedLoading>()
                .ForSourceMember(src => src.GameSessionId, opt => opt.Ignore());
        }

        private static void MapDtos(IMapperConfigurationExpression config)
        {
            config.CreateMap<GameLobby.GameLobby, GameLobbyDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Players, opt => opt.MapFrom(src => src.Players))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ReverseMap();

            config.CreateMap<PlayerDto, GameLobbyPlayerDto>().ReverseMap();

            config.CreateMap<PlayerDto, Player>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForSourceMember(src => src.IsReady, opt => opt.Ignore())
                .ForSourceMember(src => src.IsAdmin, opt => opt.Ignore())
                .ReverseMap();

            config.CreateMap<GameLobbyPlayerDto, Player>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForSourceMember(src => src.IsAdmin, opt => opt.Ignore())
                .ForSourceMember(src => src.IsReady, opt => opt.Ignore())
                .ReverseMap();

            config.CreateMap<GameSessionPlayerDto, Player>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForSourceMember(src => src.FinishedLoading, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
