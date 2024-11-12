using UnityEngine;

namespace ProjectEgoSword
{
    public abstract class StateData<TMonoBehaviour> : ScriptableObject where TMonoBehaviour : MonoBehaviour
    {
        public virtual void OnStart(Animator animator) { }
        public abstract void OnEnter(TMonoBehaviour monoBehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex);
        public abstract void UpdateAbility(TMonoBehaviour monoBehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex);
        public abstract void OnExit(TMonoBehaviour monoBehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex);
    }
}