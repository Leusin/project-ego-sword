using UnityEngine;

namespace ProjectEgoSword
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = (T)FindFirstObjectByType(typeof(T));
                    if (_instance != null )
                    {
                        Debug.Log("�̱��� ��ü( " + typeof(T).ToString() + " )��/�� Hierarchy���� ã�ҽ��ϴ�.");
                    }
                }

                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    _instance = obj.AddComponent<T>();
                    obj.name = typeof(T).ToString();
                    Debug.Log("�̱��� ��ü( " + typeof(T).ToString() + " )��/�� ã�� ���߽��ϴ�. ���ο� ��ü�� ����ϴ�.");
                }

                return _instance;
            }
        }
    }
}