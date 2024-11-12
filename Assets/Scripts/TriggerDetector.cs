using UnityEngine;

namespace ProjectEgoSword
{

    public class TriggerDetector : MonoBehaviour
    {
        private CharacterControl _owner;

        private void Awake()
        {
            _owner = GetComponentInParent<CharacterControl>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_owner.ragdollParts.Contains(other))
            {
                return;
            }

            var controller = other.transform.root.GetComponent<CharacterControl>();

            if (controller == null)
            {
                return;
            }

            if (other.gameObject == controller.gameObject)
            {
                return;
            }

            if (!_owner.collidingParts.Contains(other))
            {
                _owner.collidingParts.Add(other);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (_owner.collidingParts.Contains(other))
            {
                _owner.collidingParts.Remove(other);
            }
        }
    }
}