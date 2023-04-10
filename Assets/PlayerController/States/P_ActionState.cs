using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NormalGame
{
    public class P_ActionState : PlayerState
    {
        public P_ActionState(MainCharacterScript characterScript) : base(characterScript)
        {
            characterScript.AimLine.enabled = true;
        }

        public override void UpdateFunc()
        {
            if (!characterScript.AimLine) return;
            Vector2 pos = characterScript.transform.position;
            Vector2 MousePos = characterScript.GetMousePos();
            Vector2 dir = (MousePos - pos).normalized;
            characterScript.AimLine.SetPosition(0, pos);

            Vector2 minPos = pos + dir * characterScript.minDistance;
            Vector2 MaxPos = pos + dir * characterScript.MaxDistance;
            Vector2 rslt = MousePos;
            if (Vector2.Distance(MousePos, pos) > characterScript.MaxDistance) rslt = MaxPos;
            if (Vector2.Distance(MousePos, pos) < characterScript.minDistance) rslt = minPos;
            characterScript.AimLine.SetPosition(1, rslt);
        }

        public override void MouseClick()
        {
            characterScript.SetState(new P_DashState(characterScript, characterScript.AimLine.GetPosition(0), characterScript.AimLine.GetPosition(1)));
        }

    }
}

