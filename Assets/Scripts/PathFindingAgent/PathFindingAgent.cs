using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace ProjectEgoSword
{
    public class PathFindingAgent : MonoBehaviour
    {
        [Header("Debug")]
        public GameObject target;
        public Vector3 startPosition;
        public Vector3 endPosition;

        [Header("Setting")]
        public bool targetPlayebleCharacter;
        public GameObject startSphere;
        public GameObject endSphere;

        private Coroutine _move;
        private NavMeshAgent _navMeshAgent;


        public void GoToTarget()
        {
            startSphere.transform.parent = null;
            endSphere.transform.parent = null;

            _navMeshAgent.isStopped = false;

            if(targetPlayebleCharacter)
            {
                target = CharacterManager.Instance.GetPlayerbleCharacter().gameObject;
            }

            _navMeshAgent.SetDestination(target.transform.position);

            if(_move != null)
            {
                StopCoroutine(_move);
            }

            _move = StartCoroutine(Move());
        }

        // -----

        IEnumerator Move()
        {
            while(true)
            {
                if(_navMeshAgent.isOnOffMeshLink)
                {
                    startPosition = transform.position;
                    startSphere.transform.position = transform.position;
                    _navMeshAgent.CompleteOffMeshLink();

                    yield return new WaitForEndOfFrame();

                    endPosition = transform.position;
                    endSphere.transform.position = transform.position;
                    _navMeshAgent.isStopped = true;

                    yield break;
                }

                Vector3 dist = transform.position - _navMeshAgent.destination;
                if(Vector3.SqrMagnitude(dist) < 0.5f)
                {
                    startPosition = transform.position;
                    endPosition = transform.position;
                    _navMeshAgent.isStopped = true;
                    yield break;
                }

                yield return new WaitForEndOfFrame();
            }
        }


        // -----

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }
    }
}