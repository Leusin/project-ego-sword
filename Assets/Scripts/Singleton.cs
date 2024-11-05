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
                _instance = (T)FindFirstObjectByType(typeof(T));

                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    _instance = obj.AddComponent<T>();
                    obj.name = typeof(T).ToString();

                    Debug.Log("�̱��� ��ü( " + typeof(T).ToString() + " )��/�� ã�ҽ��ϴ�.");
                }
                else
                {
                    Debug.Log("�̱��� ��ü( " + typeof(T).ToString() + " )��/�� ã�� ���߽��ϴ�.");
                }


                return _instance;
            }
        }
    }
}