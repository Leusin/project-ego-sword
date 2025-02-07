using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectEgoSword
{
    public enum AIType
    {
        WalkAndJump,
        Run,
    }

    public class AIController : MonoBehaviour
    {
        public List<AISubset> aiList = new List<AISubset>();
        public AIType initialAI;

        void Awake()
        {
            AISubset[] arr = GetComponentsInChildren<AISubset>();

            foreach (AISubset s in arr)
            {
                if (!aiList.Contains(s))
                {
                    aiList.Add(s);
                    s.gameObject.SetActive(false);
                }
            }
        }

        private IEnumerator Start()
        {
            yield return new WaitForEndOfFrame();

            TriggerAI(initialAI);
        }

        public void TriggerAI(AIType type)
        {
            AISubset next = null;

            foreach (AISubset s in aiList)
            {
                s.gameObject.SetActive(false);

                if (s.aiType == type)
                {
                    next = s;
                }
            }

            if (next != null)
            {
                next.gameObject.SetActive(true);
            }
        }
    }

}