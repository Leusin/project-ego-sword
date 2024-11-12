using System.Collections.Generic;
using UnityEngine;

namespace ProjectEgoSword
{
    [CreateAssetMenu(fileName = "StateData", menuName = "ProjectEgoSword/AbilityData/Humanoid Attack")]
    public class HumanoidAttack : StateData<HumanoidController>
    {
        public float startAttackTime;
        public float endAttackTime;
        public List<string> colliderNames = new List<string>();

        public bool mustCollide;
        public bool mustFaceAttacker;
        public float lethalRange;
        public int maxHits;

        public List<RuntimeAnimatorController> deathAnimators = new List<RuntimeAnimatorController>();

        private AttackManager _attackManager;

        public override void OnStart(Animator animator)
        {
            _attackManager = AttackManager.Instance;
        }

        public override void OnEnter(HumanoidController monoBehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool(monoBehaviour.hashAttack, false);

            GameObject obj = Instantiate(Resources.Load("HumanoidAttackInfo", typeof(GameObject))) as GameObject;
            HumanoidAttackInfo info = obj.GetComponent<HumanoidAttackInfo>();

            info.ResetInfo(this, monoBehaviour);

            if (!_attackManager.currentAttacks.Contains(info))
            {
                _attackManager.currentAttacks.Add(info);
            }
        }

        public override void UpdateAbility(HumanoidController monoBehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            RegisterAttack(monoBehaviour, animator, stateInfo, layerIndex);
            DeregisterAttack(monoBehaviour, animator, stateInfo, layerIndex);
        }

        public override void OnExit(HumanoidController monoBehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            ClearAttack();
        }

        // ---

        public void RegisterAttack(HumanoidController monoBehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (startAttackTime <= stateInfo.normalizedTime && endAttackTime > stateInfo.normalizedTime)
            {
                foreach (AttackInfo info in _attackManager.currentAttacks)
                {
                    HumanoidAttackInfo humanoidAttackInfo = info as HumanoidAttackInfo;

                    if (humanoidAttackInfo == null)
                    {
                        continue;
                    }

                    if (!info.isRegisterd && humanoidAttackInfo.attackAbility == this)
                    {
                        humanoidAttackInfo.Register(this);
                    }
                }
            }
        }

        public void DeregisterAttack(HumanoidController monoBehaviour, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if(stateInfo.normalizedTime >= endAttackTime)
            {
                foreach (AttackInfo info in _attackManager.currentAttacks)
                {
                    HumanoidAttackInfo humanoidAttackInfo = info as HumanoidAttackInfo;

                    if (humanoidAttackInfo == null)
                    {
                        continue;
                    }

                    if (humanoidAttackInfo.attackAbility == this && !info.isFinished)
                    {
                        info.isFinished = true;
                        Destroy(info.gameObject);
                    }
                }
            }
        }

        public void ClearAttack()
        {
            for (int i =0; i < _attackManager.currentAttacks.Count; i++)
            {
                _attackManager.currentAttacks.RemoveAt(i);
            }
        }

        public RuntimeAnimatorController GetDeathAnimator()
        {
            int index = Random.Range(0, deathAnimators.Count);
            return deathAnimators[index];
        }
    }
}