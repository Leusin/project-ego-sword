using UnityEngine;

namespace ProjectEgoSword
{
    public class HumanoidAttackInfo : AttackInfo
    {
        public HumanoidAttack attackAbility;

        public void ResetInfo(HumanoidAttack attack, CharacterControl attacker)
        {
            base.ResetInfo();
            attackAbility = attack;
            this.attacker = attacker;
        }

        public void Register(HumanoidAttack attack)
        {
            isRegisterd = true;

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
