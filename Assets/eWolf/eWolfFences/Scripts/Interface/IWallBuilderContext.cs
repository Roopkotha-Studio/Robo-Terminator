namespace eWolfFences.Interfaces
{
    public interface IWallBuilderContext
    {
        /// <summary>
        /// Gets the building units
        /// </summary>
        BuildingUnits BuildingUnitsContext
        {
            get;
        }

        /// <summary>
        /// gets the material maps for each wall type
        /// </summary>
        MaterialHolders MaterialHoldersContext
        {
            get;
        }

        /// <summary>
        /// gets the lighting options
        /// </summary>
        LightingOptions LightingHolderContext
        {
            get;
        }
    }
}
