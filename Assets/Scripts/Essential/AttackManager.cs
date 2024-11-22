using System.Collections.Generic;
using UnityEngine;

namespace ProjectEgoSword
{
    public class AttackManager : Singleton<AttackManager>
    {
        public List<AttacCondition> currentAttacks = new List<AttacCondition>();
    }
}