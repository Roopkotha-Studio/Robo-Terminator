using eWolfFences.Interfaces;
using UnityEngine;

namespace eWolfFences
{
    public class FarmFenceFeatureDetails : IUseFeature
    {
        public string FeatureName
        {
            get
            {
                return FarmFenceFeature.FarmFenceFeatureStyles.Gap + " or " + FarmFenceFeature.FarmFenceFeatureStyles.Stile;
            }
        }

        /// <summary>
        /// Do we have a feature on this node?
        /// </summary>
        /// <param name="gameObject">The master game object</param>
        /// <returns>Whether or not we have feature</returns>
        public bool HasFeature(GameObject gameObject)
        {
            IFeature omf = gameObject.GetComponent<FarmFenceFeature>();
            return omf != null;
        }

        /// <summary>
        /// Add the feature
        /// </summary>
        /// <param name="gameObject">The master game object</param>
        public void AddFeature(GameObject gameObject)
        {
            gameObject.AddComponent<FarmFenceFeature>();
            /*FenceGateFeature omf = */gameObject.GetComponent<FarmFenceFeature>();
        }

        /// <summary>
        /// Remove any Features from this node
        /// </summary>
        /// <param name="gameObject">The master game object</param>
        public void RemoveFeature(GameObject gameObject)
        {
            if (HasFeature(gameObject))
            {
                gameObject.GetComponent<FarmFenceFeature>().enabled = false;
                GameObject.DestroyImmediate(gameObject.GetComponent<FarmFenceFeature>());
            }
        }
    }
}
