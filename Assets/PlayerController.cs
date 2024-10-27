using Unity.Physics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

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

    private Rigidbody m_rigidbody;
    private LayerMask m_layerEncounter;

    private PlayerInput m_playerinput;
    private PlayerInputController m_playerInputCtrl;

    // -----

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();

        m_layerEncounter = LayerMask.GetMask("Encounter");

        m_playerinput = GetComponent<PlayerInput>();
        m_playerInputCtrl = GetComponent<PlayerInputController>();
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
            float x = Screen.width - actionMapSize.x - 200; // 오른쪽 여백 10 픽셀
            float y = 10; // 위쪽 여백 10 픽셀

            // 라벨을 화면에 표시
            GUI.Label(new Rect(x, y, actionMapSize.x, actionMapSize.y), actionMapName);
            GUI.Label(new Rect(x, y + actionMapSize.y + 5, controlSchemeSize.x, controlSchemeSize.y), controlSchemeName);
        }
        else
        {
            GUI.Label(new Rect(10, 10, 300, 20), "PlayerInput or Action Map is not set.");
        }
    }

    private void OnEnable()
    {
        m_playerInputCtrl.Attaract += Attaract;
    }

    public void OnDisable()
    {
        m_playerInputCtrl.Attaract -= Attaract;
    }

    private void Update()
    {
        if(m_isAttaract)
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

    private void OnCollisionEnter(Collision collision)
    {
        //TEST
        LayerMask mask = LayerMask.GetMask("Encounter");
        if ((mask.value & (1 << collision.gameObject.layer)) != 0)
        {
            var encounterCtrl = collision.gameObject.GetComponent<EncounterController>();
            if (encounterCtrl != null)
            {
                encounterCtrl.enabled = true;
                m_owner = encounterCtrl.gameObject;
                encounterCtrl.Equip(this);
                m_playerinput.SwitchCurrentActionMap("Man");
                attractRange = 0f;
                m_rigidbody.isKinematic = true;
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

    private void Attaract(InputAction.CallbackContext context)
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
}
