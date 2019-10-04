using System;
using eWolfFences.Builders;
using eWolfFences.Interfaces;

namespace eWolfFences.Factories
{
    public class BrickTowersWallFactory : IWallBuilderStrategy
    {
        public enum BrickTowersWallOptions
        {
            SmallWallSmallTowers,
            SmallWallTinyTowers
        }

        /// <summary>
        /// gets the list of styles for this wall
        /// </summary>
        public string[] StyleList
        {
            get
            {
                return Enum.GetNames(typeof(BrickTowersWallOptions));
            }
        }

        /// <summary>
        /// The material for this type of wall
        /// </summary>
        public string Material
        {
            get
            {
                return "RedBrickWall";
            }
        }

        /// <summary>
        /// Get the feature details
        /// </summary>
        public IUseFeature GetFeatureDetails
        {
            get
            {
                return _feature;
            }
        }

        /// <summary>
        /// Creates the builder form the type and style
        /// </summary>
        /// <param name="_meshBuilder">The mesh builder to use</param>
        /// <param name="wallType">The type of wall</param>
        /// <param name="styleIndex">The style index of the wall</param>
        /// <param name="wallBuilderContext">The wall builder context</param>
        /// <returns>The builder object for the wall</returns>
        public WallBuilderBase BuilderFactory(WallMeshBuilder _meshBuilder, WallDetails.WallTypes wallType, int styleIndex, IWallBuilderContext wallBuilderContext)
        {
            BuildingUnits bu = wallBuilderContext.BuildingUnitsContext;
            switch ((BrickTowersWallOptions)styleIndex)
            {
                case BrickTowersWallOptions.SmallWallSmallTowers:
                    {
                        BrickWallTowersBuilder bwb = new BrickWallTowersBuilder(_meshBuilder, wallType, bu);
                        bwb.WallWidth = bu.Width * 0.15f;
                        bwb.WallHeight = bu.Height * 0.45f;
                        bwb.WallUVHeight = 0.45f;
                        bwb.WallLength = bu.Width;
                        bwb.WallTowerHeight = bu.Height * 0.45f;
                        return bwb;
                    }
                case BrickTowersWallOptions.SmallWallTinyTowers:
                    {
                        BrickWallTowersBuilder bwb = new BrickWallTowersBuilder(_meshBuilder, wallType, bu);
                        bwb.WallWidth = bu.Width * 0.15f;
                        bwb.WallHeight = bu.Height * 0.45f;
                        bwb.WallUVHeight = 0.45f;
                        bwb.WallLength = bu.Width;
                        bwb.WallTowerHeight = bu.Height * 0.25f;
                        return bwb;
                    }
            }

            return null;
        }

        private IUseFeature _feature = new BrickWallFeatureDetails();
    }
}
