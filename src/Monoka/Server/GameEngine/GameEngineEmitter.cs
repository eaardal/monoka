using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Akka.Actor;
using Monoka.Common.Infrastructure;
using Monoka.Common.Infrastructure.Extensions;
using Monoka.Common.Infrastructure.Logging.Contracts;
using Monoka.Common.Network;
using Monoka.Common.Network.Messages;
using Monoka.Server.Model;
using Monoka.Server.NetworkApi;

namespace Monoka.Server.GameEngine
{
    class GameEngineEmitter : LoggingReceiveActor
    {
        public GameEngineEmitter(ILogger log) : base(log)
        {
            //Receive<StartGame>(msg => OnStartGame(msg));
        }

        //private void OnStartGame(StartGame msg)
        //{
        //    try
        //    {
        //        var networkEmitter = Context.ActorFromIoC<NetworkEmitter>();

        //        var startGame = new FromServer.StartGame();

        //        var tellClientsToStartGame = new NetworkEmitter.TellClients(startGame, msg.Players.Select(p => p.Id), RemoteActorRegistry.Client.GameSessionApi);

        //        networkEmitter.Tell(tellClientsToStartGame);
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Msg(this, l => l.Error(ex));
        //        Sender.Tell(new Failure { Exception = ex }, Self);
        //    }
        //}

        //public class StartGame
        //{
        //    public IEnumerable<Player> Players { get; }

        //    public StartGame(ImmutableList<Player> players)
        //    {
        //        Players = players;
        //    }
        //}
    }
}
