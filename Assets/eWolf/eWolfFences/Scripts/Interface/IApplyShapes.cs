namespace eWolfFences.Interfaces
{
    public interface IApplyShapes
    {
        bool BottomCap
        {
            set;
        }

        bool TopCap
        {
            set;
        }

        void ApplyAgePost(float age);

        void CreateApplyShape(WallMeshBuilder meshBuilders, CuboidMeshUVs cuboidMeshUVs);
    }
}
