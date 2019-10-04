using eWolfFences.Interfaces;
using UnityEngine;

namespace eWolfFences
{
    public class GateFeatureDetails : IUseFeature
    {
        public string FeatureName
        {
            get
            {
                return "Gate";
            }
        }

        /// <summary>
        /// Do we have a feature on this node?
        /// </summary>
        /// <param name="gameObject">The master game object</param>
        /// <returns>Whether or not we have feature</returns>
        public bool HasFeature(GameObject gameObject)
        {
            IFeature omf = gameObject.GetComponent<PannelFeature>();
            return omf != null;
        }

        /// <summary>
        /// Add the feature
        /// </summary>
        /// <param name="gameObject">The master game object</param>
        public void AddFeature(GameObject gameObject)
        {
            gameObject.AddComponent<PannelFeature>();
            /*FenceGateFeature omf = */gameObject.GetComponent<PannelFeature>();
        }

        /// <summary>
        /// Remove any Features from this node
        /// </summary>
        /// <param name="gameObject">The master game object</param>
        public void RemoveFeature(GameObject gameObject)
        {
            if (HasFeature(gameObject))
            {
                gameObject.GetComponent<PannelFeature>().enabled = false;
                GameObject.DestroyImmediate(gameObject.GetComponent<PannelFeature>());
            }
        }
    }
}
