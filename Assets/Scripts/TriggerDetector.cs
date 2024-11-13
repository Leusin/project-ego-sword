using System.Collections.Generic;
using UnityEngine;

namespace ProjectEgoSword
{
    public enum GeneralBodyPart
    {
        Upper,
        Lower,
        Arm,
        Leg,
    }

    public class TriggerDetector : MonoBehaviour
    {
        public GeneralBodyPart generalBodyPart;
        public List<Collider> collidingParts = new List<Collider>();

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

            if (!collidingParts.Contains(other))
            {
                collidingParts.Add(other);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (collidingParts.Contains(other))
            {
                collidingParts.Remove(other);
            }
        }
    }
}