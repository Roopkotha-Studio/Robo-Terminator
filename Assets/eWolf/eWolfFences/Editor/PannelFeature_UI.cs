using eWolfFences.Interfaces;
using UnityEditor;

namespace eWolfFences
{
    /// <summary>
    /// The UI side of the Fence Gate
    /// </summary>
    [CustomEditor(typeof(PannelFeature))]
    [CanEditMultipleObjects]
    public class PannelFeature_UI : Editor
    {
        /// <summary>
        /// When enable has chaged
        /// </summary>
        void OnEnable()
        {
            PannelFeature fenceGate = (PannelFeature)target;
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
