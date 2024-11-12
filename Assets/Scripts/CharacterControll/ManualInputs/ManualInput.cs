using ProjectEgoSword;
using UnityEngine;

public class ManualInput : MonoBehaviour
{
    
    protected VirtualInputController _vartualInputController;

    private void Start()
    {
        _vartualInputController = VirtualInputController.Instance;
    }
}
