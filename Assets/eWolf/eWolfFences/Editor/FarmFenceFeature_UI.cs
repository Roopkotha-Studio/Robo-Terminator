using eWolfFences.Interfaces;
using UnityEditor;

namespace eWolfFences
{
    /// <summary>
    /// The UI side of the farm fences gap or stile
    /// </summary>
    [CustomEditor(typeof(FarmFenceFeature))]
    [CanEditMultipleObjects]
    public class FarmFenceFeature_UI : Editor
    {
        /// <summary>
        /// When enable has chaged
        /// </summary>
        void OnEnable()
        {
            FarmFenceFeature fenceGate = (FarmFenceFeature)target;
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
