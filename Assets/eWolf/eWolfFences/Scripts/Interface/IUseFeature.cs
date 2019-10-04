using UnityEngine;

namespace eWolfFences.Interfaces
{
    public interface IUseFeature
    {
        /// <summary>
        /// The feature name
        /// </summary>
        string FeatureName { get; }

        /// <summary>
        /// Have we get an feature attached to this node
        /// </summary>
        /// <param name="gameObject">The master game object</param>
        /// <returns>Wether or not we have a feature</returns>
        bool HasFeature(GameObject gameObject);

        /// <summary>
        /// Add a feature to this node
        /// <param name="gameObject">The master game object</param>
        /// </summary>
        void AddFeature(GameObject gameObject);

        /// <summary>
        /// Remove a feature if any are on the node
        /// </summary>
        /// <param name="gameObject">The master game object</param>
        void RemoveFeature(GameObject gameObject);
    }
}
