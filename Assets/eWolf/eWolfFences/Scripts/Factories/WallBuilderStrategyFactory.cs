using eWolfFences.Interfaces;
using UnityEngine;

namespace eWolfFences.Factories
{
    /// <summary>
    /// Create the strategy for the wall factory
    /// </summary>
    public class WallBuilderStrategyFactory
    {
        public static IWallBuilderStrategy Create(WallDetails.WallTypes wallType)
        {
            if (wallType == WallDetails.WallTypes.BrickWall)
                return new BrickWallFactory();

            if (wallType == WallDetails.WallTypes.Hedge)
                return new HedgeWallFactory();

            if (wallType == WallDetails.WallTypes.BrickWallWithTowers)
                return new BrickTowersWallFactory();

            if (wallType == WallDetails.WallTypes.WoodenFence)
                return new WoodenPannelFenceFactory();
            
            if (wallType == WallDetails.WallTypes.KneeRailFence)
                return new KneeRailFenceFactory();

            if (wallType == WallDetails.WallTypes.MetalRailings)
                return new MetalRailingFactory();

            if (wallType == WallDetails.WallTypes.FarmFence)
                return new FarmFenceFactory();

            if (wallType == WallDetails.WallTypes.PicketFence)
                return new PicketFenceFactory();

            Debug.Log("Wall Type Missing " + wallType);
            return null;
        }
    }
}