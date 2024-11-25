using UnityEngine;

namespace ProjectEgoSword
{
    public enum PoolObjectType
    {
        AttackInfo = 0,
        Axe,
        DustExplosion,
    }

    public class PoolObjectLoader : MonoBehaviour
    {
        public static PoolObject InstantiatePrefab(PoolObjectType objType)
        {
            GameObject obj = null;

            obj = Instantiate(Resources.Load(objType.ToString()) as GameObject);

            //switch (objType)
            //{
            //    case PoolObjectType.AttackInfo:
            //        {
            //            obj = Instantiate(Resources.Load("AttackInfo") as GameObject);
            //            break;
            //        }
            //        case PoolObjectType.Axe:
            //        {
            //            obj = Instantiate(Resources.Load("Axe") as GameObject);
            //            break;
            //        }
            //}

            return obj.GetComponent<PoolObject>();
        }
    }
}