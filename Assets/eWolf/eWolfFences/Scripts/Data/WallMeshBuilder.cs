using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace eWolfFences
{
    public class WallMeshBuilder
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public WallMeshBuilder()
        {
            _meshMaterialsTriangles.Add("Base", new List<int>());
        }

        #region Public Properties
        /// <summary>
        /// Gets the list of vertices
        /// </summary>
        public List<Vector3> MeshVertices
        {
            get
            {
                return _meshVertices;
            }
        }

        /// <summary>
        /// Gets the list of Road UVs
        /// </summary>
        public List<Vector2> MeshUVs
        {
            get
            {
                return _meshUVs;
            }
        }
        #endregion

        #region Public Methods
        public object Clone()
        {
            WallMeshBuilder mb = new WallMeshBuilder();
            mb._meshUVs = new List<Vector2>(_meshUVs);
            mb._meshMaterialsTriangles = _meshMaterialsTriangles;
            mb._meshVertices = new List<Vector3>(_meshVertices);

            return mb;
        }

        /// <summary>
        /// Get the triangels for the base material
        /// </summary>
        /// <returns>The list of triangles</returns>
        public List<int> GetTriangles()
        {
            return GetTriangles("Base");
        }

        /// <summary>
		/// Get the triangels for the correct material
		/// </summary>
		/// <param name="materialName">The material name</param>
		/// <returns>The list of triangles</returns>
		public List<int> GetTriangles(string materialName)
        {
            // not needed here as we only have one texture type.
            /*if (string.IsNullOrEmpty(materialName))
			{
        		return _meshMaterialsTriangles["base"];
				else
				{
					foreach (MaterialFrequency mf in Details)
					{
						if (mf.Frequency == MaterialFrequency.FrequencyRate.MainTexture)
							materialName = mf.Material.name;
					}
				}
			}*/
            return _meshMaterialsTriangles[materialName];
        }

        /// <summary>
        /// Apply the mesh details to the object
        /// </summary>
        /// <param name="baseobject">The object to add the mesh too</param>
        /// <param name="material">The material to use</param>
        /// <param name="applyOffSet">apply the off set</param>
        /// <param name="lights">The lighting options</param>
        public void ApplyMeshDetails(GameObject baseobject, Material material, bool applyOffSet, LightingOptions lights)
        {
#if UNITY_EDITOR
            Mesh mesh = new Mesh();
            mesh.name = "Building";
            baseobject.GetComponent<MeshFilter>().mesh = mesh;

            if (applyOffSet)
                ApplyObjectOffSet(baseobject.transform.position);

            mesh.vertices = MeshVertices.ToArray();
            mesh.uv = MeshUVs.ToArray();

            // Create the material and assign triangles
            Renderer r = baseobject.GetComponent<Renderer>();
            List<Material> materials = new List<Material>();
            int count = 0;
            mesh.subMeshCount = _meshMaterialsTriangles.Count;

            materials.Add(material);
            mesh.SetTriangles(GetTriangles("Base").ToArray(), count++);
            mesh.subMeshCount = count;

            r.materials = materials.ToArray();
            mesh.RecalculateNormals();
            mesh.RecalculateBounds();

            if (lights.BakedLighting)
            {
                Debug.Log("Appling unwrapping for Baked Lighting");

                UnwrapParam up = new UnwrapParam();
                up.hardAngle = lights.HardAngle;
                up.packMargin = lights.PackMargin;
                up.angleError = lights.AngleError;
                up.areaError = lights.AngleError;

                Unwrapping.GenerateSecondaryUVSet(mesh, up);
            }

            ApplyCollision(baseobject);
#endif
        }

        /// <summary>
        /// Apply the collision to the mesh
        /// </summary>
        /// <param name="baseobject">The object to apply the mesh too</param>
        private static void ApplyCollision(GameObject baseobject)
        {
            MeshCollider CCgb = baseobject.GetComponent<MeshCollider>();
            CCgb.convex = false;
            CCgb.sharedMesh = baseobject.GetComponent<MeshFilter>().sharedMesh;
        }

        /// <summary>
        /// Build a trangle
        /// </summary>
        /// <param name="a">Position A</param>
        /// <param name="b">Position B</param>
        /// <param name="c">Position C</param>
        /// <param name="uvc">UV a</param>
        /// <param name="uva">UV b</param>
        /// <param name="uvb">UV c</param>
        public void BuildTri(Vector3 a, Vector3 b, Vector3 c, Vector2 uvc, Vector2 uva, Vector2 uvb)
        {
            int indexA = AddVectorUVSets(a, uva);
            int indexB = AddVectorUVSets(b, uvb);
            int indexC = AddVectorUVSets(c, uvc);
            
            GetTriangles().AddRange(new int[] { indexA, indexB, indexC });
        }

        /// <summary>
        /// Build a quad
        /// </summary>
        /// <param name="a">Position A</param>
        /// <param name="b">Position B</param>
        /// <param name="c">Position C</param>
        /// <param name="d">Positiom D</param>
        /// <param name="r">UV set</param>
        public void BuildQuad(Vector3 a, Vector3 b, Vector3 c, Vector3 d, UVSet uvset)
        {
            int indexA = AddVectorUVSets(a, uvset.BR);
            int indexB = AddVectorUVSets(b, uvset.TR);
            int indexC = AddVectorUVSets(c, uvset.BL);
            int indexD = AddVectorUVSets(d, uvset.TL);

            GetTriangles().AddRange(new int[] { indexA, indexB, indexC });
            GetTriangles().AddRange(new int[] { indexD, indexC, indexB });
        }

        /// <summary>
        /// Build a quad
        /// </summary>
        /// <param name="a">Position A</param>
        /// <param name="b">Position B</param>
        /// <param name="c">Position C</param>
        /// <param name="d">Positiom D</param>
        /// <param name="r">UV set</param>
        public void BuildQuadFlipped(Vector3 a, Vector3 b, Vector3 c, Vector3 d, UVSet uvset)
        {
            int indexA = AddVectorUVSets(a, uvset.BR);
            int indexB = AddVectorUVSets(b, uvset.TR);
            int indexC = AddVectorUVSets(c, uvset.BL);
            int indexD = AddVectorUVSets(d, uvset.TL);

            GetTriangles().AddRange(new int[] { indexC, indexB, indexA });
            GetTriangles().AddRange(new int[] { indexB, indexC, indexD });           
        }

        /// <summary>
        /// Add the vertor and UV sets if they are unique
        /// </summary>
        /// <param name="points">The vector point to add</param>
        /// <param name="uvs">The UV to add</param>
        /// <returns>The index of existing pair, or the index of the newly added pair.</returns>
        private int AddVectorUVSets(Vector3 points, Vector2 uvs)
        {
            for (int i = 0; i < MeshVertices.Count; i++)
            {
                Vector3 vec = MeshVertices[i];
                Vector2 v2 = MeshUVs[i];

                if (vec == points && v2 == uvs)
                    return i;
                
            }

            MeshVertices.Add(points);
            MeshUVs.Add(uvs);
            return MeshVertices.Count - 1;
        }

        /// <summary>
        /// Get the UV set for the id
        /// </summary>
        /// <param name="uv">The uv set to get</param>
        /// <returns>The UV set</returns>
        private UVSet GetUVs(TextUV uv)
        {
            return TextureSetHolder.Instance.GetUvData(uv);
        }
        #endregion

        #region Private Methods
        /// <summary>
		/// Apply the off set of the base object to the vertices
		/// </summary>
		/// <param name="offSet">The off set to apply</param>
		private void ApplyObjectOffSet(Vector3 offSet)
        {
            for (int i = 0; i < MeshVertices.Count; i++)
            {
                MeshVertices[i] -= offSet;
            }
        }
        #endregion

        #region Private Fields
        /// <summary>
        /// The vertices of the road
        /// </summary>
        private List<Vector3> _meshVertices = new List<Vector3>();

        /// <summary>
        /// The UVs for the road
        /// </summary>
        private List<Vector2> _meshUVs = new List<Vector2>();

        /// <summary>
        /// The triangles draw order for the road for each materials used
        /// </summary>
        private Dictionary<string, List<int>> _meshMaterialsTriangles = new Dictionary<string, List<int>>();
        #endregion
    }
}