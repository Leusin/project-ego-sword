using UnityEngine;
using UnityEngine.Rendering;

namespace ProjectEgoSword
{
    public class Ledge : MonoBehaviour
    {
        public Vector3 offeset;
        public Vector3 endPosition;

        public static bool IsLedge(GameObject obj)
        {
            if (obj.GetComponent<Ledge>() == null)
            {
                return false;
            }

            return true;
        }
    }
}