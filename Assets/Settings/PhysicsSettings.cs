using UnityEngine;

namespace ProjectEgoSword
{
    [CreateAssetMenu(fileName = "Settings", menuName = "ProjectEgoSword/Settings/PhysicsSetting")]
    public class PhysicsSettings : ScriptableObject
    {
        public int defaultSolverVelocityIterations;
    }
}