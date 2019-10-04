using eWolfFences.Builders;
using eWolfFences.Factories;
using eWolfFences.Interfaces;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif
using UnityEngine;

namespace eWolfFences
{
    public class WallDetails : MonoBehaviour, IUIDrawable, ICreateMesh
    {
        public enum WallTypes
        {
            BrickWall,
            Hedge,
            WoodenFence,
            BrickWallWithTowers,
            MetalRailings,
            KneeRailFence,            
            FarmFence,
            PicketFence,

            // Ideas to add
            // GroundPipes,
            // BrickWallWithButtress,
            // GuardRails - RoadsideSafetyBarrier - The W-Beam guardrail is the preferred semi-rigid roadside safety barrier worldwide
        }

        /// <summary>
        /// Whether or not to let the position update.
        /// </summary>
        public bool LockWall = false;

        /// <summary>
        /// Whether or not to automaticaly create the mesh each time its changed
        /// </summary>
        public bool AutoBuild = false;

        /// <summary>
        /// Whether or not to let the height change
        /// </summary>
        public bool LockHeight = false;

        /// <summary>
        /// THe wall type
        /// </summary>
        public WallTypes WallType
        {
            get { return _wallType; }
            set { _wallType = value; }
        }

        /// <summary>
        /// The wall style
        /// </summary>
        public int Style
        {
            get { return _version; }
            set { _version = value; }
        }

        /// <summary>
        /// Gets the center of the wall
        /// </summary>
        public Vector3 WallCenter
        {
            get
            {
                Vector3 start = _wallData.Start + transform.position;
                Vector3 end = _wallData.End + transform.position;
                return (start + end) / 2;
            }
        }

        /// <summary>
        /// Get the real world loction of the end of the wall
        /// </summary>
        public Vector3 WallEnd
        {
            get
            {
                return _wallData.End + transform.position;
            }

            set
            {
                _wallData.End = value - transform.position;
            }
        }

        /// <summary>
        /// Get the real world loction of the start of the wall
        /// </summary>
        public Vector3 WallStart
        {
            get
            {
                return _wallData.Start + transform.position;
            }

            set
            {
                _wallData.Start = value - transform.position;
            }
        }

        /// <summary>
        /// Gets the wall builder strategy
        /// </summary>
        public IWallBuilderStrategy WallBuilderStrategy
        {
            get
            {
                return _wallBuilderStrategy;
            }

            set
            {
                _wallBuilderStrategy = value;
                Dirty = true;
            }
        }

        /// <summary>
        /// Get a chained wall start
        /// </summary>
        public ChainedWall ChainedWallStart
        {
            get
            {
                return _chainedWallStart;
            }
        }

        /// <summary>
        /// Get the chained wall end
        /// </summary>
        public ChainedWall ChainedWallEnd
        {
            get
            {
                return _chainedWallEnd;
            }
        }

        void Start()
        {
        }

        /// <summary>
        /// Sets the material index to a random number.
        /// </summary>
        /// <param name="wallBuilderContext">The wall builder context</param>
        public void SetRandomMaterial(IWallBuilderContext wallBuilderContext)
        {
            _materialIndex = wallBuilderContext.MaterialHoldersContext.GetRandomIndex(WallType);
        }

        /// <summary>
        /// Set up a default line
        /// </summary>
        public void SetUpLine()
        {
            _wallData.Start = new Vector3(10, 0, 0);
            _wallData.End = new Vector3(-10, 0, 0);
        }
        
        /// <summary>
        /// The wall data, eg the start and end points
        /// </summary>
        public WallData WallData
        {
            get
            {
                return _wallData;
            }
        }

        /// <summary>
        /// Whether the wall is dirty and need to be re-created
        /// </summary>
        private bool Dirty
        {
            get
            {
                return _dirty;
            }

            set
            {
                _dirty = value;
#if UNITY_EDITOR
                if (value)
                    EditorUtility.SetDirty(this);
#endif
            }
        }

        /// <summary>
        /// Gets the material index
        /// </summary>
        public int MaterialIndex
        {
            get
            {
                return _materialIndex;
            }
            set
            {
                _materialIndex = value;
            }
        }

        /// <summary>
        /// Delete this wall
        /// </summary>
        public void Delete()
        {
            DestroyImmediate(gameObject);
        }

#if UNITY_EDITOR
        /// <summary>
        /// Draw the lines
        /// </summary>
        /// <param name="position">The parent position</param>
        /// <param name="rotation">The parent rotation</param>
        private void Draw(Vector3 position, Quaternion rotation)
        {
            if (LockWall)
                return;

            Vector3 _topLeft = WallData.Start + position;
            Vector3 newpos = Handles.FreeMoveHandle(_topLeft, rotation, HandleUtility.GetHandleSize(position) * 0.075f, Vector3.zero, Handles.CircleHandleCap);
            if (LockHeight)
                newpos.y = WallData.Start.y + position.y;

            if (WallData.Start != newpos - position)
            {
                WallData.Start = newpos - position;
                if (ChainedWallStart.Chained)
                {
                    ChainedWallStart.UpdatePosition(newpos);
                }
                Dirty = true;
            }

            Vector3 _topRight = WallData.End + position;
            newpos = Handles.FreeMoveHandle(_topRight, rotation, HandleUtility.GetHandleSize(position) * 0.075f, Vector3.zero, Handles.CircleHandleCap);
            if (LockHeight)
                newpos.y = WallData.End.y + position.y;

            if (WallData.End != newpos - position)
            {
                WallData.End = newpos - position;
                if (ChainedWallEnd.Chained)
                {
                    ChainedWallEnd.UpdatePosition(newpos);
                }
                Dirty = true;
            }

            Handles.color = Color.white;
            Handles.DrawLine(_topLeft, _topRight);

            if (Dirty && AutoBuild)
            {
                WallBuilder rnl = GetComponentInParent<WallBuilder>();
                CreateMesh(rnl);
            }
        }
#endif

