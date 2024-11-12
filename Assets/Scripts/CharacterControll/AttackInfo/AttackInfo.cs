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

        protected void ResetInfo()
        {
            isRegisterd = false;
            isFinished = false;
        }
    }
}