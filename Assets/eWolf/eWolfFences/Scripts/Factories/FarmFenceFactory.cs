using System;
using eWolfFences.Builders;
using eWolfFences.Interfaces;

namespace eWolfFences.Factories
{
    public class FarmFenceFactory : IWallBuilderStrategy
    {
        public enum FarmFenceOptions
        {
            DoubleRailsMedium,
            DoubleRailsMediumAged,
        }

        /// <summary>
        /// gets the list of styles for this wall
        /// </summary>
        public string[] StyleList
        {
            get
            {
                return Enum.GetNames(typeof(FarmFenceOptions));
            }
        }

        /// <summary>
        /// The material for this type of wall
        /// </summary>
        public string Material
        {
            get
            {
                return "FarmFence";
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

            switch ((FarmFenceOptions)styleIndex)
            {
                case FarmFenceOptions.DoubleRailsMedium:
                    {
                        FarmFenceBuilder bwb = new FarmFenceBuilder(_meshBuilder, wallType);
                        bwb.WallHeight = bu.Height * 0.65f;
                        bwb.SectionLength = bu.Width;
                        bwb.PostThinkness = bu.Height * 0.1f;
                        bwb.PostDepth = bwb.PostThinkness * 0.66f;
                        return bwb;
                    }
                case FarmFenceOptions.DoubleRailsMediumAged:
                    {
                        FarmFenceBuilder bwb = new FarmFenceBuilder(_meshBuilder, wallType);
                        bwb.WallHeight = bu.Height * 0.65f;
                        bwb.SectionLength = bu.Width;
                        bwb.PostThinkness = bu.Height * 0.1f;
                        bwb.PostDepth = bwb.PostThinkness * 0.66f;
                        bwb.Aged = true;
                        return bwb;
                    }
            }
            return null;
        }

        private IUseFeature _feature = new FarmFenceFeatureDetails();
    }
}
