using System.Collections.Generic;
using UnityEngine;

namespace ProjectEgoSword
{
    [CreateAssetMenu(fileName = "StateData", menuName = "ProjectEgoSword/AbilityData/Attack")]
    public class Attack : StateData<CharacterControl>
    {
        public float startAttackTime;
        public float endAttackTime;
        public List<string> colliderNames = new List<string>();

        public bool mustCollide;
        public bool mustFaceAttacker;
        public float lethalRange;
        public int maxHits;

        // -----

        private List<AttackInfo> _finishedAttacks = new List<AttackInfo>();

        private AttackManager _attackManager;
        private PoolManager _poolManager;

        public override void OnStart(Animator animator)
        {
            _attackManager = AttackManager.Instance;
            _poolManager = PoolManager.Instance;
        }

        public override void OnEnter(CharacterControl monoBehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool(CharacterControl.TransitionParameter.Attack.ToString(), false);

            GameObject obj = _poolManager.GetObject(PoolObjectType.ATTACKINFO);
            AttackInfo info = obj.GetComponent<AttackInfo>();

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
        }

        public override void OnExit(CharacterControl monoBehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            ClearAttack();
        }

        // ---

        public void CheckCombo(CharacterControl monoBehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (stateInfo.normalizedTime >= startAttackTime + (endAttackTime - startAttackTime) / 3f)
            {
                if (stateInfo.normalizedTime < endAttackTime)
                {
                    CharacterControl control = monoBehaviour;

                    if (control.attack)
                    {
                        Debug.Log("uppercut triggered");
                    }
                }
            }
        }

        public void RegisterAttack(CharacterControl monoBehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (startAttackTime <= stateInfo.normalizedTime && endAttackTime > stateInfo.normalizedTime)
            {
                foreach (AttackInfo info in _attackManager.currentAttacks)
                {
                    if (!info.isRegisterd && info.attackAbility == this)
                    {
                        info.Register(this);
                    }
                }
            }
        }

        public void DeregisterAttack(CharacterControl monoBehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (stateInfo.normalizedTime >= endAttackTime)
            {
                foreach (AttackInfo info in _attackManager.currentAttacks)
                {
                    if (info == null)
                    {
                        continue;
                    }

                    if (info.attackAbility == this && !info.isFinished)
                    {
                        info.isFinished = true;
                        info.GetComponent<PoolObject>().TurnOff();
                    }
                }
            }
        }

        public void ClearAttack()
        {
            _finishedAttacks.Clear();

            foreach (AttackInfo info in _attackManager.currentAttacks)
            {
                if (info == null || info.isFinished)
                {
                    _finishedAttacks.Add(info);
                }
            }

            foreach (AttackInfo info in _finishedAttacks)
            {
                if (_attackManager.currentAttacks.Contains(info))
                {
                    _attackManager.currentAttacks.Remove(info);
                }
            }
        }
    }
}