using UnityEditor;
using UnityEngine;

namespace eWolfFences
{
    /// <summary>
    /// The UI side of the RoadNetworkNode
    /// </summary>
    [CustomEditor(typeof(WallBuilder))]
    [CanEditMultipleObjects]
    public class WallBuilder_UI : Editor
    {
        /// <summary>
        /// When enable has chaged
        /// </summary>
        void OnEnable()
        {
            _wallBuilder = (WallBuilder)target;
        }

        /// <summary>
        /// The inspector call
        /// </summary>
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            WallBuilder myScript = (WallBuilder)_wallBuilder;

            // if (myScript.CanExtrude)
            {
                GUI.color = Color.yellow;
                if (GUILayout.Button("Add wall"))
                {
                    Selection.activeGameObject = myScript.AddWall();
                    EditorUtility.SetDirty(target);
                    return;
                }
            }

            GUI.color = Color.green;
            if (GUILayout.Button("Create all Walls"))
            {
                myScript.CreateMeshForChildren();
                EditorUtility.SetDirty(target);
                return;
            }

            if (GUILayout.Button("Drop all corners to ground"))
            {
                myScript.DropToGround();
                myScript.CreateMeshForChildren();
                EditorUtility.SetDirty(target);
                return;
            }

            if (GUILayout.Button("Randmize Textures for all childeren"))
            {
                myScript.SetRandomizeTexturesAllChildren(_wallBuilder);
                EditorUtility.SetDirty(target);
                return;
            }

            if (GUILayout.Button("Randomize Seed for all childeren"))
            {
                myScript.SetSeedAllChildren(_wallBuilder);
                EditorUtility.SetDirty(target);
                return;
            }

            GUI.color = Color.yellow;
            if (GUILayout.Button("Recenter all walls objects"))
            {
                myScript.RecenterForChildren();
                EditorUtility.SetDirty(target);
                return;
            }
            
            if (GUILayout.Button("Lock all childeren"))
            {
                LockAllChildrenImp(true);
                return;
            }

            if (GUILayout.Button("Un-Lock all childeren"))
            {
                LockAllChildrenImp(false);
                return;
            }
        }

        /// <summary>
        /// On scene GUI
        /// </summary>
        public void OnSceneGUI()
        {
            _wallBuilder.DrawAllChildren();
        }

        #region Private Methods
        /// <summary>
        /// Set the locked flag for all the child objects
        /// </summary>
        /// <param name="locked">Wether or not to set the locked flag</param>
        private void LockAllChildrenImp(bool locked)
        {
            Transform[] allChildren = _wallBuilder.GetComponentsInChildren<Transform>(true);
            foreach (Transform child in allChildren)
            {
                WallDetails wd = child.GetComponent<WallDetails>();
                if (wd != null)
                    wd.LockWall = locked;
            }
            EditorUtility.SetDirty(target);
        }
        #endregion

        /// <summary>
        /// The road we are editing
        /// </summary>
        private WallBuilder _wallBuilder;
    }
}
