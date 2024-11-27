using UnityEngine;


namespace ProjectEgoSword
{
    public class SelectChracterMouseControl : MonoBehaviour
    {

        public PlayableCharacterType selectedCharacterType;
        public CharacterSelect characterSelect;

        private Ray _ray;
        private RaycastHit _hit;

        private CharacterSelectLight _characterSelectLight;
        private CharacterHoverLight _characterHoverLight;

        private void Awake()
        {
            _characterSelectLight = FindAnyObjectByType<CharacterSelectLight>();
            _characterHoverLight = FindAnyObjectByType<CharacterHoverLight>();
        }

        void Update()
        {
            _ray = CameraManager.Instance.mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(_ray, out _hit))
            {
                CharacterControl control = _hit.collider.gameObject.GetComponent<CharacterControl>();
                if (control != null)
                {
                    selectedCharacterType = control.characterType;
                }
                else
                {
                    selectedCharacterType = PlayableCharacterType.NONE;
                }
            }

            if(Input.GetMouseButtonDown(0))
            {
                if(selectedCharacterType != PlayableCharacterType.NONE)
                {
                    characterSelect.selectedCharacterType = selectedCharacterType;
                    _characterSelectLight.transform.position = _characterHoverLight.transform.position;
                    _characterSelectLight.light.enabled = true;
                }
                else
                {
                    characterSelect.selectedCharacterType = PlayableCharacterType.NONE;
                    _characterSelectLight.light.enabled = false;
                }
            }
        }
    }
}