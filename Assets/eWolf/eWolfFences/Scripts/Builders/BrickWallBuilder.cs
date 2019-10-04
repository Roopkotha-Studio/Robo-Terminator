using System;
using System.Collections.Generic;
using eWolfFences.Interfaces;
using UnityEngine;

namespace eWolfFences.Builders
{
    /// <summary>
    /// The builder for solid walls
    /// </summary>
    public class BrickWallBuilder : WallBuilderBase
    {
        #region Public Fields
        /// <summary>
        /// THe height of the wall
        /// </summary>
        public float WallHeight;

        /// <summary>
        /// The width of the wall
        /// </summary>
        public float WallWidth;

        /// <summary>
        /// The length of each section
        /// </summary>
        public float WallLength;

        /// <summary>
        /// The height of the UV section
        /// </summary>
        public float WallUVHeight;

        /// <summary>
        /// The amount to tapur
        /// </summary>
        public float Tapur;

        /// <summary>
        /// The amount of unkeptness
        /// </summary>
        public float Unkept;
        #endregion

        /// <summary>
        /// The standard consturctor
        /// </summary>
        /// <param name="meshBuilder">The mesh builder</param>
        /// <param name="wallType">The type of the wall</param>
        public BrickWallBuilder(WallMeshBuilder meshBuilder, WallDetails.WallTypes wallType) : base(meshBuilder, wallType)
        {
        }

        /// <summary>
        /// Build the wall
        /// </summary>
        /// <param name="wd">The wall data</param>
        /// <param name="gameObject">The game object</param>
        public override void BuildWall(WallData wd, GameObject gameObject)
        {
            Vector3 start = wd.Start;
            Vector3 direction = wd.Direction;

            IFeature feature = GetFeature(gameObject);
            if (feature != null)
            {
                string featureStyle = feature.StyleType;
                float featureLength = feature.FeatureWidth;
                float per = feature.Percentage;

                Vector3 gateHalfDir = (featureLength / 2) * direction;
                Vector3 gateStart = Vector3.Lerp(wd.Start, wd.End, per);
                gateStart -= gateHalfDir;
                float firstLen = (gateStart - wd.Start).magnitude;
                bool capWall = featureStyle == BrickWallFeature.BrickWallFeatureStyles.Gap.ToString() ? true : false;
                CreateWall(start, direction, firstLen, true, capWall);
                start = gateStart;

                if (WallType == WallDetails.WallTypes.BrickWall)
                {
                    if (featureStyle == BrickWallFeature.BrickWallFeatureStyles.Arch.ToString())
                        BrickWallArchA.BuildArchA(this, start, direction, featureLength);

                    if (featureStyle == BrickWallFeature.BrickWallFeatureStyles.ArchLarge.ToString())
                        BrickWallArchB.BuildArchB(this, start, direction, featureLength);
                }
                if (WallType == WallDetails.WallTypes.Hedge)
                {
                    if (featureStyle == HedgeFeature.HedgeFeatureStyles.Arch.ToString())
                        BrickWallArchB.BuildArchB(this, start, direction, featureLength);
                }

                start += (gateHalfDir * 2);
                firstLen = (gateStart - wd.End).magnitude;
                firstLen -= featureLength;
                CreateWall(start, direction, firstLen, capWall, true);
            }
            else
            {
                int walls = wd.WallSectionCount(WallLength);
                float endWallSize = wd.EndWallLength(WallLength);

                AddBrickEnd(start, -direction);
                for (int i = 0; i < walls; i++)
                {
                    AddBrickWall(start, direction, WallLength);
                    start += (direction * WallLength);
                }
                AddBrickWall(start, direction, endWallSize);

                start += (direction * endWallSize);
                AddBrickEnd(start, direction);
            }

            if (Unkept != 0)
                RandomizeHedge(MeshBuilders, gameObject);
        }

        protected IFeature GetFeature(GameObject gameObject)
        {
            IFeature feature = null;
            if (WallType == WallDetails.WallTypes.BrickWall)
                feature = gameObject.GetComponent<BrickWallFeature>();

            if (WallType == WallDetails.WallTypes.BrickWallWithTowers)
                feature = gameObject.GetComponent<BrickWallFeature>();
            
            if (WallType == WallDetails.WallTypes.Hedge)
                feature = gameObject.GetComponent<HedgeFeature>();

            return feature;
        }
        
