using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 1f;

    // private -----

    private bool m_isGrounded;
    
    private Rigidbody m_rigidbody;

    void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {

        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");

        if(Mathf.Abs(m_rigidbody.linearVelocity.x) < 8f)
            m_rigidbody.AddRelativeForce(Vector3.right * xInput * Time.deltaTime * moveSpeed);

        if (Input.GetButton("Jump") == true && m_isGrounded == true)
        {
            m_rigidbody.AddRelativeForce(Vector3.up * 50f);
        }

        if(m_isGrounded != false)
        {
            m_rigidbody.AddRelativeForce(Vector3.down * 50f * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        m_isGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        m_isGrounded = false;
    }
}
