using System.Collections.Generic;
using UnityEngine;

namespace ProjectEgoSword
{
    public class AnimationProgress : MonoBehaviour
    {
        public bool jumped;
        public bool cameraShaken;
        public List<PoolObjectType> poolObjectList = new List<PoolObjectType> ();
    }
}