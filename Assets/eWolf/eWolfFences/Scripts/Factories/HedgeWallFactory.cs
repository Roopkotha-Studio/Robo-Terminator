using System;
using eWolfFences.Builders;
using eWolfFences.Interfaces;

namespace eWolfFences.Factories
{
    public class HedgeWallFactory : IWallBuilderStrategy
    {
        public enum HedgeOptions
        {
            Large,
            Medium,
            Small,
            LargeUnkept,
            MediumUnkept,
            SmallUnkept
        }

        /// <summary>
        /// gets the list of styles for this wall
        /// </summary>
        public string[] StyleList
        {
            get
            {
                return Enum.GetNames(typeof(HedgeOptions));
            }
        }

        /// <summary>
        /// The material for this type of wall
        /// </summary>
        public string Material
        {
            get
            {
                return "GreenHedgeWall";
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

            if ((HedgeOptions)styleIndex == HedgeOptions.LargeUnkept ||
                (HedgeOptions)styleIndex == HedgeOptions.MediumUnkept ||
                (HedgeOptions)styleIndex == HedgeOptions.SmallUnkept)
            {
                unkeptScale = 55;
            }

            switch ((HedgeOptions)styleIndex)
            {
                case HedgeOptions.Large:
                case HedgeOptions.LargeUnkept:
                    {
                        BrickWallBuilder bwb = new BrickWallBuilder(_meshBuilder, wallType);
                        bwb.WallWidth = bu.Width * 0.40f;
                        bwb.WallHeight = bu.Height;
                        bwb.WallLength = bu.Width;
                        bwb.WallUVHeight = 1;
                        bwb.Tapur = bu.Width * 0.06f;
                        bwb.Unkept = unkeptScale;
                        return bwb;
                    }
                case HedgeOptions.Medium:
                case HedgeOptions.MediumUnkept:
                    {
                        BrickWallBuilder bwb = new BrickWallBuilder(_meshBuilder, wallType);
                        bwb.WallWidth = bu.Width * 0.30f;
                        bwb.WallHeight = bu.Height * 0.65f;
                        bwb.WallLength = bu.Width;
                        bwb.WallUVHeight = 0.75f;
                        bwb.Tapur = bu.Width * 0.02f;
                        bwb.Unkept = unkeptScale;
                        return bwb;
                    }
                case HedgeOptions.Small:
                case HedgeOptions.SmallUnkept:
                    {
                        BrickWallBuilder bwb = new BrickWallBuilder(_meshBuilder, wallType);
                        bwb.WallWidth = bu.Width * 0.25f;
                        bwb.WallHeight = bu.Height * 0.45f;
                        bwb.WallLength = bu.Width;
                        bwb.WallUVHeight = 0.35f;
                        bwb.Tapur = bu.Width * 0.01f;
                        bwb.Unkept = unkeptScale;
                        return bwb;
                    }
            }

            return null;
        }

        private IUseFeature _feature = new HedgeFeatureDetails();
    }
}
