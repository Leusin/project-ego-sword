using System.Collections.Generic;
using UnityEngine;

namespace ProjectEgoSword
{
    [CreateAssetMenu(fileName = "New ScriptableObject", menuName = "ProjectEgoSword/Death/DeathAnimation Data")]
    public class DeathAnimationData : ScriptableObject
    {
        public List<GeneralBodyPart> generalBodyParts = new List<GeneralBodyPart>();
        public RuntimeAnimatorController animator;
        public bool lunchIntoAir;
        public bool isFacingAttacker;
    }
}