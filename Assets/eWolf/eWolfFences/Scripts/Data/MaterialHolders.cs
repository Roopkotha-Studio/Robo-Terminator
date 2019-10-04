using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace eWolfFences
{
    [Serializable]
    public class MaterialHolders
    {
        /// <summary>
        /// The list of materials we could use for a Brick wall
        /// </summary>
        public List<Material> BrickWall = new List<Material>();

        /// <summary>
        /// The list of materials we could use for hedges
        /// </summary>
        public List<Material> Hedge = new List<Material>();

        /// <summary>
        /// The list of materials we could use for wooden fence
        /// </summary>
        public List<Material> WoodenFence = new List<Material>();

        /// <summary>
        /// The list of materials we could use for a Brick wall and towers
        /// </summary>
        public List<Material> BrickWallWithTowers = new List<Material>();

        /// <summary>
        /// The list of materials we could use for a metal railings
        /// </summary>
        public List<Material> MetalRailings = new List<Material>();

        /// <summary>
        /// The list of materials we could use for a Knee Rail Fence
        /// </summary>
        public List<Material> KneeRailFence = new List<Material>();

        /// <summary>
        /// The list of materials we could use for a Farm Fence
        /// </summary>
        public List<Material> FarmFence = new List<Material>();

        /// <summary>
        /// The list of materials we could use for a picket Fence
        /// </summary>
        public List<Material> PicketFence = new List<Material>();

        /// <summary>
        /// Get the material to use
        /// </summary>
        /// <param name="wallType">The wall type</param>
        /// <param name="randomIndex">The index to use</param>
        /// <returns>The material to set</returns>
        public Material GetMaterial(WallDetails.WallTypes wallType, int randomIndex)
        {
            return GetMaterialList(wallType)[randomIndex];
        }

        /// <summary>
        /// Get a random number for the material available fro this wall type
        /// </summary>
        /// <param name="wallType">The wall type</param>
        /// <returns>A number with in the range for the wall type</returns>
        public int GetRandomIndex(WallDetails.WallTypes wallType)
        {
            int total = GetMaterialList(wallType).Count;
            return UnityEngine.Random.Range(0, total);
        }

        public string[] GetMaterialNameList(WallDetails.WallTypes wallType)
        {
            string[] s = GetMaterialList(wallType).Select((mat) =>
            {
                return mat.name;
            }).ToArray();
            return s;
        }

        /// <summary>
        /// Get the materail and UV list for the given wall type
        /// </summary>
        /// <param name="wallType">The wall type</param>
        /// <returns>The material Uv set list</returns>
        private List<Material> GetMaterialList(WallDetails.WallTypes wallType)
        {
            switch (wallType)
            {
                case WallDetails.WallTypes.BrickWall:
                    return BrickWall;
                case WallDetails.WallTypes.Hedge:
                    return Hedge;
                case WallDetails.WallTypes.WoodenFence:
                    return WoodenFence;
                case WallDetails.WallTypes.BrickWallWithTowers:
                    return BrickWallWithTowers;
                case WallDetails.WallTypes.MetalRailings:
                    return MetalRailings;
                case WallDetails.WallTypes.KneeRailFence:
                    return KneeRailFence;
                case WallDetails.WallTypes.FarmFence:
                    return FarmFence;
                case WallDetails.WallTypes.PicketFence:
                    return PicketFence;
            }
            Debug.Log("ERROR: No materials mapped for wall type " + wallType);
            return null;
        }
    }
}
