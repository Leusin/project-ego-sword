using UnityEngine;
using UnityEngine.AI;

namespace ProjectEgoSword
{
    public class HumanoidController : CharacterControl
    {
        public Transform WeaponMountPosition;

        public NavMeshAgent NavMeshAgentComponent
        {
            get
            {
                if (_cachednavMeshAgent == null)
                {
                    _cachednavMeshAgent = GetComponent<NavMeshAgent>();
                }
                return _cachednavMeshAgent;
            }
        }

        private NavMeshAgent _cachednavMeshAgent;

        // -----

        public void Equip(PlayerController playerController)
        {
            playerController.transform.SetParent(WeaponMountPosition.transform);
            playerController.transform.localPosition = Vector3.zero;
            playerController.transform.localRotation = Quaternion.identity;
            _cachednavMeshAgent.enabled = false;
        }

        public void Unequip()
        {
            _cachednavMeshAgent.enabled = true;
            transform.SetParent(null);
            this.enabled = false;
        }

        // -----

        private void Awake()
        {
            SceneLinkedSMB<HumanoidController>.Initialise(skinedMeshAnimator, this);
        }
    }
}