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
    public float maxAttractRange = 10f;
    public float attractAcceleration = 10f;

    private LayerMask m_layerEncounter;

    private Rigidbody m_rigidbody;
    private PlayerInput m_playerinput;
    private PlayerInputController m_playerInputCtrl;

    // -----


    // -----

    private void Awake()
    {
        m_playerinput = GetComponent<PlayerInput>();
        m_rigidbody = GetComponent<Rigidbody>();

        m_layerEncounter = LayerMask.GetMask("Encounter");
    }

    private void Start()
    {
        m_playerInputCtrl = PlayerInputController.Instance;
    }

    private void Update()
    {
        Attaract();
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
                m_playerinput.SwitchCurrentActionMap("Encounter");
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

    private void Attaract()
    {
        if (m_playerInputCtrl.IsAttract)
        {
            // 매 프레임마다 attractRange를 증가시킴
            if (attractRange < maxAttractRange)
            {
                attractRange += Time.deltaTime * attractAcceleration;
            }
            else
            {
                attractRange = maxAttractRange;
            }

            //TEST
            // Attract Encounter
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
            attractRange = Mathf.Max(0f, attractRange - Time.deltaTime * attractAcceleration);
        }
    }
}
