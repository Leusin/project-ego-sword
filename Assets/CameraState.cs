using UnityEngine;

namespace ProjectEgoSword
{
    public class CameraState : StateMachineBehaviour
    {
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            CameraController.Trigger [] arr = System.Enum.GetValues(typeof(CameraController.Trigger)) as CameraController.Trigger[];

            foreach (CameraController.Trigger t in arr) 
            {
                CameraManager.Instance.CameraController.AnimatorComponent.ResetTrigger(t.ToString());
            }
        }
    }
}
