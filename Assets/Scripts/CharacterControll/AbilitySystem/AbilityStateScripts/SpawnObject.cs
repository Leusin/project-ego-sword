using UnityEngine;

namespace ProjectEgoSword
{
    [CreateAssetMenu(fileName = "New State", menuName = "ProjectEgoSword/AbilityData/SpawnObject")]
    public class SpawnObject : StateData<CharacterControl>
    {
        public PoolObjectType objectType;
        [Range(0f, 1f)]
        public float spwnTiming;
        public string parentObjectName = string.Empty;
        public bool stickToParent;

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
            }
        }

        public override void UpdateAbility(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (!monobehaviour.animationProgress.poolObjectList.Contains(objectType))
            {
                if(stateInfo.normalizedTime >= spwnTiming)
                {
                    SpawnObj(monobehaviour);
                }
            }
        }

        public override void OnExit(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (monobehaviour.animationProgress.poolObjectList.Contains(objectType))
            {
                monobehaviour.animationProgress.poolObjectList.Remove(objectType);
            }
        }

        private void SpawnObj(CharacterControl monobehaviour)
        {
            if (monobehaviour.animationProgress.poolObjectList.Contains(objectType))
            {
                return;
            }

            GameObject obj = _pooledManager.GetObject(objectType);

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

            monobehaviour.animationProgress.poolObjectList.Add(objectType);
        }
    }
}
