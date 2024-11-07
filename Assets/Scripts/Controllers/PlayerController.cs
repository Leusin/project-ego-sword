using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace ProjectEgoSword
{

    public class PlayerController : MonoBehaviour
    {
        public bool drawGizmo;

        [HideInInspector]
        private GameObject m_owner;

        [Header("AttractRange")]
        private float attractRange;
        public float maxAttaractRange = 10f;
        public float attaractAcceleration = 10f;
        public float attaractDeceleration = 8f;

        private bool m_isAttaract;
        private bool m_isLook;

        [SerializeField] private float m_timer;

        private Vector2 m_lookPosition;

        private Rigidbody m_rigidbody;
        private LayerMask m_layerEncounter;

        private PlayerInput m_playerinput;
        private InputController m_playerInputCtrl;

        [Header("Cinemachine")]
        public CinemachinePositionComposer cinemachinePositionComposer;
        private Vector2 m_screenPosition;

        [Header("Look")]
        [SerializeField] private float m_cameraMoveSpeed = 0.5f; // 카메라 이동 속도
        [SerializeField] private float m_resetCameraTime = 2f;
        private float m_LookTimer;

        [SerializeField] private Vector2 m_moveRangeMax = new Vector2(0.2f, 0.15f);
        [SerializeField] private Vector2 m_moveRangeMin = new Vector2(-0.2f, -0.1f);

        // -----

        private void Awake()
        {
            m_rigidbody = GetComponent<Rigidbody>();

            m_layerEncounter = LayerMask.GetMask("Encounter");

            m_playerinput = GetComponent<PlayerInput>();
            m_playerInputCtrl = GetComponent<InputController>();

            m_screenPosition = cinemachinePositionComposer.Composition.ScreenPosition;
        }

        private void Start()
        {
            if (m_playerinput.currentActionMap.name == "Karl")
            {
                cinemachinePositionComposer.Composition.DeadZone.Enabled = false;
            }
        }

        private void OnEnable()
        {
        }

        public void OnDisable()
        {
        }

        private void Update()
        {
            //Debug.Log($"플레이어 컨트롤러는 업데이트중");

            Attract();
            // TEST - Look
            //Look();
        }
        private void OnGUI()
        {
            if (m_playerinput != null && m_playerinput.currentActionMap != null)
            {
                string actionMapName = "Current Action Map: " + m_playerinput.currentActionMap.name;
                string controlSchemeName = "Current Control Scheme: " + m_playerinput.currentControlScheme;

                // 라벨의 크기 계산
                Vector2 actionMapSize = GUI.skin.label.CalcSize(new GUIContent(actionMapName));
                Vector2 controlSchemeSize = GUI.skin.label.CalcSize(new GUIContent(controlSchemeName));

                // 화면의 우상단에 위치 설정
                float x = Screen.width - actionMapSize.x - 200f; // 오른쪽 여백 10 픽셀
                float y = 50f; // 위쪽 여백 10 픽셀

                // 라벨을 화면에 표시
                GUI.Label(new Rect(x, y, actionMapSize.x, actionMapSize.y), actionMapName);
                GUI.Label(new Rect(x, y + actionMapSize.y + 5f, controlSchemeSize.x, controlSchemeSize.y), controlSchemeName);
            }
            else
            {
                GUI.Label(new Rect(10f, 10f, 300f, 20f), "PlayerInput or Action Map is not set.");
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            //TEST
            LayerMask mask = LayerMask.GetMask("Encounter");
            if ((mask.value & (1 << collision.gameObject.layer)) != 0)
            {
                var humanoidCtrl = collision.gameObject.GetComponent<HumanoidController>();
                if (humanoidCtrl != null)
                {
                    humanoidCtrl.enabled = true;
                    humanoidCtrl.Equip(this);

                    m_owner = humanoidCtrl.gameObject;
                    m_rigidbody.isKinematic = true;
                    m_playerinput.SwitchCurrentActionMap("Humanoid");

                    // Attrack 초기화
                    attractRange = 0f;

                    // Look 초기화
                    m_isLook = false;
                    cinemachinePositionComposer.Composition.DeadZone.Enabled = true;
                }
            }
        }

        private void OnDrawGizmos()
        {
            if (drawGizmo == false)
                return;

            Gizmos.color = new Color(0.5f, 0, 0.5f, 0.5f);
            Gizmos.DrawWireSphere(transform.position, attractRange);
        }

        // -----

        private void OnAttaract(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                m_isAttaract = true;
            }
            else if (context.canceled)
            {
                m_isAttaract = false;
            }
        }

        private void Attract()
        {
            if (m_isAttaract)
            {
                if (attractRange < maxAttaractRange)
                {
                    attractRange += Time.deltaTime * attaractAcceleration;
                }
                else
                {
                    attractRange = maxAttaractRange;
                }

                // Temp
                UnityEngine.Collider[] hitColliders = Physics.OverlapSphere(transform.position, attractRange);
                foreach (var hitCollider in hitColliders)
                {
                    var navMeshAgent = hitCollider.GetComponent<NavMeshAgent>();
                    if (navMeshAgent != null)
                    {
                        navMeshAgent.SetDestination(transform.position);
                    }
                }
            }
            else
            {
                // 매 프레임마다 attractRange를 감소시킴
                attractRange = Mathf.Max(0f, attractRange - Time.deltaTime * attaractDeceleration);
            }
        }

        private void Look()
        {
            if (m_LookTimer < 0f)
            {
                m_isLook = false;
            }
            else
            {
                m_LookTimer -= Time.deltaTime;
            }

            float scaledMoveSpeed = m_cameraMoveSpeed * Time.deltaTime;

            if (m_isLook)
            {
                m_screenPosition = cinemachinePositionComposer.Composition.ScreenPosition;

                m_screenPosition += m_lookPosition * scaledMoveSpeed;

                m_screenPosition.x = Mathf.Clamp(m_screenPosition.x, m_moveRangeMin.x, m_moveRangeMax.x);
                m_screenPosition.y = Mathf.Clamp(m_screenPosition.y, m_moveRangeMin.y, m_moveRangeMax.y);

            }
            else
            {
                m_screenPosition = Vector2.zero;
            }

            cinemachinePositionComposer.Composition.ScreenPosition = Vector2.MoveTowards(cinemachinePositionComposer.Composition.ScreenPosition, m_screenPosition, scaledMoveSpeed);
        }
    }
}