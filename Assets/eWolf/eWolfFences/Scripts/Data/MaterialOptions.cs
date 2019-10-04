using System;
using System.Collections.Generic;
using UnityEngine;

namespace eWolfFences
{
    [Serializable]
    public class MaterialOptions
    {
        #region Fields
        /// <summary>
        /// The set of materials
        /// </summary>
        public List<Material> Materials = new List<Material>();
        #endregion

        #region Private Methods
        /// <summary>
        /// Get a random material from the selction
        /// </summary>
        /// <returns></returns>
        public int GetRandomIndex()
        {
            int total = Materials.Count;
            return UnityEngine.Random.Range(0, total);
        }

        /// <summary>
        /// Check the material is in the range of the materials listed
        /// </summary>
        /// <param name="materialIndex">The current index</param>
        /// <returns>The material capped to the size of the materials list</returns>
        public int CheckLimits(int materialIndex)
        {
            if (materialIndex < 0)
                materialIndex = 0;

            if (materialIndex >= Materials.Count-1)
                materialIndex = Materials.Count - 1;

            return materialIndex;
        }

        public void SetMaterial(string materialName)
        {
            Materials = new List<Material>();
            Material go = (Material)Resources.Load(materialName);
            Materials.Add(go);
        }
        #endregion
    }
}
