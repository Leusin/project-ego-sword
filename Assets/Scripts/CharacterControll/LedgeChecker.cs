using UnityEngine;

namespace ProjectEgoSword
{
    public class LedgeChecker : MonoBehaviour
    {
        public bool isGrabbingLedge;
        private Ledge _ledge = null;

        private void OnTriggerEnter(Collider other)
        {
            _ledge = other.gameObject.GetComponent<Ledge>();

            if( _ledge != null )
            {
                isGrabbingLedge = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            _ledge = other.gameObject.GetComponent<Ledge>();

            if (_ledge != null)
            {
                isGrabbingLedge = false;
                _ledge = null;
            }
        }
    }
}