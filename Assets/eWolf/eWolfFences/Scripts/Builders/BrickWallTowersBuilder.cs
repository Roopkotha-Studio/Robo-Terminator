using System.Collections.Generic;
using eWolfFences.Interfaces;
using UnityEngine;

namespace eWolfFences.Builders
{
    public class BrickWallTowersBuilder : BrickWallBuilder
    {
        public BrickWallTowersBuilder(WallMeshBuilder yardBuilder, WallDetails.WallTypes wallType, BuildingUnits builderUnits) : base(yardBuilder, wallType)
        {
            _builderUnits = builderUnits;
        }

        /// <summary>
        /// The height of the tower
        /// </summary>
        public float WallTowerHeight;

        /// <summary>
        /// Build the wall
        /// </summary>
        /// <param name="wd">The wall data</param>
        /// <param name="gameObject">The gameObject to use</param>
        public override void BuildWall(WallData wd, GameObject gameObject)
        {
            PartBuilder[] parts = CreatePartsList(wd, gameObject);

            foreach (PartBuilder part in parts)
            {
                part.Builder(part);
            }
        }

        private PartBuilder[] CreatePartsList(WallData wd, GameObject gameObject)
        {
            List<PartBuilder> partList = new List<PartBuilder>();
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

                PartBuilder part = new PartBuilder();
                part.StartCap = true;
                part.EndCap = true;
                part.WallData = wd;
                part.Builder = AddWallWithTowers;
                part.Start = start;
                part.Direction = direction;
                part.Length = firstLen;
                partList.Add(part);

                start = gateStart;
                PartBuilder partArch = null;
                if (featureStyle == BrickWallFeature.BrickWallFeatureStyles.Arch.ToString())
                {
                    partArch = new PartBuilder();
                    partArch.StartCap = false;
                    partArch.EndCap = false;
                    partArch.WallData = wd;
                    partArch.Builder = AddWallWithTowers_ArchA;
                    partArch.Start = start;
                    partArch.Direction = direction;
                    partArch.Length = featureLength;
                    start = gateStart;
                }

                if (featureStyle == BrickWallFeature.BrickWallFeatureStyles.ArchLarge.ToString())
                {
                    // BrickWallArchB.BuildArchB(this, start, direction, featureLength);
                    // TODO : still need to add large arch to the Brick wall with toweres
                }

                start += (gateHalfDir * 2);
                firstLen = (gateStart - wd.End).magnitude;
                firstLen -= featureLength;

                part = new PartBuilder();
                part.StartCap = true;
                part.EndCap = true;
                part.WallData = wd;
                part.Builder = AddWallWithTowers;
                part.Start = start;
                part.Direction = direction;
                part.Length = firstLen;
                partList.Add(part);

                if (partArch != null)
                    partList.Add(partArch);
            }
            else
            {
                PartBuilder part = new PartBuilder();
                part.StartCap = true;
                part.EndCap = true;
                part.WallData = wd;
                part.Builder = AddWallWithTowers;
                part.Start = start;
                part.Direction = direction;
                part.Length = wd.Length;
                partList.Add(part);
            }

            return partList.ToArray();
        }

        private void AddWallWithTowers_ArchA(PartBuilder part)
        {
            // Please note :  we don't need to add the two vertical towers
            Vector3 start = part.Start;
            float walllength = part.Length + (WallWidth * 2);


            start -= part.Direction * (WallWidth);
            Vector3 startTemp = start + (part.Direction * (part.Length + WallWidth));

            start += (Vector3.up * WallHeight);
            start += (Vector3.up * WallTowerHeight);

            UVSet WallUVs = GetUVTop();

            Vector3 dir = part.Direction;
            dir = Quaternion.AngleAxis(-90, Vector3.up) * dir;
            start += dir * (WallWidth / 2);
            
            BuilderHelper.AddWall(MeshBuilders, start, part.Direction, WallUVs, WallWidth, walllength, new Vector3(0,0,0));

            startTemp = start;
            startTemp += part.Direction * walllength;
            startTemp -= dir * (WallWidth);
            
            BuilderHelper.AddWall(MeshBuilders, startTemp, -part.Direction, WallUVs, WallWidth, walllength, new Vector3(0, 0, 0));
            
            Vector3 startTopTemp = start;
            startTopTemp += Vector3.up * WallWidth;
            BuilderHelper.AddFloor(MeshBuilders, startTopTemp, part.Direction, dir * WallWidth, WallUVs, walllength);
            
            startTopTemp -= Vector3.up * WallWidth;
            startTopTemp -= dir * WallWidth;
            UVSet cap = GetTowerUVTopFull();
            BuilderHelper.AddWall(MeshBuilders, startTopTemp, dir, cap, WallWidth, WallWidth, new Vector3(0, 0, 0));
            
            startTemp += dir * WallWidth;
            BuilderHelper.AddWall(MeshBuilders, startTemp, -dir, cap, WallWidth, WallWidth, new Vector3(0, 0, 0));
            
            startTopTemp = start;
            startTopTemp += part.Direction * (WallWidth + part.Length);
            BuilderHelper.AddFloor(MeshBuilders, startTopTemp, -part.Direction, dir * WallWidth, WallUVs, part.Length);
        }

