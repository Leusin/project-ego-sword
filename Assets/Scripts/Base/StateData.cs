using UnityEngine;

namespace ProjectEgoSword
{
    public abstract class StateData<TMonoBehaviour> : ScriptableObject where TMonoBehaviour : MonoBehaviour
    {
        public float duration;

        public abstract void UpdateAbility(TMonoBehaviour monoBehaviour, Animator animator);
    }
}