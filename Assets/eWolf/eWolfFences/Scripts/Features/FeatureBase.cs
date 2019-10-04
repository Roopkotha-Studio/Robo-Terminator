#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace eWolfFences
{
    public class FeatureBase : MonoBehaviour
    {
        /// <summary>
        /// Gets the position of the gate as a percentage from the start to the end
        /// </summary>
        public float Percentage
        {
            get
            {
                return _percentage;
            }

            set
            {
                _percentage = value;
            }
        }

        /// <summary>
        /// Draw the UI in the editor
        /// </summary>
        public void DrawUI()
        {
#if UNITY_EDITOR
            Draw(transform.position, transform.rotation);
#endif
        }

#if UNITY_EDITOR
        /// <summary>
        /// Draw the lines
        /// </summary>
        /// <param name="position">The parent position</param>
        private void Draw(Vector3 position, Quaternion rotation)
        {
            float percentage = Percentage;
            WallDetails wd = gameObject.GetComponent<WallDetails>();
            WallBuilder wallBuilder = wd.GetComponentInParent<WallBuilder>();

            Vector3 start = wd.WallStart;
            Vector3 end = wd.WallEnd;
            Vector3 gatePos = Vector3.Lerp(start, end, percentage);

            float size = wallBuilder.BuildingUnits.Height *0.3f;
            Vector3 newpos = Handles.FreeMoveHandle(gatePos, rotation, size, Vector3.zero, Handles.RectangleHandleCap);
            if (newpos != gatePos)
            {
                newpos = MathsHelper.ClosestPointOnLine(start, end, newpos);
                float length = (start - end).magnitude;
                float newOffSet = (start - newpos).magnitude;
                float percent = 100 / length;
                float updatedPercentage = newOffSet * percent;

                Percentage = updatedPercentage / 100;
                wd.UpdatedPositions();
            }
        }

        /// <summary>
        /// Update the wall any time we change a value.
        /// </summary>
        public void OnValidate()
        {
            WallDetails wd = GetComponent<WallDetails>();
            if (wd != null)
                wd.UpdatedPositions();
        }
#endif

        [SerializeField]
        [HideInInspector]
        private float _percentage = 0.50f;
    }
}
