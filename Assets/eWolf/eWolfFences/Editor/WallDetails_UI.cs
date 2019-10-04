using System;
using eWolfFences.Factories;
using eWolfFences.Interfaces;
using UnityEditor;
using UnityEngine;

namespace eWolfFences
{
    /// <summary>
    /// The UI side of the Wall Details
    /// </summary>
    [CustomEditor(typeof(WallDetails))]
    [CanEditMultipleObjects]
    public class WallDetails_UI : Editor
    {
        /// <summary>
        /// When enable has chaged
        /// </summary>
        void OnEnable()
        {
            _wallDetails = (WallDetails)target;
            _wallDetails.WallBuilderStrategy = WallBuilderStrategyFactory.Create(_wallDetails.WallType);
        }

        /// <summary>
        /// Override the inspector
        /// </summary>
        public override void OnInspectorGUI()
        {
            WallBuilder wallBuilder = _wallDetails.GetComponentInParent<WallBuilder>();

            UpdateWallType(wallBuilder);

            UpdateStyle(wallBuilder);

            UpdateMaterialsList(wallBuilder);

            DrawDefaultInspector();
            ICreateMesh createMesh = (ICreateMesh)_wallDetails;

            GUI.color = Color.green;
            if (createMesh != null && GUILayout.Button("Create Wall"))
            {
                createMesh.CreateMesh(wallBuilder);
                EditorUtility.SetDirty(target);
                return;
            }

            if (wallBuilder != null)
            {
                GUI.color = Color.green;
                if (GUILayout.Button("Create all walls"))
                {
                    wallBuilder.CreateMeshForChildren();
                    EditorUtility.SetDirty(target);
                    return;
                }
            }

            GUI.color = Color.red;
            if (GUILayout.Button("Delete wall"))
            {
                _wallDetails.Delete();
                EditorGUIUtility.ExitGUI();
                return;
            }

            // TODO : check to see if we have move then one texture in the list and only show the below if we do.
            GUI.color = Color.green;
            if (GUILayout.Button("Randomize texture"))
            {
                UpdateSelected(wallBuilder, (wd) => wd.SetRandomMaterial(wallBuilder));
                return;
            }

            GUI.color = Color.green;
            if (GUILayout.Button("Randomize Seed"))
            {
                UpdateSelected(wallBuilder, (wd) => wd.SetSeed());
                return;
            }

            if (Selection.gameObjects.Length == 1)
            {
                IUseFeature featureMaster = (IUseFeature)_wallDetails.WallBuilderStrategy.GetFeatureDetails;
                if (featureMaster != null)
                {
                    if (!featureMaster.HasFeature(_wallDetails.gameObject))
                    {
                        GUI.color = Color.green;
                        if (GUILayout.Button("Add " + featureMaster.FeatureName))
                        {
                            foreach (GameObject o in Selection.gameObjects)
                            {
                                WallDetails wd = o.GetComponent<WallDetails>();
                                IUseFeature feature = (IUseFeature)wd.WallBuilderStrategy.GetFeatureDetails;
                                if (feature != null)
                                    feature.AddFeature(o);

                                ICreateMesh createMeshImp = o.GetComponent<WallDetails>();
                                if (createMeshImp != null)
                                    createMeshImp.CreateMesh(wallBuilder);
                            }
                        }
                    }
                    else
                    {
                        GUI.color = Color.red;
                        if (GUILayout.Button("Remove " + featureMaster.FeatureName))
                        {
                            foreach (GameObject o in Selection.gameObjects)
                            {
                                WallDetails wd = o.GetComponent<WallDetails>();
                                IUseFeature feature = (IUseFeature)wd.WallBuilderStrategy.GetFeatureDetails;
                                if (feature != null)
                                    feature.RemoveFeature(o);

                                ICreateMesh createMeshImp = o.GetComponent<WallDetails>();
                                if (createMeshImp != null)
                                    createMeshImp.CreateMesh(wallBuilder);
                            }
                            EditorGUIUtility.ExitGUI();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Update the wall style in the ui
        /// </summary>
        /// <param name="wallBuilder">The wall builder</param>
        private void UpdateStyle(WallBuilder wallBuilder)
        {
            string[] styleListDisplay = _wallDetails.WallBuilderStrategy.StyleList;
            int newStyle = EditorGUILayout.Popup("Style", _wallDetails.Style, styleListDisplay);
            if (newStyle != _wallDetails.Style)
            {
                foreach (GameObject o in Selection.gameObjects)
                {
                    WallDetails wd = o.GetComponent<WallDetails>();
                    if (wd != null)
                    {
                        if (newStyle != wd.Style)
                        {
                            wd.Style = newStyle;
                            ICreateMesh createSelectedMesh = (ICreateMesh)wd;
                            createSelectedMesh.CreateMesh(wallBuilder);
                            EditorUtility.SetDirty(wd.gameObject);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Update the material seclection
        /// </summary>
        /// <param name="wallBuilder">The wall builder</param>
        private void UpdateMaterialsList(WallBuilder wallBuilder)
        {
            WallBuilder rnl = wallBuilder.GetComponentInParent<WallBuilder>();
            string[] list = rnl.MaterialHoldersContext.GetMaterialNameList(_wallDetails.WallType);
            int newMaterial = EditorGUILayout.Popup("Material", _wallDetails.MaterialIndex, list);

            if (newMaterial != _wallDetails.MaterialIndex)
            {
                foreach (GameObject o in Selection.gameObjects)
                {
                    WallDetails wd = o.GetComponent<WallDetails>();
                    if (wd != null)
                    {
                        if (newMaterial != wd.MaterialIndex)
                        {
                            wd.MaterialIndex = newMaterial;
                            ICreateMesh createSelectedMesh = (ICreateMesh)wd;
                            createSelectedMesh.CreateMesh(wallBuilder);
                            EditorUtility.SetDirty(wd.gameObject);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Update the selected object by calling the action on them
        /// </summary>
        /// <param name="wallBuilder">The wall builder</param>
        /// <param name="updateAction">The action to call</param>
        private void UpdateSelected(WallBuilder wallBuilder, Action<WallDetails> updateAction)
        {
            foreach (GameObject o in Selection.gameObjects)
            {
                WallDetails wd = o.GetComponent<WallDetails>();
                if (wd != null)
                {
                    updateAction(wd);
                    ICreateMesh createSelectedMesh = (ICreateMesh)wd;
                    createSelectedMesh.CreateMesh(wallBuilder);
                    EditorUtility.SetDirty(wd.gameObject);
                }
            }
        }

        private void UpdateWallType(WallBuilder wallBuilder)
        {
            WallDetails.WallTypes newSetting = (WallDetails.WallTypes)EditorGUILayout.EnumPopup("Base Type", _wallDetails.WallType);
            if (newSetting == _wallDetails.WallType)
                return;

            WallBuilder rnl = wallBuilder.GetComponentInParent<WallBuilder>();
            foreach (GameObject o in Selection.gameObjects)
            {
                WallDetails wd = o.GetComponent<WallDetails>();
                if (wd != null)
                {
                    if (newSetting != wd.WallType)
                    {
                        wd.WallType = newSetting;

                        wd.WallBuilderStrategy = WallBuilderStrategyFactory.Create(wd.WallType);
                        string[] styleList = wd.WallBuilderStrategy.StyleList;
                        if (wd.Style >= styleList.Length)
                            wd.Style = 0;

                        wd.SetRandomMaterial(rnl);
                        ICreateMesh createSelectedMesh = (ICreateMesh)wd;
                        createSelectedMesh.CreateMesh(wallBuilder);
                        EditorUtility.SetDirty(wd.gameObject);
                    }
                }
            }
        }

        /// <summary>
        /// Custom Scene GUI draw
        /// </summary>
        void OnSceneGUI()
        {
            WallBuilder rnl = _wallDetails.GetComponentInParent<WallBuilder>();
            if (rnl != null)
            {
                rnl.DrawAllChildren();
            }

            WallBuilder wallBuilder = _wallDetails.GetComponentInParent<WallBuilder>();
            Vector3 dir = _wallDetails.WallData.Direction;
            dir.Normalize();
            float size = wallBuilder.BuildingUnits.Height / 2;

            if (!_wallDetails.ChainedWallEnd.Chained)
            {
                Vector3 offSet = _wallDetails.WallEnd;
                offSet += dir * (wallBuilder.BuildingUnits.Height * 2);

                DebugDrawHelper.DrawCross(offSet, size * 1.5f);
                Handles.Label(offSet, "  Extrude Wall End");
                bool extruderoad = DebugDrawHelper.DrawCircleButton(offSet, size);
                if (extruderoad == true)
                {
                    Selection.activeGameObject = _wallDetails.ExtrudeObjectEnd(wallBuilder, _wallDetails.WallEnd - _wallDetails.transform.position, dir * (wallBuilder.BuildingUnits.Height * 4));
                    EditorUtility.SetDirty(target);
                    return;
                }
            }

            if (!_wallDetails.ChainedWallStart.Chained)
            {
                Vector3 offSet = _wallDetails.WallStart;
                offSet -= dir * (wallBuilder.BuildingUnits.Height * 2);
                DebugDrawHelper.DrawCross(offSet, size * 1.5f);
                Handles.Label(offSet, "  Extrude Wall Start");
                bool extruderoad = DebugDrawHelper.DrawCircleButton(offSet, size);
                if (extruderoad == true)
                {
                    Selection.activeGameObject = _wallDetails.ExtrudeObjectStart(wallBuilder, _wallDetails.WallStart - _wallDetails.transform.position, -dir * (wallBuilder.BuildingUnits.Height * 4));
                    EditorUtility.SetDirty(target);
                    return;
                }
            }
        }

        /// <summary>
        /// The road we are editing
        /// </summary>
        private WallDetails _wallDetails;
    }
}