        /// <summary>
        /// Create a wall
        /// </summary>
        /// <param name="start"></param>
        /// <param name="direction"></param>
        /// <param name="wallLength"></param>
        private void CreateWall(Vector3 start, Vector3 direction, float wallLength, bool capStart, bool capEnd)
        {
            int walls = WallSectionCount(wallLength);
            float endWallSize = EndWallLength(wallLength);

            if (capStart)
                AddBrickEnd(start, -direction);

            for (int i = 0; i < walls; i++)
            {
                AddBrickWall(start, direction, WallLength);
                start += (direction * WallLength);
            }
            AddBrickWall(start, direction, endWallSize);

            if (capEnd)
            {
                start += (direction * endWallSize);
                AddBrickEnd(start, direction);
            }
        }

        protected int WallSectionCount(float length)
        {
            return (int)(length / WallLength);
        }

        protected float EndWallLength(float length)
        {
            return length % WallLength;
        }

        private bool HasGateFeature(GameObject gameObject)
        {
            IFeature feature = null;
            if (WallType == WallDetails.WallTypes.BrickWall)
                feature = gameObject.GetComponent<BrickWallFeature>();

            if (WallType == WallDetails.WallTypes.Hedge)
                feature = gameObject.GetComponent<HedgeFeature>();

            return feature != null;
        }

        /// <summary>
        /// Add the end of the wall
        /// </summary>
        /// <param name="start">The starting positions</param>
        /// <param name="direction">The direction</param>
        protected void AddBrickEnd(Vector3 start, Vector3 direction)
        {
            Vector3 dir = direction;
            dir = Quaternion.AngleAxis(-90, Vector3.up) * dir;
            start += dir * (WallWidth / 2);

            UVSet topUVs = GetUVEnd();
            Vector3 tapurDistance = Tapur * dir;
            BuilderHelper.AddWallEnd(MeshBuilders, start, -dir, topUVs, WallHeight, WallWidth, tapurDistance);
        }

        /// <summary>
        /// Add the wall seciton
        /// </summary>
        /// <param name="start">The starting positions</param>
        /// <param name="direction"></param>
        /// <param name="wallLength"></param>
        protected void AddBrickWall(Vector3 start, Vector3 direction, float wallLength)
        {
            float percent = wallLength / WallLength;

            UVSet WallUVs = GetUVFull();
            WallUVs.BL.x = percent;
            WallUVs.BR.x = percent;

            UVSet topUVs = GetUVTop();
            topUVs.BL.x = percent;
            topUVs.BR.x = percent;

            Vector3 dir = direction;
            dir = Quaternion.AngleAxis(-90, Vector3.up) * dir;

            start += dir * (WallWidth / 2);
            Vector3 tapurDistance = Tapur * dir;
            BuilderHelper.AddWall(MeshBuilders, start, direction, WallUVs, WallHeight, wallLength, tapurDistance);

            Vector3 startTemp = start;
            startTemp.y += WallHeight;
            startTemp -= tapurDistance;
            BuilderHelper.AddFloor(MeshBuilders, startTemp, direction, dir * (WallWidth - (Tapur * 2)), topUVs, wallLength);
            start -= dir * (WallWidth);

            start += (direction * wallLength);
            BuilderHelper.AddWall(MeshBuilders, start, -direction, WallUVs, WallHeight, wallLength, -tapurDistance);
        }


        /// <summary>
        /// Get the UV set for the id
        /// </summary>
        /// <param name="uv">The uv set to get</param>
        /// <returns>The UV set</returns>
        protected UVSet GetUVFull()
        {
            UVSet u = new UVSet(0, 0);
            u.TL = new Vector2(0, WallUVHeight);
            u.TR = new Vector2(0, 0);

            u.BL = new Vector2(1, WallUVHeight);
            u.BR = new Vector2(1, 0);

            return u;
        }

