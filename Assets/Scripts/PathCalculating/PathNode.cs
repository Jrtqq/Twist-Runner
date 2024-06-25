using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PathCalculating
{
    public class PathNode : MonoBehaviour
    {
        public PathNode NextNode { get; private set; }

        public float DistanceToNext { get; private set; }

        private void Awake()
        {
            DistanceToNext = (NextNode.transform.position - transform.position).magnitude;
        }
    }
}
