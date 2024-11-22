using System.Collections;
using UnityEngine;

namespace ProjectEgoSword
{
    public class CameraManager : Singleton<CameraManager>
    {
        public CameraController CameraController
        {
            get
            {
                if (_cameraController == null)
                {
                    _cameraController = GameObject.FindFirstObjectByType<CameraController>();
                }

                return _cameraController;
            }
        }
        private CameraController _cameraController;

        private Coroutine _routine;

        // -----

        public void ShakeCamera(float sec)
        {
            if(_routine != null)
            {
               StopCoroutine( _routine);
            }

            _routine = StartCoroutine(CameraShake(sec));
        }

        private IEnumerator CameraShake(float sec)
        {
            CameraController.TriggerCamera(CameraController.Trigger.Shake);
            yield return new WaitForSeconds(sec);
            CameraController.TriggerCamera(CameraController.Trigger.Default);
        }
    }
}