        private void AddWallWithTowers(PartBuilder part)
        {
            Vector3 start = part.Start;
            int walls = WallSectionCount(part.Length);
            float endWallSize = EndWallLength(part.Length);

            bool two = false;
            if (walls > 0)
            {
                walls -= 1;
                endWallSize += WallLength;
                endWallSize /= 2;
                two = true;
            }

            if (part.StartCap)
                AddBrickEnd(start, -part.Direction);

            bool startTower = part.StartCap;
            for (int i = 0; i < walls; i++)
            {
                AddBrickWall(start, part.Direction, WallLength);

                if (startTower == true)
                    AddBrickTower(start, part.Direction, true);
                else
                    startTower = true;

                start += (part.Direction * WallLength);
            }

            AddBrickWall(start, part.Direction, endWallSize);
            if (startTower == true)
                AddBrickTower(start, part.Direction, true);

            start += (part.Direction * endWallSize);

            if (two)
            {
                AddBrickWall(start, part.Direction, endWallSize);
                AddBrickTower(start, part.Direction, true);
                start += (part.Direction * (endWallSize - WallWidth));
                if (part.EndCap)
                    AddBrickTower(start, part.Direction, true);
                start += (part.Direction * (WallWidth));
            }

            if (part.EndCap)
            {
                AddBrickEnd(start, part.Direction);
            }
        }

        private void AddBrickTower(Vector3 start, Vector3 direction, bool top)
        {
            UVSet WallUVs = GetTowerUVFull(WallTowerHeight, WallWidth);

            Vector3 dir = direction;
            dir = Quaternion.AngleAxis(-90, Vector3.up) * dir;

            start += dir * (WallWidth / 2);
            start += Vector3.up * WallHeight;
            BuilderHelper.AddWall(MeshBuilders, start, direction, WallUVs, WallTowerHeight, WallWidth, new Vector3(0, 0, 0));

            start += direction * WallWidth;
            dir = Quaternion.AngleAxis(90, Vector3.up) * direction;
            BuilderHelper.AddWall(MeshBuilders, start, dir, WallUVs, WallTowerHeight, WallWidth, new Vector3(0, 0, 0));

            start += dir * WallWidth;
            dir = Quaternion.AngleAxis(90, Vector3.up) * dir;
            BuilderHelper.AddWall(MeshBuilders, start, dir, WallUVs, WallTowerHeight, WallWidth, new Vector3(0, 0, 0));

            start += dir * WallWidth;
            dir = Quaternion.AngleAxis(90, Vector3.up) * dir;
            BuilderHelper.AddWall(MeshBuilders, start, dir, WallUVs, WallTowerHeight, WallWidth, new Vector3(0, 0, 0));

            if (top)
            {
                start += dir * WallWidth;
                start.y += WallTowerHeight;
                WallUVs = GetTowerUVTopFull();
                BuilderHelper.AddFloor(MeshBuilders, start, direction, dir * WallWidth, WallUVs, WallWidth);
            }
        }

        /// <summary>
        /// Get the UV for the wall
        /// </summary>
        /// <param name="height">The hight of the wall UVs</param>
        /// <param name="width">The width of the wall UVs</param>
        /// <returns>The set of UVs</returns>
        private UVSet GetTowerUVFull(float height, float width)
        {
            float towerPercent = height / _builderUnits.Height;
            float widthPercent = width / _builderUnits.Width;

            UVSet u = new UVSet(0, 0);
            u.TL = new Vector2(0, 0);
            u.TR = new Vector2(0, towerPercent);

            u.BL = new Vector2(widthPercent, 0);
            u.BR = new Vector2(widthPercent, towerPercent);
            return u;
        }

        private UVSet GetTowerUVTopFull()
        {
            float towerPercent = WallTowerHeight / _builderUnits.Height;
            float widthPercent = WallWidth / _builderUnits.Width;

            UVSet u = new UVSet(0, 0);
            u.TL = new Vector2(0, 0);
            u.TR = new Vector2(0, widthPercent);

            u.BL = new Vector2(towerPercent, 0);
            u.BR = new Vector2(towerPercent, widthPercent);
            return u;
        }


        private BuildingUnits _builderUnits;
    }
}
