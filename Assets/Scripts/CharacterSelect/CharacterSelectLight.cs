using System.Collections;
using UnityEngine;

namespace ProjectEgoSword
{
    public class CharacterSelectLight : MonoBehaviour
    {
        public Light light;

        void Start()
        {
            if(light == null)
            {
                light = GetComponent<Light>();
            }
            light.enabled = false;
        }
    }
}