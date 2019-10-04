namespace eWolfFences.Interfaces
{
    /// <summary>
    /// Create mesh
    /// </summary>
    public interface ICreateMesh
    {
        /// <summary>
        /// Create the mesh for this object
        /// </summary>
        /// <param name="wallBuilderContext">The wall builder context</param>
        void CreateMesh(IWallBuilderContext builderUnits);
    }
}
