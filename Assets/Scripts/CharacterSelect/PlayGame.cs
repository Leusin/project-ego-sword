using System.Collections;
using UnityEngine;

namespace ProjectEgoSword
{
    public class PlayGame : MonoBehaviour
    {
        public CharacterSelect characterSelect;

        void Update()
        {
            if(Input.GetKeyDown(KeyCode.Return))
            {
                if(characterSelect.selectedCharacterType != PlayableCharacterType.NONE)
                {
                    UnityEngine.SceneManagement.SceneManager.LoadScene(ProjectScenes.SampleScene_CharacterSelect.ToString());
                }
                else
                {
                    Debug.Log("캐릭터를 선택하삼");
                }
            }
        }
    }
}