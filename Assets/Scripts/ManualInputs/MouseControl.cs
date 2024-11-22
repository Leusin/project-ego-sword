using UnityEngine;


namespace ProjectEgoSword
{
    public class MouseControl : MonoBehaviour
    {
        private Ray _ray;
        private RaycastHit _hit;

        void Awake()
        {

        }

        // Update is called once per frame
        void Update()
        {
            _ray = CameraManager.Instance.mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(_ray, out _hit))
            {
            }
        }
    }
}