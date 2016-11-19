using System;
using Autofac;
using AutoMapper;
using Monoka.Client.GameLobby.Results;
using Monoka.Client.Model;
using Monoka.Common.Dto;
using Monoka.Common.Network.Messages;

namespace Monoka.Client.Startup
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

        private static void Configure(IProfileExpression config)
        {
            MapNetworkMessages(config);
            MapDtos(config);
        }

        private static void MapNetworkMessages(IProfileExpression config)
        {
            config.CreateMap<FromServer.JoinGameLobbyResult, JoinGameLobbyResult>();
            config.CreateMap<FromServer.HandshakeResult, HandshakeResult>();
            config.CreateMap<FromServer.IsAnyLobbyLookingForPlayersResult, LookingForPlayersResult>();
        }

        private static void MapDtos(IProfileExpression config)
        {
            config.CreateMap<PlayerDto, Player>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Color, opt => opt.Ignore())
                .ForMember(dest => dest.IsLocalPlayer, opt => opt.Ignore())
                .ForMember(dest => dest.Nr, opt => opt.Ignore())
                .ReverseMap();

            config.CreateMap<GameLobby.GameLobby, GameLobbyDto>().ReverseMap();
        }
    }
}
