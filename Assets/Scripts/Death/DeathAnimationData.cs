using System.Collections.Generic;
using UnityEngine;

namespace ProjectEgoSword
{
    public enum DeathType
    {
        NONE = 0,
        LAUNCH_INTO_AIR,
        GROUND_SHOCK,
    }

    [CreateAssetMenu(fileName = "New ScriptableObject", menuName = "ProjectEgoSword/Death/DeathAnimation Data")]
    public class DeathAnimationData : ScriptableObject
    {
        public List<GeneralBodyPart> generalBodyParts = new List<GeneralBodyPart>();
        public RuntimeAnimatorController animator;
        public DeathType deathType;
        public bool isFacingAttacker;
    }
}