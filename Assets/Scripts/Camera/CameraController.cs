using UnityEngine;

namespace ProjectEgoSword
{
    public class CameraController : MonoBehaviour
    {
        public enum Trigger
        {
            Default,
            Shake
        }

        private Animator _animator;

        public Animator AnimatorComponent
        {
            get
            {
                if (_animator == null)
                {
                    _animator = GetComponent<Animator>();
                }

                return _animator;
            }
        }

        public void TriggerCamera(Trigger trigger)
        {
            AnimatorComponent.SetTrigger(trigger.ToString());
        }
    }
}