        protected UVSet GetUVTop()
        {
            UVSet u = new UVSet(0, 0);
            u.TL = new Vector2(0, 1);
            u.TR = new Vector2(0, 1 - 0.15f);

            u.BL = new Vector2(1, 1);
            u.BR = new Vector2(1, 1 - 0.15f);

            return u;
        }

        private UVSet GetUVEnd()
        {
            UVSet u = new UVSet(0, 0);
            u.TL = new Vector2(0, WallUVHeight);
            u.TR = new Vector2(0, 0);

            u.BL = new Vector2(0.15f, WallUVHeight);
            u.BR = new Vector2(0.15f, 0);

            return u;
        }

        /// <summary>
        /// Randomize Hedge row to make it look unkept
        /// </summary>
        /// <param name="meshBuilders">The wall mesh builder</param>
        private void RandomizeHedge(WallMeshBuilder meshBuilders, GameObject gameObject)
        {
            Dictionary<Vector3, List<int>> topVerts = new Dictionary<Vector3, List<int>>();
            Dictionary<Vector3, List<int>> groundVerts = new Dictionary<Vector3, List<int>>();
            for (int i = 0; i < meshBuilders.MeshVertices.Count; i++)
            {
                Vector3 v = meshBuilders.MeshVertices[i];

                v.x = (float)Math.Round(v.x, 2);
                v.y = (float)Math.Round(v.y, 2);
                v.z = (float)Math.Round(v.z, 2);

                if (v.y != 0)
                {
                    List<int> verIndexs;
                    if (!topVerts.TryGetValue(v, out verIndexs))
                    {
                        verIndexs = new List<int>();
                        topVerts.Add(v, verIndexs);
                    }
                    verIndexs.Add(i);
                }
                else
                {
                    List<int> verIndexs;
                    if (!groundVerts.TryGetValue(v, out verIndexs))
                    {
                        verIndexs = new List<int>();
                        groundVerts.Add(v, verIndexs);
                    }
                    verIndexs.Add(i);
                }
            }


            if (HasGateFeature(gameObject))
                ApplyTopRandomEffect(meshBuilders, topVerts, true);
            else
            {
                ApplyTopRandomEffect(meshBuilders, topVerts, false);
                ApplyGroundRandomEffect(meshBuilders, groundVerts);
            }
        }

        private void ApplyTopRandomEffect(WallMeshBuilder meshBuilders, Dictionary<Vector3, List<int>> topVerts, bool verticalOnly)
        {
            float modiferHeight = WallHeight / 8f;
            float modiferWidth = WallWidth / 10f;

            foreach (KeyValuePair<Vector3, List<int>> KeyValuePair in topVerts)
            {
                List<int> li = (List<int>)KeyValuePair.Value;
                int r = UnityEngine.Random.Range(0, 100);
                if (r > Unkept)
                {
                    Vector3 v = meshBuilders.MeshVertices[li[0]];
                    float x = (UnityEngine.Random.Range(-50, 50));
                    float y = (UnityEngine.Random.Range(-50, 50));
                    float z = (UnityEngine.Random.Range(-50, 50));
                    v.y += (y * modiferHeight) / 100;
                    if (!verticalOnly)
                    {
                        v.x += (x * modiferWidth) / 100;
                        v.z += (z * modiferWidth) / 100;
                    }

                    foreach (int index in li)
                    {
                        meshBuilders.MeshVertices[index] = v;
                    }
                }
            }
        }

        private void ApplyGroundRandomEffect(WallMeshBuilder meshBuilders, Dictionary<Vector3, List<int>> groundVerts)
        {
            float modifer = WallWidth / 10f;

            foreach (KeyValuePair<Vector3, List<int>> KeyValuePair in groundVerts)
            {
                List<int> li = (List<int>)KeyValuePair.Value;
                int r = UnityEngine.Random.Range(0, 100);
                if (r > Unkept)
                {
                    Vector3 v = meshBuilders.MeshVertices[li[0]];
                    float x = (UnityEngine.Random.Range(-50, 50));
                    float z = (UnityEngine.Random.Range(-50, 50));
                    v.x += (x * modifer) / 100;
                    v.z += (z * modifer) / 100;

                    foreach (int index in li)
                    {
                        meshBuilders.MeshVertices[index] = v;
                    }
                }
            }
        }
    }
}