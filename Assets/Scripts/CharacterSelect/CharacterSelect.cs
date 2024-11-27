using UnityEngine;

namespace ProjectEgoSword
{
    public enum PlayableCharacterType
    {
        NONE,
        RED,
        GREEN,
        BLUE,
    }

    [CreateAssetMenu(fileName = "CharacterSelect", menuName = "ProjectEgoSword/CharacterSelect/CharacterSelect")]
    public class CharacterSelect : ScriptableObject
    {

        public PlayableCharacterType selectedCharacterType;
    }
}