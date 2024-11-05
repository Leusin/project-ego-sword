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

                    Debug.Log("싱글턴 객체( " + typeof(T).ToString() + " )을/를 찾았습니다.");
                }
                else
                {
                    Debug.Log("싱글턴 객체( " + typeof(T).ToString() + " )을/를 찾지 못했습니다.");
                }


                return _instance;
            }
        }
    }
}