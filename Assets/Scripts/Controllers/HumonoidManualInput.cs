using ProjectEgoSword;
using UnityEngine;

public class HumonoidManualInput : ManualInput
{
    private HumanoidController _humanoidController;

    private void Awake()
    {
        _humanoidController = GetComponent<HumanoidController>();
    }

    void Update()
    {
        _humanoidController.move = _vartualInputController.MoveInput;
        _humanoidController.jump = _vartualInputController.JumpInput;
        _humanoidController.attack = _vartualInputController.AttackInput;
    }
}
