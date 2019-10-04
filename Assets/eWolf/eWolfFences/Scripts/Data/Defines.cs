namespace eWolfFences
{
    /// <summary>
    /// The UV set
    /// </summary>
    public enum TextUV
    {
        GroundFloorWall,
        SecondFloorWall,

        WallEndSlope,
        WallEndSlopePoint,
        WallEndSlopeMiddle,

        GroundFloorWindow,
        SecondFloorWindow,

        Door_A,
        Door_B,
        Door_C,

        RoofTiles,
        RoofTilesTop,
        FlatRoofMiddle,
        RoofTilesTopPoint,
        
        GarageDoors,
        GarageRoof,
    }

    /// <summary>
    /// The sytle of the roof
    /// </summary>
    public enum RoofStyle
    {
        Sloping,
        Flat,
    }

    /// <summary>
    /// The pre-set style for the random values
    /// </summary>
    public enum BuildingStyle
    {
        None,
        VillageHouse,
        TownHouse,
        TowerBlock,
    }
}
