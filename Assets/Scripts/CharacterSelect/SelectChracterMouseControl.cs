using UnityEngine;


namespace ProjectEgoSword
{
    public class SelectChracterMouseControl : MonoBehaviour
    {

        public PlayableCharacterType selectedCharacterType;
        public CharacterSelect characterSelect;

        private Ray _ray;
        private RaycastHit _hit;
        public GameObject chractorSelection;

        private CharacterSelectLight _characterSelectLight;
        private CharacterHoverLight _characterHoverLight;

        private CharacterManager _characterManger;

        private void Awake()
        {
            _characterSelectLight = FindAnyObjectByType<CharacterSelectLight>();
            _characterHoverLight = FindAnyObjectByType<CharacterHoverLight>();

            chractorSelection.SetActive(false);
        }

        private void Start()
        {
            _characterManger = CharacterManager.Instance;
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
                    var contorl = _characterManger.GetCharacter(selectedCharacterType);

                    characterSelect.selectedCharacterType = selectedCharacterType;
                    _characterSelectLight.transform.position = _characterHoverLight.transform.position;
                    _characterSelectLight.light.enabled = true;

                    chractorSelection.SetActive(true);
                    chractorSelection.transform.parent = contorl.skinnedMeshAnimator.transform;
                    chractorSelection.transform.localPosition = Vector3.zero;
                }
                else
                {
                    characterSelect.selectedCharacterType = PlayableCharacterType.NONE;
                    _characterSelectLight.light.enabled = false;
                    chractorSelection.SetActive(false);
                }

                foreach(CharacterControl c in _characterManger.characters)
                {
                    if(c.characterType == selectedCharacterType)
                    {
                        c.skinnedMeshAnimator.SetBool(CharacterControl.TransitionParameter.ClickAnimation.ToString(), true);
                    }
                    else
                    {
                        c.skinnedMeshAnimator.SetBool(CharacterControl.TransitionParameter.ClickAnimation.ToString(), false);
                    }
                }
            }
        }
    }
}