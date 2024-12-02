using UnityEngine;
using UnityEngine.AI;

namespace ProjectEgoSword
{
    public class PathFindingAgent : MonoBehaviour
    {
        public bool targetPlayebleCharacter;
        public GameObject target;

        private NavMeshAgent _navMeshAgent;

        public void GoToTarget()
        {
            if(targetPlayebleCharacter)
            {
                target = CharacterManager.Instance.GetPlayerbleCharacter().gameObject;
            }

            _navMeshAgent.SetDestination(target.transform.position);
        }

        // -----

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }
    }
}