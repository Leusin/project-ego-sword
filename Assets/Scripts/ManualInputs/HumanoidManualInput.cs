using ProjectEgoSword;
using UnityEngine;

public class HumanoidManualInput : ManualInput
{
    private HumanoidControl _humanoidController;

    private void Awake()
    {
        _humanoidController = GetComponent<HumanoidControl>();
    }

    void Update()
    {

        _humanoidController.move = _vartualInputController.MoveInput;

        var moveForce = _vartualInputController.MoveInput.x;
        if (moveForce > 0f)
        {
            _humanoidController.moveRight = true;
        }
        else if (moveForce < 0f)
        {
            _humanoidController.moveLeft = true;
        }
        else
        {
            _humanoidController.moveRight = false;
            _humanoidController.moveLeft = false;
        }

        _humanoidController.moveUp = _vartualInputController.MoveUpInput;
        _humanoidController.moveDown = _vartualInputController.MoveDownInput;
        _humanoidController.jump = _vartualInputController.JumpInput;
        _humanoidController.attack = _vartualInputController.AttackInput;
    }
}
