using UnityEngine;

namespace ProjectEgoSword
{
    public class DamageDetector : MonoBehaviour
    {
        private CharacterControl _control;
        private GeneralBodyPart _damagedPart;

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
            foreach (AttacCondition info in _attackManager.currentAttacks)
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
                else
                {
                    float dist = Vector3.SqrMagnitude(gameObject.transform.position - info.attacker.transform.position);
                    Debug.Log(gameObject.name + " distance: " + dist.ToString());

                    if (dist <= info.lethalRange)
                    {

                    }
                }
            }
        }

        private bool IsCollided(AttacCondition info)
        {
            foreach (TriggerDetector trigger in _control.GetAllTriggers())
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

        private void TakeDamage(AttacCondition info)
        {
            if (info.mustCollide)
            {
                CameraManager.Instance.ShakeCamera(0.25f);
            }

            Debug.Log(info.attacker.gameObject.name + " hits: " + this.gameObject.name + "(" + _damagedPart.ToString() + ")");

            _control.skinedMeshAnimator.runtimeAnimatorController = _deathAnimationManager.GetAnimator(_damagedPart, info);
            info.currentHits++;

            _control.GetComponent<Collider>().enabled = false;
            _control.RigidbodyComponent.useGravity = false;
        }
    }
}
