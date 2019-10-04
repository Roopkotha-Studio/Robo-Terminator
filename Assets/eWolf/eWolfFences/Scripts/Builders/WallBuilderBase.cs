using UnityEngine;

namespace eWolfFences.Builders
{
    public class WallBuilderBase
    {
        /// <summary>
        /// The uvs to use
        /// </summary>
        // public UVSet UVset;

        /// <summary>
        /// The standard constructor
        /// </summary>
        /// <param name="meshBuilder">The mesh builder</param>
        /// <param name="wallType">The type of wall</param>
        public WallBuilderBase(WallMeshBuilder meshBuilder, WallDetails.WallTypes wallType)
        {
            _meshBuilder = meshBuilder;
            _wallType = wallType;
        }

        #region Public Properties
        /// <summary>
        /// Gets the mesh builder
        /// </summary>
        public WallMeshBuilder MeshBuilders
        {
            get { return _meshBuilder; }
        }

        /// <summary>
        /// Gets the wall type
        /// </summary>
        public WallDetails.WallTypes WallType
        {
            get { return _wallType; }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Build the wall
        /// </summary>
        /// <param name="wd">The wall data to use</param>
        /// <param name="gameObject">The game object</param>
        public virtual void BuildWall(WallData wd, GameObject gameObject)
        {

        }
        #endregion

        #region Private Fields
        private WallMeshBuilder _meshBuilder;
        private WallDetails.WallTypes _wallType;
        #endregion
    }
}
