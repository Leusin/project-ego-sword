using System.Collections;
using UnityEngine;

namespace ProjectEgoSword
{
    public class PoolObject : MonoBehaviour
    {
        public PoolObjectType poolObjectType;
        public float scheduledOffTime;

        private Coroutine _offRoutine;

        private PoolManager _poolManager;

        private void Start()
        {
            _poolManager = PoolManager.Instance;
        }

        private void OnEnable()
        {
            if( _offRoutine != null )
            {
                StopCoroutine(_offRoutine);
            }

            if(scheduledOffTime > 0f)
            {
                _offRoutine = StartCoroutine(_ScheduledOff());
            }
        }

        public void TurnOff()
        {
            transform.parent = null;
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;

            _poolManager.AddObject(this);
        }

        IEnumerator _ScheduledOff()
        {
            yield return new WaitForSeconds(scheduledOffTime);

            if (!_poolManager.poolDictionary[poolObjectType].Contains(this.gameObject))
            {
                GetComponent<PoolObject>().TurnOff();
            }
        }
    }
}