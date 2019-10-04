using System;
using eWolfFences.Builders;
using eWolfFences.Interfaces;

namespace eWolfFences.Factories
{
    public class BrickWallFactory : IWallBuilderStrategy
    {
        public enum BrickWallOptions
        {
            Large,
            Medium,
            Small,
            LargeAged,
            MediumAged,
            SmallAged,
        }

        /// <summary>
        /// gets the list of styles for this wall
        /// </summary>
        public string[] StyleList
        {
            get
            {
                return Enum.GetNames(typeof(BrickWallOptions));
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

            float unkeptScale = 0;
            float tapur = 0;
            float width = bu.Width * 0.15f;

            if ((BrickWallOptions)styleIndex == BrickWallOptions.LargeAged ||
                (BrickWallOptions)styleIndex == BrickWallOptions.MediumAged ||
                (BrickWallOptions)styleIndex == BrickWallOptions.SmallAged)
            {
                unkeptScale = 55;
                tapur = bu.Width * 0.01f;
                width = bu.Width * 0.25f;
            }

            switch ((BrickWallOptions)styleIndex)
            {
                case BrickWallOptions.Large:
                case BrickWallOptions.LargeAged:
                    {
                        BrickWallBuilder bwb = new BrickWallBuilder(_meshBuilder, wallType);
                        bwb.WallWidth = width;
                        bwb.WallHeight = bu.Height;
                        bwb.WallLength = bu.Width;
                        bwb.WallUVHeight = 1;
                        bwb.Unkept = unkeptScale;
                        bwb.Tapur = tapur;
                        return bwb;
                    }
                case BrickWallOptions.Medium:
                case BrickWallOptions.MediumAged:
                    {
                        BrickWallBuilder bwb = new BrickWallBuilder(_meshBuilder, wallType);
                        bwb.WallWidth = width;
                        bwb.WallHeight = bu.Height * 0.65f;
                        bwb.WallLength = bu.Width;
                        bwb.WallUVHeight = 0.75f;
                        bwb.Unkept = unkeptScale;
                        bwb.Tapur = tapur;
                        return bwb;
                    }
                case BrickWallOptions.Small:
                case BrickWallOptions.SmallAged:
                    {
                        BrickWallBuilder bwb = new BrickWallBuilder(_meshBuilder, wallType);
                        bwb.WallWidth = width;
                        bwb.WallHeight = bu.Height * 0.45f;
                        bwb.WallUVHeight = 0.45f;
                        bwb.WallLength = bu.Width;
                        bwb.Unkept = unkeptScale;
                        bwb.Tapur = tapur;
                        return bwb;
                    }
            }
            return null;
        }

        private IUseFeature _feature = new BrickWallFeatureDetails();
    }
}
