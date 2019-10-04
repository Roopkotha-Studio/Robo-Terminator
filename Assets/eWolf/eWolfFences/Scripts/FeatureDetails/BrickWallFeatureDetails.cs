using eWolfFences.Interfaces;
using UnityEngine;

namespace eWolfFences
{
    public class BrickWallFeatureDetails : IUseFeature
    {
        public string FeatureName
        {
            get
            {
                return BrickWallFeature.BrickWallFeatureStyles.Gap + " or " + BrickWallFeature.BrickWallFeatureStyles.Arch;
            }
        }

        /// <summary>
        /// Do we have a feature on this node?
        /// </summary>
        /// <param name="gameObject">The master game object</param>
        /// <returns>Whether or not we have feature</returns>
        public bool HasFeature(GameObject gameObject)
        {
            IFeature omf = gameObject.GetComponent<BrickWallFeature>();
            return omf != null;
        }

        /// <summary>
        /// Add the feature
        /// </summary>
        /// <param name="gameObject">The master game object</param>
        public void AddFeature(GameObject gameObject)
        {
            gameObject.AddComponent<BrickWallFeature>();
            /*FenceGateFeature omf = */gameObject.GetComponent<BrickWallFeature>();
        }

        /// <summary>
        /// Remove any Features from this node
        /// </summary>
        /// <param name="gameObject">The master game object</param>
        public void RemoveFeature(GameObject gameObject)
        {
            if (HasFeature(gameObject))
            {
                gameObject.GetComponent<BrickWallFeature>().enabled = false;
                GameObject.DestroyImmediate(gameObject.GetComponent<BrickWallFeature>());
            }
        }
    }
}
