using System;
using eWolfFences.Builders;
using eWolfFences.Interfaces;
using UnityEngine;

namespace eWolfFences.Factories
{
    class MetalRailingFactory : IWallBuilderStrategy
    {
        public enum MetalRailingFenceStyles
        {
            YellowTipsLarge,
            YellowTipsMedium,
            YellowTipsMediumLargeGate,
        }

        /// <summary>
        /// gets the list of styles for this wall
        /// </summary>
        public string[] StyleList
        {
            get
            {
                return Enum.GetNames(typeof(MetalRailingFenceStyles));
            }
        }

        /// <summary>
        /// The material for this type of wall
        /// </summary>
        public string Material
        {
            get
            {
                return "MetailRailings";
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
            switch ((MetalRailingFenceStyles)styleIndex)
            {
                case MetalRailingFenceStyles.YellowTipsLarge:
                    {
                        PannelWallBuilder bwb = new PannelWallBuilder(_meshBuilder, wallType);
                        bwb.PannelHeight = bu.Height;
                        bwb.PannelLength = bu.Width;
                        bwb.PannelGateHeight = bwb.PannelHeight;
                        return bwb;
                    }
                case MetalRailingFenceStyles.YellowTipsMedium:
                    {
                        PannelWallBuilder bwb = new PannelWallBuilder(_meshBuilder, wallType);
                        bwb.PannelHeight = bu.Height * 0.65f;
                        bwb.PannelLength = bu.Width;
                        bwb.PannelGateHeight = bwb.PannelHeight;
                        return bwb;
                    }
                case MetalRailingFenceStyles.YellowTipsMediumLargeGate:
                    {
                        PannelWallBuilder bwb = new PannelWallBuilder(_meshBuilder, wallType);
                        bwb.PannelHeight = bu.Height * 0.65f;
                        bwb.PannelLength = bu.Width;
                        bwb.PannelGateHeight = bu.Height;
                        return bwb;
                    }
            }

            return null;
        }

        private IUseFeature _feature = new MetalRailingFeatureDetails();
    }
}
