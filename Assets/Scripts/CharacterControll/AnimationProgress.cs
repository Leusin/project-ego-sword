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
        public bool ragdollTriggerd;
        public float maxPressTime;
        public bool disallowEarylTurn;

        [Header("AirControl")]
        public float airMomentum;
        public bool frameUpdated;

        [Header("UpdateBoxCollider")]
        public bool updatingBoxCollider;
        public bool updatingSpheres;
        public Vector3 targetSize;
        public float sizeSpeed;
        public Vector3 targetCenter;
        public float centerSpeed;

        private CharacterControl _characterControl;
        private float _pressTime;

        private void Awake()
        {
            _characterControl = GetComponentInParent<CharacterControl> ();
        }

        private void Update()
        {
            if(_characterControl.attack)
            {
                _pressTime += Time.deltaTime;
            }
            else
            {
                _pressTime = 0f;
            }

            if (_pressTime.Equals(0f))
            {
                attackTriggerd = false;
            }
            else if (_pressTime > maxPressTime)
            {
                attackTriggerd = false;
            }
            else
            {
                attackTriggerd = true;
            }
        }

        private void LateUpdate()
        {
            frameUpdated = false;
        }
    }
}