using UnityEngine;

namespace ProjectEgoSword
{
    public class DamageDetector : MonoBehaviour
    {
        private CharacterControl _control;
        private GeneralBodyPart _damagedPart;

        public int damageTaken;

        private AttackManager _attackManager;
        private DeathAnimationManager _deathAnimationManager;

        private void Awake()
        {
            _control = GetComponent<CharacterControl>();
        }

        private void Start()
        {
            _attackManager = AttackManager.Instance;
            _deathAnimationManager = DeathAnimationManager.Instance;
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
            foreach (AttackCondition info in _attackManager.currentAttacks)
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

                if (info.currentHits >= info.maxHits)
                {
                    continue;
                }

                if(info.mustFaceAttacker)
                {
                    Vector3 vec = transform.position - info.attacker.transform.position;
                    if(vec.z * info.attacker.transform.forward.z < 0f)
                    {
                        continue;
                    }
                }

                // 이거 작동 잘 안된다
                if (info.mustCollide)
                {
                    if (IsCollided(info))
                    {
                        TakeDamage(info);
                    }
                }
                else
                {
                    float dist = Vector3.SqrMagnitude(gameObject.transform.position - info.attacker.transform.position);

                    if (dist <= info.lethalRange)
                    {
                        TakeDamage(info);
                    }
                }
            }
        }

        private bool IsCollided(AttackCondition info)
        {
            foreach (TriggerDetector trigger in _control.GetAllTriggers())
            {
                foreach (Collider collider in trigger.collidingParts)
                {
                    foreach (AttackPartType part in info.AttackParts)
                    {
                        if (part == AttackPartType.LEFT_HAND)
                        {
                            if(collider.gameObject == info.attacker.leftHandAttack)
                            {
                                _damagedPart = trigger.generalBodyPart;
                                return true;
                            }
                        }
                        else if(part == AttackPartType.RIGHT_HAND)
                        {
                            if(collider.gameObject == info.attacker.rightHandAttack)
                            {
                                _damagedPart = trigger.generalBodyPart;
                                return true;
                            }
                        }
                        else if(part == AttackPartType.LEFT_FOOT)
                        {
                            if(collider.gameObject == info.attacker.leftfootAttack)
                            {
                                _damagedPart = trigger.generalBodyPart;
                                return true;
                            }
                        }
                        else if(part == AttackPartType.RIGHT_FOOT)
                        {
                            if(collider.gameObject == info.attacker.rightfootAttack)
                            {
                                _damagedPart = trigger.generalBodyPart;
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

        private void TakeDamage(AttackCondition info)
        {
            // TODO - HP system �� ����ٸ� ��ü �� ��
            if(damageTaken > 0)
            {
                return;
            }

            if (info.mustCollide)
            {
                CameraManager.Instance.ShakeCamera(0.25f);
            }

            Debug.Log(info.attacker.gameObject.name + " hits: " + this.gameObject.name + "(" + _damagedPart.ToString() + ")");

            _control.skinnedMeshAnimator.runtimeAnimatorController = _deathAnimationManager.GetAnimator(_damagedPart, info);
            info.currentHits++;

            _control.GetComponent<Collider>().enabled = false;
            _control.ledgeChecker.GetComponent<Collider>().enabled = false;
            _control.RigidbodyComponent.useGravity = false;
            _control.navMeshObstacle.carving = false;

            damageTaken++;
        }
    }
}
