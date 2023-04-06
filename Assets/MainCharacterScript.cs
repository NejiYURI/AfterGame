using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainCharacterScript : MonoBehaviour
{
    private LineRenderer AimLine;

    public float minDistance;
    public float MaxDistance;

    [SerializeField]
    private Vector2 MousePos;

    private void Start()
    {
        AimLine = this.GetComponent<LineRenderer>();
    }

    private void Update()
    {
        SetLine();
    }

    void SetLine()
    {
        if (!AimLine) return;
        Vector2 pos = this.transform.position;
        Vector2 dir = (MousePos - pos).normalized;
        AimLine.SetPosition(0, pos);

        Vector2 minPos = pos + dir * minDistance;
        Vector2 MaxPos = pos + dir * MaxDistance;
        Vector2 rslt = MousePos;
        if (Vector2.Distance(MousePos, pos) > MaxDistance) rslt = MaxPos;
        if (Vector2.Distance(MousePos, pos) < minDistance) rslt = minPos;
        AimLine.SetPosition(1, rslt);
    }

    public void GetPos(InputAction.CallbackContext context)
    {
        if (context.performed)
            MousePos = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());
    }

    public void MouseClick(InputAction.CallbackContext context)
    {
        if (context.performed)
            this.transform.LeanMove(AimLine.GetPosition(1), 0.05f);
    }
}
