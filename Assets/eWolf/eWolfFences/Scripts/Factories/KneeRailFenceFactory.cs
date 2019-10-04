using System;
using eWolfFences.Builders;
using eWolfFences.Interfaces;
using UnityEngine;

namespace eWolfFences.Factories
{
    public class KneeRailFenceFactory : IWallBuilderStrategy
    {
        public enum KneeRailFenceStyles
        {
            SmallSingle,
            MediumSingle,
        }

        /// <summary>
        /// gets the list of styles for this wall
        /// </summary>
        public string[] StyleList
        {
            get
            {
                return Enum.GetNames(typeof(KneeRailFenceStyles));
            }
        }

        /// <summary>
        /// The material for this type of wall
        /// </summary>
        public string Material
        {
            get
            {
                return "KneeRailFence";
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
            switch ((KneeRailFenceStyles)styleIndex)
            {
                case KneeRailFenceStyles.SmallSingle:
                    {
                        KneeRailBuilder bwb = new KneeRailBuilder(_meshBuilder, wallType);
                        bwb.Height = bu.Height * 0.33f;
                        bwb.SectionLength = bu.Width;
                        bwb.Thickness = bu.Height * 0.06f;
                        return bwb;
                    }
                case KneeRailFenceStyles.MediumSingle:
                    {
                        KneeRailBuilder bwb = new KneeRailBuilder(_meshBuilder, wallType);
                        bwb.Height = bu.Height * 0.50f;
                        bwb.SectionLength = bu.Width;
                        bwb.Thickness = bu.Height * 0.06f;
                        return bwb;
                    }
            }

            return null;
        }
        /*
        /// <summary>
        /// THe wooden fence Uv set
        /// </summary>
        /// <returns>UV set for the woodend fence</returns>
        private UVSet GetWoodenFenceUVs()
        {
            UVSet u = new UVSet();
            u.TL = new Vector2(0.001f, 1);
            u.TR = new Vector2(0.001f, 0.41f);

            u.BL = new Vector2(0.649f, 1);
            u.BR = new Vector2(0.649f, 0.41f);

            return u;
        }

        /// <summary>
        /// THe wooden get fence Uv set
        /// </summary>
        /// <returns>UV set for the woodend fence</returns>
        private UVSet GetWoodenFenceGateUVs()
        {
            UVSet u = new UVSet();
            u.TL = new Vector2(0.65f, 1);
            u.TR = new Vector2(0.65f, 0.38f);

            u.BL = new Vector2(0.99f, 1);
            u.BR = new Vector2(0.99f, 0.38f);

            return u;
        }*/

        /// <summary>
        /// The Feature
        /// </summary>
        private IUseFeature _feature = new KneeRailFeatureDetails();
    }
}
