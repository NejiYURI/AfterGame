using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
namespace NormalGame
{

    public class MainCharacterScript : PlayerStateMachine
    {
        public LineRenderer AimLine;

        public GameObject SlashLine;

        public float minDistance;
        public float MaxDistance;

        [SerializeField]
        private Vector2 MousePos;

        private void Start()
        {
            AimLine = this.GetComponent<LineRenderer>();
            SetState(new P_NormalState(this));
            if (GameEventManager.instance)
            {
                GameEventManager.instance.ActionStart.AddListener(ActionStart);
            }
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

        void ActionStart(bool IsStart)
        {
            State.StateEnd();
            if (IsStart) SetState(new P_ActionState(this));
            else SetState(new P_NormalState(this));
        }

        public void SpawnSlashLine(Vector2 start, Vector2 end)
        {
            GameObject obj = Instantiate(SlashLine, start, Quaternion.identity);
            if (obj.GetComponent<SlahLineScript>()) obj.GetComponent<SlahLineScript>().SetLineRender(start, end);
        }

    }
}
