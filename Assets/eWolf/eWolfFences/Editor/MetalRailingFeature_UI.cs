using eWolfFences.Interfaces;
using UnityEditor;

namespace eWolfFences
    {
        /// <summary>
        /// The UI side of the Knee rail wall gap or arch
        /// </summary>
        [CustomEditor(typeof(MetalRailingFeature))]
        [CanEditMultipleObjects]
        public class MetalRailingFeature_UI : Editor
        {
            /// <summary>
            /// When enable has chaged
            /// </summary>
            void OnEnable()
            {
            MetalRailingFeature fenceGate = (MetalRailingFeature)target;
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
