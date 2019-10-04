using eWolfFences.Builders;

namespace eWolfFences.Interfaces
{
    public interface IWallBuilderStrategy
    {
        /// <summary>
        /// gets the list of styles for this wall
        /// </summary>
        string[] StyleList { get; }

        /// <summary>
        /// The material for this type of wall
        /// </summary>
        string Material { get; }

        /// <summary>
        /// Get the feature details
        /// </summary>
        IUseFeature GetFeatureDetails { get; }

        /// <summary>
        /// Creates the builder form the type and style
        /// </summary>
        /// <param name="_meshBuilder">The mesh builder to use</param>
        /// <param name="wallType">The type of wall</param>
        /// <param name="styleIndex">The style index of the wall</param>
        /// <param name="wallBuilderContext">The wall builder context</param>
        /// <returns>The builder object for the wall</returns>
        WallBuilderBase BuilderFactory(WallMeshBuilder _meshBuilder, WallDetails.WallTypes wallType, int styleIndex, IWallBuilderContext wallBuilderContext);
    }
}
