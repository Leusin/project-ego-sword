using UnityEngine;

namespace ProjectEgoSword
{
    public class CharacterHoverLight : MonoBehaviour
    {
        public Vector3 offset;

        private CharacterControl _hoverSelectedCharacter;
        private SelectChracterMouseControl _mouseHoverSelect;
        private Vector3 _TargetPos;
        private Light _light;

        private CharacterManager _characterManager;


        private void Awake()
        {
            _characterManager = CharacterManager.Instance;
        }

        private void Start()
        {
            _mouseHoverSelect = FindFirstObjectByType<SelectChracterMouseControl>();
            _light = GetComponent<Light>();
        }

        private void Update()
        {
            if(_mouseHoverSelect.selectedCharacterType == PlayableCharacterType.NONE)
            {
                _hoverSelectedCharacter = null;
                _light.enabled = false;
            }
            else
            {
                _light.enabled = true;
                LightUpSelecedCharacter();
            }
        }

        // -----

        private void LightUpSelecedCharacter()
        {
            if( _hoverSelectedCharacter == null)
            {
                _hoverSelectedCharacter = _characterManager.GetCharacter(_mouseHoverSelect.selectedCharacterType);
                transform.position = _hoverSelectedCharacter.transform.position + _hoverSelectedCharacter.transform.TransformDirection(offset);
            }
        }
    }
}