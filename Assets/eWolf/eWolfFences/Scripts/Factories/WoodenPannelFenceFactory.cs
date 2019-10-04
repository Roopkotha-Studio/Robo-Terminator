using System;
using eWolfFences.Builders;
using eWolfFences.Interfaces;
using UnityEngine;

namespace eWolfFences.Factories
{
    public class WoodenPannelFenceFactory : IWallBuilderStrategy
    {
        public enum WoodenPannelFenceStyles
        {
            Large,
            Medium,
        }

        /// <summary>
        /// gets the list of styles for this wall
        /// </summary>
        public string[] StyleList
        {
            get
            {
                return Enum.GetNames(typeof(WoodenPannelFenceStyles));
            }
        }

        /// <summary>
        /// The material for this type of wall
        /// </summary>
        public string Material
        {
            get
            {
                return "WoodenPannels";
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
            switch ((WoodenPannelFenceStyles)styleIndex)
            {
                case WoodenPannelFenceStyles.Large:
                    {
                        PannelWallBuilder bwb = new PannelWallBuilder(_meshBuilder, wallType);
                        bwb.PannelHeight = bu.Height;
                        bwb.PannelLength = bu.Width;
                        bwb.PannelGateHeight = bwb.PannelHeight * 1.06f;
                        return bwb;
                    }
                case WoodenPannelFenceStyles.Medium:
                    {
                        PannelWallBuilder bwb = new PannelWallBuilder(_meshBuilder, wallType);
                        bwb.PannelHeight = bu.Height * 0.65f;
                        bwb.PannelLength = bu.Width;
                        bwb.PannelGateHeight = bwb.PannelHeight * 1.06f;
                        return bwb;
                    }
            }

            return null;
        }
        private IUseFeature _feature = new GateFeatureDetails();
    }
}
