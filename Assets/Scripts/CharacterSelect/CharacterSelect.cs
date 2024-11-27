using UnityEngine;

namespace ProjectEgoSword
{
    public enum PlayableCharacterType
    {
        NONE,
        CharacterR,
        CharactorG,
        CharactorB,
    }

    [CreateAssetMenu(fileName = "CharacterSelect", menuName = "ProjectEgoSword/CharacterSelect/CharacterSelect")]
    public class CharacterSelect : ScriptableObject
    {

        public PlayableCharacterType selectedCharacterType;
    }
}