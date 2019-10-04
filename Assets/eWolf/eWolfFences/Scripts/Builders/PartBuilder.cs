using System;
using System.Collections.Generic;
using UnityEngine;

namespace eWolfFences.Builders
{
    public class PartBuilder
    {
        public Vector3 Start;
        public Vector3 Direction;
        public float Length;
        public bool StartCap = false;
        public bool EndCap = false;
        public Action<PartBuilder> Builder;
        public WallData WallData;
    }
}
