using System.Collections.Generic;
using UnityEngine;


namespace ProjectEgoSword
{
    public enum TransitionConditionType
    {
        UP,
        DOWN,
        LEFT,
        RIGHT,
        ATTACK,
        JUMP,
        GRABBING_LEDGE,
        LEFT_OR_RIGHT,
    }

    [CreateAssetMenu(fileName = "New State", menuName = "ProjectEgoSword/AbilityData/TransitionIndexer")]
    public class TransitionIndexer : StateData<CharacterControl>
    {
        public int index;
        public List<TransitionConditionType> conditions = new List<TransitionConditionType>();

        public override void OnEnter(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if(MakeTransition(monobehaviour))
            {
                animator.SetInteger(CharacterControl.TransitionParameter.TransitionIndex.ToString(), index);
            }
        }

        public override void UpdateAbility(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (MakeTransition(monobehaviour))
            {
                animator.SetInteger(CharacterControl.TransitionParameter.TransitionIndex.ToString(), index);
            }
            else
            {
                animator.SetInteger(CharacterControl.TransitionParameter.TransitionIndex.ToString(), 0);
            }
        }

        public override void OnExit(CharacterControl monobehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetInteger(CharacterControl.TransitionParameter.TransitionIndex.ToString(), 0);
        }

        // -----

        private bool MakeTransition(CharacterControl monobehaviour)
        {
            foreach (TransitionConditionType c in conditions)
            {
                switch (c)
                {
                    case TransitionConditionType.UP:
                        {
                            if(!monobehaviour.moveUp)
                            {
                                return false;
                            }
                        }
                        break;
                    case TransitionConditionType.DOWN:
                        {
                            if (!monobehaviour.moveDown)
                            {
                                return false;
                            }
                        }
                        break;
                    case TransitionConditionType.LEFT:
                        {
                            if (!monobehaviour.moveLeft)
                            {
                                return false;
                            }
                        }
                        break;
                    case TransitionConditionType.RIGHT:
                        {
                            if (!monobehaviour.moveRight)
                            {
                                return false;
                            }
                        }
                        break;
                    case TransitionConditionType.ATTACK:
                        {
                            if (!monobehaviour.animationProgress.attackTriggerd)
                            {
                                return false;
                            }
                        }
                        break;
                    case TransitionConditionType.JUMP:
                        {
                            if (!monobehaviour.jump)
                            {
                                return false;
                            }
                        }
                        break;
                    case TransitionConditionType.GRABBING_LEDGE:
                        {
                            if(!monobehaviour.ledgeChecker.isGrabbingLedge)
                            {
                                return false;
                            }
                        }
                        break;
                    case TransitionConditionType.LEFT_OR_RIGHT:
                        {
                            if(!monobehaviour.moveLeft && !monobehaviour.moveRight)
                            {
                                return false;
                            }
                        }
                        break;
                }
            }
            return true;
        }
    }
}