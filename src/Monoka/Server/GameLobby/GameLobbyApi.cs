using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Akka.Actor;
using Monoka.Common.Dto;
using Monoka.Common.Infrastructure;
using Monoka.Common.Infrastructure.Adapters;
using Monoka.Common.Infrastructure.Extensions;
using Monoka.Common.Infrastructure.Logging.Contracts;
using Monoka.Common.Network.Messages;

namespace Monoka.Server.GameLobby
{
    class GameLobbyApi : LoggingReceiveActor
    {
        private readonly IAutoMapperAdapter _mapper;
        private readonly IActorRef _gameLobbyManager;
        private readonly IActorRef _gameSessionManager;
        private readonly IActorRef _gameLobbyEmitter;

        public GameLobbyApi(ILogger log, IAutoMapperAdapter mapper) : base(log)
        {
            if (mapper == null) throw new ArgumentNullException(nameof(mapper));
            _mapper = mapper;
            
            _gameLobbyManager = Context.ActorFromIoC(ActorRegistry.GameLobbyManager);
            _gameSessionManager = Context.ActorFromIoC(ActorRegistry.GameSessionManager);
            _gameLobbyEmitter = Context.ActorFromIoC(ActorRegistry.GameLobbyEmitter);

            ReceiveAsync<FromClient.GetGameLobbies>(async msg => await OnGetGameLobbies(msg));
            ReceiveAsync<FromClient.CreateAndJoinGameLobby>(async msg => await OnCreateAndJoinGameLobby(msg));
            ReceiveAsync<FromClient.JoinGameLobby>(async msg => await OnJoinGameLobby(msg));
            ReceiveAsync<FromClient.PlayerReady>(async msg => await OnPlayerReady(msg));
            //ReceiveAsync<FromClient.PrepareGameScreen>(async msg => await OnStartGame(msg));
            ReceiveAsync<FromClient.IsAnyLobbyLookingForPlayers>(async msg => await OnIsAnyLobbyLookingForPlayers(msg));
        }

        private async Task OnIsAnyLobbyLookingForPlayers(FromClient.IsAnyLobbyLookingForPlayers message)
        {
            try
            {
                var isLookingForPlayers = _mapper.Map<FromClient.IsAnyLobbyLookingForPlayers, GameLobbyManager.IsAnyLobbyLookingForPlayers>(message);

                var answer = await _gameLobbyManager.Ask(isLookingForPlayers);

                if (answer is GameLobby)
                {
                    var gameLobby = answer as GameLobby;

                    var dto = _mapper.Map<GameLobby, GameLobbyDto>(gameLobby);

                    Sender.Tell(new FromServer.IsAnyLobbyLookingForPlayersResult(dto, true), Self);
                }

                if (answer == null)
                {
                    Sender.Tell(new FromServer.IsAnyLobbyLookingForPlayersResult(null, false), Self);
                }

                if (answer is Failure)
                {
                    var failure = answer as Failure;
                    Log.Msg(this, l => l.Error(failure.Exception));
                    Sender.Tell(failure, Self);
                }
            }
            catch (Exception ex)
            {
                Log.Msg(this, l => l.Error(ex));
                Sender.Tell(new Failure { Exception = ex }, Self);
            }
        }

        private async Task OnGetGameLobbies(FromClient.GetGameLobbies message)
        {
            try
            {
                var answer = await _gameLobbyManager.Ask(new GameLobbyManager.GetGameLobbies());

                if (answer is IEnumerable<GameLobby>)
                {
                    var gameLobbies = answer as IEnumerable<GameLobby>;

                    var dtos = _mapper.Map<IEnumerable<GameLobby>, IEnumerable<GameLobbyDto>>(gameLobbies);

                    Sender.Tell(new FromServer.GetGameLobbiesResult(dtos), Self);
                }

                if (answer is Failure)
                {
                    var failure = answer as Failure;
                    Log.Msg(this, l => l.Error(failure.Exception));
                    Sender.Tell(failure, Self);
                }
            }
            catch (Exception ex)
            {
                Log.Msg(this, l => l.Error(ex));
                Sender.Tell(new Failure { Exception = ex }, Self);
            }
        }

