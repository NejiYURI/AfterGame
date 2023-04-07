using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainCharacterScript : PlayerStateMachine
{
    public LineRenderer AimLine;

    public float minDistance;
    public float MaxDistance;

    [SerializeField]
    private Vector2 MousePos;

    private void Start()
    {
        AimLine = this.GetComponent<LineRenderer>();
        SetState(new P_NormalState(this));
    }

    private void Update()
    {
        State.UpdateFunc();
    }

    public Vector2 GetMousePos()
    {
        return MousePos;
    }

    public void GetPos(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            MousePos = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());
        }
    }

    public void MouseClick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            State.MouseClick();
        }
    }

    public void Dash()
    {
        this.transform.LeanMove(AimLine.GetPosition(1), 0.05f);
    }



}
