using UnityEngine;

namespace ProjectEgoSword
{
    public class DamageDetector : MonoBehaviour
    {
        private CharacterControl _control;
        private GeneralBodyPart _damagedPart;

        private AttackManager _attackManager;

        private void Awake()
        {
            _control = GetComponent<CharacterControl>();
        }

        private void Start()
        {
            _attackManager = AttackManager.Instance;
        }

        private void Update()
        {
            if (_attackManager.currentAttacks.Count > 0f)
            {
                CheckAttacks();
            }
        }

        // -----

        private void CheckAttacks()
        {
            foreach (AttackInfo info in _attackManager.currentAttacks)
            {
                if (info == null)
                {
                    continue;
                }

                if (info.attacker == _control)
                {
                    continue;
                }

                if (!info.isRegisterd)
                {
                    continue;
                }

                if (info.isFinished)
                {
                    continue;
                }

                //if (info.currentHits >= info.maxHits)
                //{
                //    continue;
                //}

                if (info.mustCollide)
                {
                    if (IsCollided(info))
                    {
                        TakeDamage(info);
                    }
                }
            }
        }

        private bool IsCollided(AttackInfo info)
        {
            foreach(TriggerDetector trigger in _control.GetAllTriggers())
            {
                foreach (Collider collider in trigger.collidingParts)
                {
                    foreach (string name in info.colliderNames)
                    {
                        if (name == collider.gameObject.name)
                        {
                            _damagedPart = trigger.generalBodyPart;
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private void TakeDamage(AttackInfo info)
        {
            Debug.Log(info.attacker.gameObject.name + " hits: " + this.gameObject.name + "(" + _damagedPart.ToString() + ")");

            HumanoidAttackInfo humanoidAttackInfo = info as HumanoidAttackInfo;
            if (humanoidAttackInfo != null)
            {
                _control.skinedMeshAnimator.runtimeAnimatorController = humanoidAttackInfo.attackAbility.GetDeathAnimator();
            }

            info.currentHits++;

            _control.GetComponent<Collider>().enabled = false;
            _control.RigidbodyComponent.useGravity = false;
        }
    }
}