        private async Task OnCreateAndJoinGameLobby(FromClient.CreateAndJoinGameLobby message)
        {
            try
            {
                var createGameLobby = new GameLobbyManager.CreateNewGameLobby(message.GameLobbyName, message.PlayerId);

                var createAnswer = await _gameLobbyManager.Ask(createGameLobby);

                if (createAnswer is GameLobbyManager.CreatedNewGameLobby)
                {
                    var createResult = createAnswer as GameLobbyManager.CreatedNewGameLobby;

                    var joinAnswer =
                        await _gameLobbyManager.Ask(new GameLobbyManager.JoinGameLobby(message.PlayerId, createResult.GameLobby.Id, true));

                    if (joinAnswer is GameLobbyManager.JoinGameLobbyResult)
                    {
                        var joinResult = joinAnswer as GameLobbyManager.JoinGameLobbyResult;

                        var dto = _mapper.Map<GameLobby, GameLobbyDto>(createResult.GameLobby);

                        Sender.Tell(new FromServer.CreateAndJoinGameLobbyResult(dto, joinResult.Success));
                    }
                }

                if (createAnswer is Failure)
                {
                    var failure = createAnswer as Failure;
                    Log.Msg(this, l => l.Error(failure.Exception));
                    Sender.Tell(failure, Self);
                }
            }
            catch (Exception ex)
            {
                Log.Msg(this, l => l.Error(ex));
                Sender.Tell(new Failure { Exception = ex }, Self);
            }
        }

        private async Task OnJoinGameLobby(FromClient.JoinGameLobby message)
        {
            try
            {
                var joinLobby = _mapper.Map<FromClient.JoinGameLobby, GameLobbyManager.JoinGameLobby>(message);

                var answer = await _gameLobbyManager.Ask(joinLobby);

                if (answer is GameLobbyManager.JoinGameLobbyResult)
                {
                    var result = answer as GameLobbyManager.JoinGameLobbyResult;

                    var joinLobbyResult =
                        _mapper.Map<GameLobbyManager.JoinGameLobbyResult, FromServer.JoinGameLobbyResult>(result);

                    Sender.Tell(joinLobbyResult, Self);

                    _gameLobbyEmitter.Tell(new GameLobbyEmitter.PlayerJoinedGameLobby(message.PlayerId, result.GameLobby));
                }

                if (answer is Failure)
                {
                    var failure = answer as Failure;
                    Log.Msg(this, l => l.Error(failure.Exception));
                    Sender.Tell(failure, Self);
                }
            }
            catch (Exception ex)
            {
                Log.Msg(this, l => l.Error(ex));
                Sender.Tell(new Failure { Exception = ex }, Self);
            }
        }

        private async Task OnPlayerReady(FromClient.PlayerReady message)
        {
            try
            {
                var playerReady = _mapper.Map<FromClient.PlayerReady, GameLobbyManager.PlayerReady>(message);

                var answer = await _gameLobbyManager.Ask(playerReady);

                if (answer is GameLobby)
                {
                    var gameLobby = answer as GameLobby;

                    var dto = _mapper.Map<GameLobby, GameLobbyDto>(gameLobby);

                    Sender.Tell(new FromServer.GameLobbyUpdated(dto), Self);
                }

                if (answer is Failure)
                {
                    var failure = answer as Failure;
                    Log.Msg(this, l => l.Error(failure.Exception));
                    Sender.Tell(failure, Self);
                }
            }
            catch (Exception ex)
            {
                Log.Msg(this, l => l.Error(ex));
                Sender.Tell(new Failure { Exception = ex }, Self);
            }
        }

        //private async Task OnStartGame(FromClient.PrepareGameScreen message)
        //{
        //    try
        //    {
        //        var answer = await _gameLobbyManager.Ask(new GetGameLobby(message.GameLobbyId));

        //        if (answer is GetGameLobbyResult)
        //        {
        //            var gameLobby = (answer as GetGameLobbyResult).GameLobby;

        //            _gameSessionManager.Tell(new Internal.NewSession(gameLobby));
        //        }
                
        //        if (answer is Failure)
        //        {
        //            var failure = answer as Failure;
        //            Log.Msg(this, l => l.Error(failure.Exception));
        //            Sender.Tell(failure, Self);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Msg(this, l => l.Error(ex));
        //        Sender.Tell(new Failure { Exception = ex }, Self);
        //    }
        //}
        
        //private async Task BroadcastGameLobbies()
        //{
        //    try
        //    {
        //        var answer = await _gameLobbyManager.Ask(new GetGameLobbies());

        //        if (answer is IEnumerable<GameLobbyDto>)
        //        {
        //            var gameLobbies = answer as IEnumerable<GameLobbyDto>;

        //            Sender.Tell(new FromServer.AvailableGameLobbies(gameLobbies), Self);
        //        }

        //        if (answer is Failure)
        //        {
        //            var failure = answer as Failure;
        //            Log.Msg(this, l => l.Error(failure.Exception));
        //            Sender.Tell(failure, Self);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Msg(this, l => l.Error(ex));
        //        Sender.Tell(new Failure { Exception = ex }, Self);
        //    }
        //}
    }
}
