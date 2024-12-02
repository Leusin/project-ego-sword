using Unity.Cinemachine;
using UnityEngine;

namespace ProjectEgoSword
{
    public class PlayerSpawn : MonoBehaviour
    {
        public CharacterSelect characterSelect;

        private string _objName;

        private CharacterManager _characterManager;

        private void Start()
        {
            _characterManager = CharacterManager.Instance;

            switch (characterSelect.selectedCharacterType)
            {
                case PlayableCharacterType.CharacterR:
                    {
                        _objName = "CharactorR";
                    }
                    break;
                case PlayableCharacterType.CharactorG:
                    {
                        _objName = "CharactorG";
                    }
                    break;
                case PlayableCharacterType.CharactorB:
                    {
                        _objName = "CharactorB";
                    }
                    break;
            }

            GameObject obj = Instantiate(Resources.Load(_objName, typeof(GameObject))) as GameObject;
            obj.transform.position = transform.position;
            obj.transform.rotation = transform.rotation;
            GetComponent<MeshRenderer>().enabled = false;

            CinemachineCamera[] arr = FindObjectsByType<CinemachineCamera>(FindObjectsSortMode.None);
            foreach (CinemachineCamera c in arr)
            {
                CharacterControl contrl = _characterManager.GetCharacter(characterSelect.selectedCharacterType);
                Collider target = contrl.GetBodyPart("mixamorig:Spine1");

                c.LookAt = target.transform;
                c.Follow = target.transform;
            }
        }
    }
}