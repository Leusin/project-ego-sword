using UnityEngine;

namespace ProjectEgoSword
{
    public class LedgeChecker : MonoBehaviour
    {
        public bool isGrabbingLedge;
        public Ledge grabbingLedge = null;
        private Ledge _checkLedge = null;

        private void OnTriggerEnter(Collider other)
        {
            _checkLedge = other.gameObject.GetComponent<Ledge>();

            if( _checkLedge != null )
            {
                grabbingLedge = _checkLedge;
                isGrabbingLedge = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            _checkLedge = other.gameObject.GetComponent<Ledge>();

            if (_checkLedge != null)
            {
                isGrabbingLedge = false;
                _checkLedge = null;
            }
        }
    }
}