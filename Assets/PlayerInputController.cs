using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-100)]
public class PlayerInputController : MonoBehaviour
{
    public static PlayerInputController Instance
    {
        get { return s_instance; }
    }

    private static PlayerInputController s_instance;

    // -----

    public void OnAttract(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {

            case InputActionPhase.Started:
                {
                    m_isAttract = true;
                }
                break;

            case InputActionPhase.Canceled:
                {
                    m_isAttract = false;
                }
                break;
        }
    }

    public bool IsAttract 
    { 
        get 
        { 
            return m_isAttract; 
        } 
    }

    public Vector2 MoveInput
    {
        get
        {
            return m_movement;
        }
    }

    private bool m_isAttract;
    private Vector2 m_movement;
    private bool m_isAttack;
    private bool m_isTnteract;

    public UnityEvent Crounch;

    // -----

    public void OnMove(InputAction.CallbackContext context)
    {
        m_movement = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
        }
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
        }
    }

    public void OnCrounch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Crounch.Invoke();
        }
    }

    // -----

    void Awake()
    {
        // 싱글턴 패턴 초기화
        if (s_instance == null)
        {
            s_instance = this;
        }
        else if (s_instance != this)
        {
            throw new UnityException("There cannot be more than one PlayerInput script.  The instances are " + s_instance.name + " and " + name + ".");
        }
    }
}
