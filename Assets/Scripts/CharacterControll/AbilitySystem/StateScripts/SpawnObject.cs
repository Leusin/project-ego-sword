using UnityEngine;

namespace ProjectEgoSword
{
    [CreateAssetMenu(fileName = "New State", menuName = "ProjectEgoSword/AbilityData/SpawnObject")]
    public class SpawnObject : StateData<CharacterControl>
    {
        public PoolObjectType poolObjectType;
        [Range(0f, 1f)]
        public float spwnTiming;
        public string parentObjectName = string.Empty;
        public bool stickToParent;

        private bool isSpwned;
        private PoolManager _pooledManager;

        public override void OnStart(CharacterControl monoBehaviour, Animator animator)
        {
            _pooledManager = PoolManager.Instance;
        }

        public override void OnEnter(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if(spwnTiming.Equals(0f))
            {
                SpawnObj(monobehaviour);
                isSpwned = true;
            }
        }

        public override void UpdateAbility(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if(!isSpwned)
            {
                if(stateInfo.normalizedTime >= spwnTiming)
                {
                    SpawnObj(monobehaviour);
                    isSpwned = true;
                }
            }
        }

        public override void OnExit(CharacterControl monoBehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            isSpwned = false;
        }

        private void SpawnObj(CharacterControl monobehaviour)
        {
            GameObject obj = _pooledManager.GetObject(poolObjectType);

            if(!string.IsNullOrEmpty(parentObjectName))
            {
                GameObject parent = monobehaviour.GetChildObject(parentObjectName);
                obj.transform.parent = parent.transform;
                obj.transform.localPosition = Vector3.zero;
                obj.transform.localRotation = Quaternion.identity;
            }

            if(!stickToParent)
            {
                obj.transform.parent = null;
            }

            obj.SetActive(true);
        }
    }
}
