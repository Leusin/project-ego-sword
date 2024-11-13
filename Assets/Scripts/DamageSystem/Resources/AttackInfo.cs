using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectEgoSword
{
    public class AttackInfo: MonoBehaviour
    {
        public CharacterControl attacker;
        public List<string> colliderNames = new List<string>();

        public bool mustCollide;
        public bool mustFaceAttacker;
        public float lethalRange;
        public int maxHits;
        public int currentHits;
        public bool isRegisterd;
        public bool isFinished;

        public Attack attackAbility;

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
            colliderNames = attack.colliderNames;
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