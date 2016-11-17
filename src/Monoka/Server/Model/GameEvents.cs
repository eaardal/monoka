namespace Monoka.Server.Model
{
    internal class GameEvents
    {
        internal class UnitKilled
        {
            public UnitKilled(GameUnit gameUnit)
            {
                GameUnit = gameUnit;
            }

            public GameUnit GameUnit { get;  }
        }

        internal class UnitPurchased
        {
            public UnitPurchased(GameUnit gameUnit)
            {
                GameUnit = gameUnit;
            }

            public GameUnit GameUnit { get; }
        }

        internal class BuildingPurchased
        {
            public BuildingPurchased(int cost)
            {
                Cost = cost;
            }

            public int Cost { get;  }
        }

        internal class UnitPurchaseCanceled
        {
        }
    }
}