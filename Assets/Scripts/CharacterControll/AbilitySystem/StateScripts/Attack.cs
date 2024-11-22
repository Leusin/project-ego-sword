using System.Collections.Generic;
using UnityEngine;

namespace ProjectEgoSword
{
    [CreateAssetMenu(fileName = "StateData", menuName = "ProjectEgoSword/AbilityData/Attack")]
    public class Attack : StateData<CharacterControl>
    {
        public bool debug;

        public float startAttackTime;
        public float endAttackTime;
        public List<string> colliderNames = new List<string>();

        public bool lunchIntoAir;
        public bool mustCollide;
        public bool mustFaceAttacker;
        public float lethalRange;
        public int maxHits;

        // -----

        private List<AttacCondition> _finishedAttacks = new List<AttacCondition>();

        private AttackManager _attackManager;
        private PoolManager _poolManager;

        public override void OnStart(CharacterControl monobehavior, Animator animator)
        {
            _attackManager = AttackManager.Instance;
            _poolManager = PoolManager.Instance;
        }

        public override void OnEnter(CharacterControl monoBehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool(CharacterControl.TransitionParameter.Attack.ToString(), false);

            GameObject obj = _poolManager.GetObject(PoolObjectType.ATTACKINFO);
            AttacCondition info = obj.GetComponent<AttacCondition>();

            obj.SetActive(true);
            info.ResetInfo(this, monoBehaviour);

            if (!_attackManager.currentAttacks.Contains(info))
            {
                _attackManager.currentAttacks.Add(info);
            }
        }

        public override void UpdateAbility(CharacterControl monoBehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            RegisterAttack(monoBehaviour, animator, stateInfo, layerIndex);
            DeregisterAttack(monoBehaviour, animator, stateInfo, layerIndex);
            CheckCombo(monoBehaviour, animator, stateInfo, layerIndex);
        }

        public override void OnExit(CharacterControl monoBehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            ClearAttack();
            animator.SetBool(CharacterControl.TransitionParameter.Attack.ToString(), false);
        }

        // ---

        public void CheckCombo(CharacterControl monoBehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (stateInfo.normalizedTime >= startAttackTime + ((endAttackTime - startAttackTime) * 0.3f))
            {
                if (stateInfo.normalizedTime < endAttackTime + ((endAttackTime - startAttackTime) * 0.5f))
                {
                    if (monoBehaviour.attack)
                    {
                        animator.SetBool(CharacterControl.TransitionParameter.Attack.ToString(), true);
                        if (debug)
                        {
                            Debug.Log("uppercut triggered");
                        }
                    }
                }
            }
        }

        public void RegisterAttack(CharacterControl monoBehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (startAttackTime <= stateInfo.normalizedTime && endAttackTime > stateInfo.normalizedTime)
            {
                foreach (AttacCondition info in _attackManager.currentAttacks)
                {
                    if (!info.isRegisterd && info.attackAbility == this)
                    {
                        if (debug)
                        {
                            Debug.Log(this.name + " registered: " + stateInfo.normalizedTime);
                        }

                        info.Register(this);
                    }
                }
            }
        }

        public void DeregisterAttack(CharacterControl monoBehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (stateInfo.normalizedTime >= endAttackTime)
            {
                foreach (AttacCondition info in _attackManager.currentAttacks)
                {
                    if (info == null)
                    {
                        continue;
                    }

                    if (info.attackAbility == this && !info.isFinished)
                    {
                        info.isFinished = true;
                        info.GetComponent<PoolObject>().TurnOff();

                        if (debug)
                        {
                            Debug.Log(this.name + " de-registered: " + stateInfo.normalizedTime);
                        }
                    }
                }
            }
        }

        public void ClearAttack()
        {
            _finishedAttacks.Clear();

            foreach (AttacCondition info in _attackManager.currentAttacks)
            {
                if (info == null || info.attackAbility == this)
                {
                    _finishedAttacks.Add(info);
                }
            }

            foreach (AttacCondition info in _finishedAttacks)
            {
                if (_attackManager.currentAttacks.Contains(info))
                {
                    _attackManager.currentAttacks.Remove(info);
                }
            }
        }
    }
}