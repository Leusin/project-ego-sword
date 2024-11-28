using System.Collections.Generic;
using UnityEngine;

namespace ProjectEgoSword
{
    public class AnimationProgress : MonoBehaviour
    {
        public bool jumped;
        public bool cameraShaken;
        public List<PoolObjectType> poolObjectList = new List<PoolObjectType> ();
        public bool attackTriggerd;
        public float maxPressTime;

        private CharacterControl _characterControl;
        private float pressTime;

        private void Awake()
        {
            _characterControl = GetComponentInParent<CharacterControl> ();
        }

        private void Update()
        {
            if(_characterControl.attack)
            {
                pressTime += Time.deltaTime;
            }
            else
            {
                pressTime = 0f;
            }

            if (pressTime.Equals(0f))
            {
                attackTriggerd = false;
            }
            else if (pressTime > maxPressTime)
            {
                attackTriggerd = false;
            }
            else
            {
                attackTriggerd = true;
            }
        }
    }
}