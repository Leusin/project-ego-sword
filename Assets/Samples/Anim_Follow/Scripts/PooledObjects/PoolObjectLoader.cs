using UnityEngine;

namespace ProjectEgoSword
{
    public enum PoolObjectType
    {
        HUMANOID_ATTACKINFO = 0,
    }

    public class PoolObjectLoader : MonoBehaviour
    {
        public static PoolObject InstantiatePrefab(PoolObjectType objType)
        {
            GameObject obj = null;

            switch (objType)
            {
                case PoolObjectType.HUMANOID_ATTACKINFO:
                    {
                        obj = Instantiate(Resources.Load("HumanoidAttackInfo") as GameObject);
                        break;
                    }
            }

            return obj.GetComponent<PoolObject>();
        }
    }
}