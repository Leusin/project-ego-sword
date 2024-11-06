using UnityEngine;
using UnityEngine.Animations;


namespace ProjectEgoSword
{

    /// <summary>
    /// StateMachineBehaviour �� ��ӹ޾� �߰� ��ɼ��� ���� Ŭ����.
    /// StateMucine�� ���� ��ü�� �������̹��� �ҷ��� �� �ְ�,
    /// Enter / Update /  Exit �̺�Ʈ �ܿ��� �ִϸ��̼� Ʈ�����ǿ� ���� ������Ʈ�� �߰��Ǿ���.
    /// </summary>
    /// <typeparam name="TMonoBehaviour">MonoBehaviour�� ��ӹ��� Ŭ����</typeparam>
    public class SceneLinkedSMB<TMonoBehaviour> : SealedSMB
            where TMonoBehaviour : MonoBehaviour
    {
        protected TMonoBehaviour _monobehaviour;

        bool _firstFrameHappened;
        bool _lastFrameHappened;

        public static void Initialise(Animator animator, TMonoBehaviour monoBehaviour)
        {
            SceneLinkedSMB<TMonoBehaviour>[] sceneLinkedSMBs = animator.GetBehaviours<SceneLinkedSMB<TMonoBehaviour>>();

            for (int i = 0; i < sceneLinkedSMBs.Length; i++)
            {
                sceneLinkedSMBs[i].InternalInitialise(animator, monoBehaviour);
            }
        }

        protected void InternalInitialise(Animator animator, TMonoBehaviour monoBehaviour)
        {
            _monobehaviour = monoBehaviour;
            OnStart(animator);
        }

        public sealed override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
        {
            _firstFrameHappened = false;

            OnSLStateEnter(animator, stateInfo, layerIndex);
            OnSLStateEnter(animator, stateInfo, layerIndex, controller);
        }

        public sealed override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
        {
            if (!animator.gameObject.activeSelf)
                return;

            if (animator.IsInTransition(layerIndex) && animator.GetNextAnimatorStateInfo(layerIndex).fullPathHash == stateInfo.fullPathHash)
            {
                OnSLTransitionToStateUpdate(animator, stateInfo, layerIndex);
                OnSLTransitionToStateUpdate(animator, stateInfo, layerIndex, controller);
            }

            if (!animator.IsInTransition(layerIndex) && _firstFrameHappened)
            {
                OnSLStateNoTransitionUpdate(animator, stateInfo, layerIndex);
                OnSLStateNoTransitionUpdate(animator, stateInfo, layerIndex, controller);
            }

            if (animator.IsInTransition(layerIndex) && !_lastFrameHappened && _firstFrameHappened)
            {
                _lastFrameHappened = true;

                OnSLStatePreExit(animator, stateInfo, layerIndex);
                OnSLStatePreExit(animator, stateInfo, layerIndex, controller);
            }

            if (!animator.IsInTransition(layerIndex) && !_firstFrameHappened)
            {
                _firstFrameHappened = true;

                OnSLStatePostEnter(animator, stateInfo, layerIndex);
                OnSLStatePostEnter(animator, stateInfo, layerIndex, controller);
            }

            if (animator.IsInTransition(layerIndex) && animator.GetCurrentAnimatorStateInfo(layerIndex).fullPathHash == stateInfo.fullPathHash)
            {
                OnSLTransitionFromStateUpdate(animator, stateInfo, layerIndex);
                OnSLTransitionFromStateUpdate(animator, stateInfo, layerIndex, controller);
            }
        }

        public sealed override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
        {
            _lastFrameHappened = false;

            OnSLStateExit(animator, stateInfo, layerIndex);
            OnSLStateExit(animator, stateInfo, layerIndex, controller);
        }

        /// <summary>
        ///  �� �� Monobehviour �� ���� �Լ��� ȣ���� �� ���� ȣ��.
        /// </summary>
        public virtual void OnStart(Animator animator) { }

        /// ----------------------------------------------------------------------------------------------------------------------------------------
        /// Ʈ������ -> ������Ʈ 
        /// ----------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// ������Ʈ�� ������ ó�� ���۵� ��(Ʈ������ -> ������Ʈ ��) Update ������ ȣ��.
        /// </summary>
        public virtual void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) { }
        public virtual void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller) { }

        /// <summary>
        /// Ʈ������ -> ������Ʈ �߿� �� �����Ӹ��� ȣ��. OnSLStateEnter ���Ŀ� ȣ��.
        /// </summary>
        public virtual void OnSLTransitionToStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) { }
        public virtual void OnSLTransitionToStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller) { }

        /// <summary>
        /// Ʈ������ -> ������Ʈ �� ù ��° �����ӿ� ȣ��.
        /// </summary>
        public virtual void OnSLStatePostEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) { }
        public virtual void OnSLStatePostEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller) { }

        /// ----------------------------------------------------------------------------------------------------------------------------------------
        /// Update
        /// ----------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// ������Ʈ�� Ʈ������ ���� ���� ��(Ʈ������ ���� �ƴ� ��) �� �����Ӹ��� ȣ���.
        /// </summary>
        public virtual void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) { }
        public virtual void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller) { }

        /// ----------------------------------------------------------------------------------------------------------------------------------------
        /// ������Ʈ -> Ʈ������
        /// ----------------------------------------------------------------------------------------------------------------------------------------

        /// ������Ʈ -> Ʈ�������� ���۵� ù ��° �����ӿ� ȣ���. ��ȯ�� ���� �ð��� 1 �����Ӻ��� ª���� ȣ����� ����.
        public virtual void OnSLStatePreExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) { }
        public virtual void OnSLStatePreExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller) { }

        /// <summary>
        /// ������Ʈ -> Ʈ������ �߿� �� �����Ӹ��� OnSLStatePreExit ���Ŀ� ȣ��.
        /// </summary>
        public virtual void OnSLTransitionFromStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) { }
        public virtual void OnSLTransitionFromStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller) { }

        /// <summary>
        /// Called after Updates when execution of the state first finshes (after transition from the state).
        /// ���� ������Ʈ���� ������ Ʈ�������� �Ϸ�� �� ȣ��.
        /// </summary>
        public virtual void OnSLStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) { }
        public virtual void OnSLStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller) { }
    }

    // ���� ���� ��ü�� ���� ���� �������� �� �ֵ��� �Ѵ�.
    // ���°� ���� ���� ��ü�� ���� ������ �� �ִ� ���ɼ��� �߰��Ͽ� �Ź� GetComponent�� ���� �������� ����� ���� �� �ִ�.
    public abstract class SealedSMB : StateMachineBehaviour
    {
        public sealed override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) { }

        public sealed override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) { }

        public sealed override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) { }
    }
}