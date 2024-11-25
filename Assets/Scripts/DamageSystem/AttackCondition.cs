using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectEgoSword
{
    public class AttackCondition: MonoBehaviour
    {
        public CharacterControl attacker;
        public Attack attackAbility;
        public DeathType deathType;

        public bool mustCollide;
        public bool mustFaceAttacker;
        public float lethalRange;
        public int maxHits;
        public int currentHits;
        public bool isRegisterd;
        public bool isFinished;

        public List<AttackPartType> AttackParts = new List<AttackPartType>();
        public List<string> colliderNames = new List<string>();

        public void ResetInfo(Attack attack, CharacterControl attacker)
        {
            this.attacker = attacker;
            attackAbility = attack;
            isRegisterd = false;
            isFinished = false;
        }

        public void Register(Attack attack)
        {
            isRegisterd = true;

            attackAbility = attack;
            AttackParts = attack.attackParts;
            mustCollide = attack.mustCollide;
            mustFaceAttacker = attack.mustFaceAttacker;
            lethalRange = attack.lethalRange;
            maxHits = attack.maxHits;
            currentHits = 0;
        }

        public void CopyInfo(Attack attack, CharacterControl attacker)
        {
            this.attacker = attacker;
            attackAbility = attack;
            AttackParts = attack.attackParts;
            mustCollide = attack.mustCollide;
            mustFaceAttacker = attack.mustFaceAttacker;
            lethalRange = attack.lethalRange;
            maxHits = attack.maxHits;
            currentHits = 0;
        }

        // -----

        private void OnDisable()
        {
            isFinished = true;
        }
    }
}