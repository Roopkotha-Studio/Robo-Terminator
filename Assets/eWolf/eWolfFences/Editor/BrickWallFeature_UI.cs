using eWolfFences.Interfaces;
using UnityEditor;

namespace eWolfFences
{
    /// <summary>
    /// The UI side of the brick wall gap or arch
    /// </summary>
    [CustomEditor(typeof(BrickWallFeature))]
    [CanEditMultipleObjects]
    public class BrickWallFeature_UI : Editor
    {
        /// <summary>
        /// When enable has chaged
        /// </summary>
        void OnEnable()
        {
            BrickWallFeature fenceGate = (BrickWallFeature)target;
            _fenceGate = (IFeature)fenceGate;
        }

        /// <summary>
        /// Custom Scene GUI draw
        /// </summary>
        void OnSceneGUI()
        {
            _fenceGate.DrawUI();
        }

        private IFeature _fenceGate;
    }
}