        /// <summary>
        /// The position have been updated - check to see if we need to re-create the mesh
        /// </summary>
        public void UpdatedPositions()
        {
            Dirty = true;
            if (AutoBuild)
            {
                WallBuilder rnl = GetComponentInParent<WallBuilder>();
                CreateMesh(rnl);
            }
        }

        /// <summary>
        /// Create the mesh
        /// </summary>
        /// <param name="wallBuilderContext">The wall builder context</param>
        public void CreateMesh(IWallBuilderContext wallBuilderContext)
        {
#if UNITY_EDITOR
            _meshBuilder = new WallMeshBuilder();

            if (WallBuilderStrategy == null)
            {
                WallBuilderStrategy = WallBuilderStrategyFactory.Create(WallType);
            }

            UnityEngine.Random.InitState(_seed);
            WallBuilderBase wb = WallBuilderStrategy.BuilderFactory(_meshBuilder, WallType, Style, wallBuilderContext);
            if (wb != null)
            {
                wb.BuildWall(_wallData, gameObject);
                EditorUtility.SetDirty(this);
            }

            Material material = wallBuilderContext.MaterialHoldersContext.GetMaterial(WallType, _materialIndex);
            _meshBuilder.ApplyMeshDetails(gameObject, material, false, wallBuilderContext.LightingHolderContext);

            Dirty = false;

            //Re-set the seed to a random value
            UnityEngine.Random.InitState((int)System.DateTime.Now.Ticks);
            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
#endif
        }

        /// <summary>
        /// Extrude the wall from the end of the current wall
        /// </summary>
        /// <param name="wallBuilderContext">The wall builder context</param>
        /// <param name="startPos">The start position of the wall</param>
        /// <param name="offSetDirection">The end position as an off set</param>
        /// <returns>The newly create wall object</returns>

        public GameObject ExtrudeObjectEnd(IWallBuilderContext wallBuilderContext, Vector3 startPos, Vector3 offSetDirection)
        {
#if UNITY_EDITOR
            WallBuilder wallBuilder = GetComponentInParent<WallBuilder>();
            GameObject selection = wallBuilder.CloneWall(gameObject);
            WallDetails wd = selection.GetComponent<WallDetails>();
            if (wd != null)
            {
                IUseFeature feature = (IUseFeature)wd.WallBuilderStrategy.GetFeatureDetails;
                if (feature != null)
                    feature.RemoveFeature(selection);

                wd.WallData.Start = startPos;
                wd.WallData.End = startPos + offSetDirection;

                wd.RecenterObject();
                wd.CreateMesh(wallBuilderContext);

                ChainedWallEnd.SetChainedStart(wd);
                wd.ChainedWallStart.SetChainedEnd(this);
                
                EditorUtility.SetDirty(this);
            }
            return selection;
#else
            return null;
#endif
        }

        /// <summary>
        /// Extrude the wall from the start of the current wall
        /// </summary>
        /// <param name="wallBuilderContext">The wall builder context</param>
        /// <param name="startPos">The start position of the wall</param>
        /// <param name="offSetDirection">The end position as an off set</param>
        /// <returns>The newly create wall object</returns>
        public GameObject ExtrudeObjectStart(IWallBuilderContext wallBuilderContext, Vector3 startPos, Vector3 offSetDirection)
        {
#if UNITY_EDITOR
            WallBuilder wallBuilder = GetComponentInParent<WallBuilder>();
            GameObject selection = wallBuilder.CloneWall(gameObject);
            WallDetails wd = selection.GetComponent<WallDetails>();
            if (wd != null)
            {
                wd.WallData.Start = startPos;
                wd.WallData.End = startPos + offSetDirection;

                wd.RecenterObject();
                wd.CreateMesh(wallBuilderContext);

                ChainedWallStart.SetChainedStart(wd);
                wd.ChainedWallStart.SetChainedStart(this);
                EditorUtility.SetDirty(this);
            }

            return selection;
#else
            return null;
#endif

        }

        /// <summary>
        /// Draw the UI in the editor
        /// </summary>
        public void DrawUI()
        {
#if UNITY_EDITOR
            Draw(transform.position, transform.rotation);
#endif
        }

        public void SetSeed()
        {
            _seed = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
        }

        public void RecenterObject()
        {
            Vector3 start = _wallData.Start + transform.position;
            Vector3 end = _wallData.End + transform.position;
            Vector3 v = (start + end) / 2;
            transform.position = v;
            _wallData.Start = start - v;
            _wallData.End = end - v;
        }

        #region Private Fields
        [SerializeField]
        [HideInInspector]
        private int _version;

        [SerializeField]
        [HideInInspector]
        private WallTypes _wallType;

        [SerializeField]
        [HideInInspector]
        private WallData _wallData = new WallData();

        [SerializeField]
        [HideInInspector]
        private int _seed;

        [SerializeField]
        [HideInInspector]
        private ChainedWall _chainedWallStart = new ChainedWall();

        [SerializeField]
        [HideInInspector]
        private ChainedWall _chainedWallEnd = new ChainedWall();

        [SerializeField]
        [HideInInspector]
        private WallMeshBuilder _meshBuilder;

        [SerializeField]
        [HideInInspector]
        private int _materialIndex;

        [SerializeField]
        [HideInInspector]
        private IWallBuilderStrategy _wallBuilderStrategy;

        /// <summary>
        /// Whether this object is dirty
        /// </summary>
        [SerializeField]
        [HideInInspector]
        private bool _dirty = false;
        #endregion
    }


}
