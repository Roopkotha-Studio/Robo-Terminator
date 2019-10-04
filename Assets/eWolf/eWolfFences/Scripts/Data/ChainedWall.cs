#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEditor.SceneManagement;
#endif
using UnityEngine;

namespace eWolfFences
{
#if UNITY_EDITOR
    [Serializable]
#endif
    public class ChainedWall
    {
        private enum ChainedTooPoint
        {
            Unknown,
            End,
            Start
        }

        /// <summary>
        /// Is this chained to another game object
        /// </summary>
        public bool Chained
        {
            get
            {
                return _chainedToo != null;
            }
        }

        /// <summary>
        /// The the chained to end object
        /// </summary>
        /// <param name="wall">The wall object to chain</param>
        public void SetChainedEnd(WallDetails wall)
        {
            _chainedToo = wall;
            _chainedTooPoint = ChainedTooPoint.End;
        }

        /// <summary>
        /// The the chained to start object
        /// </summary>
        /// <param name="wall">The wall object to chain</param>
        public void SetChainedStart(WallDetails wall)
        {
            _chainedToo = wall;
            _chainedTooPoint = ChainedTooPoint.Start;
        }

        internal void UpdatePosition(Vector3 updatePosition)
        {
            if (_chainedTooPoint == ChainedTooPoint.Start)
                UpdateStartPosition(updatePosition);
            else
                UpdateEndPosition(updatePosition);
        }

        /// <summary>
        /// Update the end wall position of the chained fence
        /// </summary>
        /// <param name="updatePosition">The updated position</param>
        internal void UpdateEndPosition(Vector3 updatePosition)
        {
            _chainedToo.WallData.End = updatePosition - _chainedToo.transform.position;
            _chainedToo.UpdatedPositions();
        }

        /// <summary>
        /// Update the start wall position of the chained fence
        /// </summary>
        /// <param name="updatePosition">The updated position</param>
        internal void UpdateStartPosition(Vector3 updatePosition)
        {

            _chainedToo.WallData.Start = updatePosition - _chainedToo.transform.position;
            _chainedToo.UpdatedPositions();
        }

        [SerializeField]
        [HideInInspector]
        private ChainedTooPoint _chainedTooPoint = ChainedTooPoint.Unknown;

        [SerializeField]
        [HideInInspector]
        private WallDetails _chainedToo = null;
    }
}