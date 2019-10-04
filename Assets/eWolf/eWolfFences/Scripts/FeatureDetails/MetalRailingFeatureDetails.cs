using eWolfFences.Interfaces;
using UnityEngine;

namespace eWolfFences
{
    public class MetalRailingFeatureDetails : IUseFeature
    {
        public string FeatureName
        {
            get
            {
                return "Gap";
            }
        }

        /// <summary>
        /// Do we have a feature on this node?
        /// </summary>
        /// <param name="gameObject">The master game object</param>
        /// <returns>Whether or not we have feature</returns>
        public bool HasFeature(GameObject gameObject)
        {
            IFeature omf = gameObject.GetComponent<MetalRailingFeature>();
            return omf != null;
        }

        /// <summary>
        /// Add the feature
        /// </summary>
        /// <param name="gameObject">The master game object</param>
        public void AddFeature(GameObject gameObject)
        {
            gameObject.AddComponent<MetalRailingFeature>();
            gameObject.GetComponent<MetalRailingFeature>();
        }

        /// <summary>
        /// Remove any Features from this node
        /// </summary>
        /// <param name="gameObject">The master game object</param>
        public void RemoveFeature(GameObject gameObject)
        {
            if (HasFeature(gameObject))
            {
                gameObject.GetComponent<MetalRailingFeature>().enabled = false;
                GameObject.DestroyImmediate(gameObject.GetComponent<MetalRailingFeature>());
            }
        }
    }
}
