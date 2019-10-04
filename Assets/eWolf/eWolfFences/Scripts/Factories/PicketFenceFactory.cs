using System;
using eWolfFences.Builders;
using eWolfFences.Interfaces;

namespace eWolfFences.Factories
{
    public class PicketFenceFactory : IWallBuilderStrategy
    {
        public enum PicketFenceOptions
        {
            PicketA,
        }

        /// <summary>
        /// gets the list of styles for this wall
        /// </summary>
        public string[] StyleList
        {
            get
            {
                return Enum.GetNames(typeof(PicketFenceOptions));
            }
        }

        /// <summary>
        /// The material for this type of wall
        /// </summary>
        public string Material
        {
            get
            {
                return "Picketfence";
            }
        }

        /// <summary>
        /// Get the feature details
        /// </summary>
        public IUseFeature GetFeatureDetails
        {
            get
            {
                return null;
                //_feature;
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

            switch ((PicketFenceOptions)styleIndex)
            {
                case PicketFenceOptions.PicketA:
                    {
                        PicketFenceBuilder bwb = new PicketFenceBuilder(_meshBuilder, wallType);
                        bwb.PannelHeight = bu.Height * 0.65f;
                        bwb.SectionLength = bu.Width;
                        return bwb;
                    }
            }
            return null;
        }

        // private IUseFeature _feature = new FarmFenceFeatureDetails();
    }
}
