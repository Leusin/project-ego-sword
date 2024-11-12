using UnityEngine;

namespace ProjectEgoSword
{
    public class HumanoidAttackInfo : AttackInfo
    {
        public HumanoidAttack attackAbility;

        public void ResetInfo(HumanoidAttack attack)
        {
            base.ResetInfo();
            attackAbility = attack;
        }

        public void Register(HumanoidAttack attack, HumanoidController attacker)
        {
            isRegisterd = true;
            this.attacker = attacker;

            attackAbility = attack;
            colliderNames = attack.colliderNames;
            mustCollide = attack.mustCollide;
            mustFaceAttacker = attack.mustFaceAttacker;
            lethalRange = attack.lethalRange;
            maxHits = attack.maxHits;
            currentHits = 0;
        }
    }
}
