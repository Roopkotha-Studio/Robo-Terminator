using System;
using System.Collections.Generic;
using eWolfFences.Interfaces;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace eWolfFences
{
    /// <summary>
    /// The wall builder
    /// </summary>
    public class WallBuilder : MonoBehaviour, IUIBaseDraw, IWallBuilderContext
    {
        /// <summary>
        /// The basic building units
        /// </summary>
        public BuildingUnits BuildingUnits = new BuildingUnits();

        /// <summary>
        /// THe material maps for each wall type
        /// </summary>
        public MaterialHolders MaterialHolders = new MaterialHolders();

        /// <summary>
        /// The Lighting options for the fence
        /// </summary>
        public LightingOptions Lighting;

        /// <summary>
        /// Gets the builder units
        /// </summary>
        public BuildingUnits BuildingUnitsContext
        {
            get
            {
                return BuildingUnits;
            }
        }

        /// <summary>
        /// Gets the materials holder
        /// </summary>
        public MaterialHolders MaterialHoldersContext
        {
            get
            {
                return MaterialHolders;
            }
        }

        /// <summary>
        /// Gets the lighting options
        /// </summary>
        public LightingOptions LightingHolderContext
        {
            get
            {
                return Lighting;
            }
        }

        /// <summary>
        /// Add a new wall object
        /// </summary>
        /// <returns>The newly created wall object</returns>
        public GameObject AddWall()
        {
            GameObject newWall = (GameObject)GameObject.Instantiate(Resources.Load("Wall_pf"),
                transform.position,
                Quaternion.identity);

            newWall.name = GetUniqueRoadName(gameObject);
            newWall.transform.parent = this.transform;

            WallDetails wd = newWall.GetComponent<WallDetails>();
            wd.SetUpLine();
            return newWall.gameObject;
        }

        public void DropToGround()
        {
            Transform[] allChildren = GetComponentsInChildren<Transform>(true);
            foreach (Transform child in allChildren)
            {
                WallDetails wd = child.GetComponent<WallDetails>();
                if (wd != null)
                {
                    float gap = 2;
                    wd.WallEnd = GetDownGap(wd.WallEnd, gap, 0.05f);
                    wd.WallStart = GetDownGap(wd.WallStart, gap, 0.05f);
                }
            }
        }
        
        /// <summary>
        /// Re-randomize all the seeds for all the children
        /// </summary>
        /// <param name="wallBuilderContext">The Wall Builder Contrext</param>
        public void SetSeedAllChildren(IWallBuilderContext wallBuilderContext)
        {
#if UNITY_EDITOR
            SetSeedImp(wallBuilderContext);
#endif
        }

        /// <summary>
        /// Re-randomize all the textures for all the children
        /// </summary>
        /// <param name="wallBuilderContext">The Wall Builder Contrext</param>
        public void SetRandomizeTexturesAllChildren(IWallBuilderContext wallBuilderContext)
        {
#if UNITY_EDITOR
            SetRandomizeTexturesImp(wallBuilderContext);
#endif
        }

        /// <summary>
        /// Clone a wall object
        /// </summary>
        /// <param name="cloneable">The wall to clone</param>
        /// <returns>The newly cloned wall object</returns>
        public GameObject CloneWall(GameObject cloneable)
        {
            GameObject newWall = (GameObject)GameObject.Instantiate(cloneable, this.transform);

            newWall.name = GetUniqueRoadName(gameObject);
            WallDetails wd = newWall.GetComponent<WallDetails>();
            WallDetails fromWD = cloneable.GetComponent<WallDetails>();
            wd.WallBuilderStrategy = fromWD.WallBuilderStrategy;
            return newWall.gameObject;
        }

        /// <summary>
        /// Draw all the children
        /// </summary>
        public void DrawAllChildren()
        {
            Transform[] allChildren = GetComponentsInChildren<Transform>(true);
            foreach (Transform child in allChildren)
            {
                IUIDrawable drawable = child.GetComponent<IUIDrawable>();
                if (drawable != null)
                {
                    drawable.DrawUI();
                }
            }
        }

        /// <summary>
        /// Create the mesh for each child
        /// </summary>
        public void CreateMeshForChildren()
        {
            Transform[] allChildren = GetComponentsInChildren<Transform>(true);
            foreach (Transform child in allChildren)
            {
                ICreateMesh drawable = child.GetComponent<ICreateMesh>();
                if (drawable != null)
                {
                    drawable.CreateMesh(this);
                }
            }
        }

        /// <summary>
        /// re center the main object so it's in the mindle of the wall
        /// </summary>
        public void RecenterForChildren()
        {
            Transform[] allChildren = GetComponentsInChildren<Transform>(true);
            foreach (Transform child in allChildren)
            {
                WallDetails center = child.GetComponent<WallDetails>();
                if (center != null)
                {
                    center.RecenterObject();
                }
                ICreateMesh drawable = child.GetComponent<ICreateMesh>();
                if (drawable != null)
                {
                    drawable.CreateMesh(this);
                }
            }
        }

        /// <summary>
        /// Get the unique name for a wall
        /// </summary>
        /// <returns>The unique name for the road</returns>
        private static string GetUniqueRoadName(GameObject parent)
        {
            List<string> wallNames = new List<string>();
            foreach (Transform child in parent.transform)
            {
                wallNames.Add(child.name);
            }

            int wallNumber = wallNames.Count;

            bool valid = false;

            string wallTestName = string.Empty;
            while (!valid)
            {
                wallTestName = string.Format("Wall_" + wallNumber.ToString("0000"));
                valid = !wallNames.Contains(wallTestName);
                wallNumber++;
            }

            return wallTestName;
        }

        private Vector3 GetDownGap(Vector3 pos, float gap, float offSet)
        {
            Vector3 posEnd = pos;
            pos.y += 20f;
            posEnd.y -= 40;
            Vector3 diff = posEnd - pos;

            float mag = diff.magnitude;
            diff.Normalize();

            RaycastHit hitInfo;
            if (Physics.Raycast(pos, diff, out hitInfo, mag, 1 << LayerMask.NameToLayer("Ground")))
            {
                Vector3 pohi = hitInfo.point;
                pohi.y += offSet;
                return pohi;
            }
            return posEnd;
        }

#if UNITY_EDITOR
        private void SetSeedImp(IWallBuilderContext wallBuilderContext)
        {
            Transform[] allChildren = GetComponentsInChildren<Transform>(true);
            foreach (Transform child in allChildren)
            {
                WallDetails wd = child.GetComponent<WallDetails>();
                if (wd != null)
                {
                    wd.SetSeed();
                    ICreateMesh createSelectedMesh = (ICreateMesh)wd;
                    createSelectedMesh.CreateMesh(wallBuilderContext);
                    EditorUtility.SetDirty(wd.gameObject);
                }
            }
        }

        private void SetRandomizeTexturesImp(IWallBuilderContext wallBuilderContext)
        {
            Transform[] allChildren = GetComponentsInChildren<Transform>(true);
            foreach (Transform child in allChildren)
            {
                WallDetails wd = child.GetComponent<WallDetails>();
                if (wd != null)
                {
                    wd.SetRandomMaterial(wallBuilderContext);
                    ICreateMesh createSelectedMesh = (ICreateMesh)wd;
                    createSelectedMesh.CreateMesh(wallBuilderContext);
                    EditorUtility.SetDirty(wd.gameObject);
                }
            }
        }
#endif
    }
}