//using System;
//using System.Collections.Generic;
//using System.Collections.Immutable;
//using Akka.Actor;
//using Monoka.Common.Infrastructure;
//using Monoka.Common.Infrastructure.Adapters;
//using Monoka.Common.Infrastructure.Extensions;
//using Monoka.Common.Infrastructure.Logging.Contracts;
//using Monoka.Server.Model;

//namespace Monoka.Server.GameEngine
//{
//    class GameEngine : LoggingReceiveActor
//    {
//        private ImmutableList<Player> _players;
//        private ImmutableList<PlayerState> _playerState;
//        private readonly IAutoMapperAdapter _mapper;
//        private Guid _gameSessionId;
//        private readonly IActorRef _gameEngineEmitter;

//        public GameEngine(ILogger log, IAutoMapperAdapter mapper) : base(log)
//        {
//            if (mapper == null) throw new ArgumentNullException(nameof(mapper));
//            _mapper = mapper;

//            _players = ImmutableList<Player>.Empty;
//            _playerState = ImmutableList<PlayerState>.Empty;

//            _gameEngineEmitter = Context.System.ActorFromIoC<GameEngineEmitter>();
            
//            Become(Idle);
//        }

//        private void Idle()
//        {
//            Receive<StartGame>(msg => OnStartGame(msg));
//        }

//        private void OnStartGame(StartGame msg)
//        {
//            _gameSessionId = msg.SessionId;
            
//            InitializeGame(msg.Players);

//            _gameEngineEmitter.Tell(new GameEngineEmitter.StartGame(_players));
//        }
        
//        private void InitializeGame(IEnumerable<Player> players)
//        {
//            foreach (var player in players)
//            {
//                //var bank = Context.ActorFromIoC(ActorRegistry.Bank);
//                //bank.Tell(new Bank.CreateAccount());

//                //var powerBar = Context.ActorFromIoC(ActorRegistry.PowerBar);
//                //powerBar.Tell(new PowerBar.CreateNew(player.Id));

//                //var buildingRegistry = Context.ActorFromIoC(ActorRegistry.BuildingRegistry);
//                //buildingRegistry.Tell(new BuildingRegistry.CreateInitialBuildings(player.Id));

//                //_playerState = _playerState.Add(new PlayerState
//                //{
//                //    Player = player,
//                //    PowerBar = powerBar,
//                //    BuildingRegistry = buildingRegistry,
//                //    Bank = bank
//                //});
//            }
//        }

//        #region Messages

//        public class StartGame
//        {
//            public IEnumerable<Player> Players { get; }
//            public Guid SessionId { get; }

//            public StartGame(IEnumerable<Player> players, Guid sessionId)
//            {
//                Players = players;
//                SessionId = sessionId;
//            }
//        }

//        #endregion
//    }
//}
