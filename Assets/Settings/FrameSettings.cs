using UnityEngine;

namespace ProjectEgoSword
{
    [CreateAssetMenu(fileName = "Settings", menuName ="ProjectEgoSword/Settings/FrameSettings")]
    public class FrameSettings : ScriptableObject
    {
        [Range(0.01f, 1f)]
        public float timeScale;
        public int targetFPS;
    }
}