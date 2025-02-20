using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ProjectEgoSword
{
    public class PathFindingAgent : MonoBehaviour
    {
        [Header("Debug")]
        public GameObject target;

        [Header("Setting")]
        public bool targetPlayebleCharacter;
        public GameObject startSphere;
        public GameObject endSphere;
        public bool startWalk;

        public CharacterControl owner = null;

        private NavMeshAgent _navMeshAgent;
        private List<Coroutine> _moveRoutines = new List<Coroutine>();

        private float _distToTarget = 0.5f;

        public void GoToTarget()
        {
            _navMeshAgent.enabled = true;
            startSphere.transform.parent = null;
            endSphere.transform.parent = null;
            startWalk = false;

            _navMeshAgent.isStopped = false;

            if (targetPlayebleCharacter)
            {
                target = CharacterManager.Instance.GetPlayerbleCharacter().gameObject;
            }

            _navMeshAgent.SetDestination(target.transform.position);

            if (_moveRoutines.Count != 0)
            {
                if (_moveRoutines[0] != null)
                {
                    StopCoroutine(_moveRoutines[0]);
                }

                _moveRoutines.RemoveAt(0);
            }

            _moveRoutines.Add(StartCoroutine(Move()));
        }

        // -----

        IEnumerator Move()
        {
            while (true)
            {
                owner.navMeshObstacle.carving = true;

                if (_navMeshAgent.isOnOffMeshLink)
                {
                    startSphere.transform.position = _navMeshAgent.currentOffMeshLinkData.startPos;
                    endSphere.transform.position = _navMeshAgent.currentOffMeshLinkData.endPos;

                    _navMeshAgent.CompleteOffMeshLink();

                    _navMeshAgent.isStopped = true;
                    startWalk = true;

                    yield break;
                }

                Vector3 dist = transform.position - _navMeshAgent.destination;
                if (Vector3.SqrMagnitude(dist) < _distToTarget)
                {
                    float targetDistance = 1f;
                    // if not next to the target
                    if (Vector3.SqrMagnitude(owner.transform.position - _navMeshAgent.destination) > targetDistance)
                    {
                        owner.navMeshObstacle.carving = true;
                    }


                    startSphere.transform.position = _navMeshAgent.destination;
                    endSphere.transform.position = _navMeshAgent.destination;

                    _navMeshAgent.isStopped = true;
                    startWalk = true;

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