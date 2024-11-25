using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace ProjectEgoSword
{
    public class CharacterManager : Singleton<CharacterManager>
    {
        public List<CharacterControl> characters = new List<CharacterControl>();

        public CharacterControl GetCharacter(Animator animator)
        {
            foreach (CharacterControl control in characters)
            {
                if (control.skinnedMeshAnimator == animator)
                {
                    return control;
                }
            }

            return null;
        }

        public CharacterControl GetCharacter(GameObject obj)
        {
            foreach (CharacterControl control in characters)
            {
                if (control.gameObject == obj)
                {
                    return control;
                }
            }

            return null;
        }
    }
}