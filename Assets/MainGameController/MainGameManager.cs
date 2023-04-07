using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameManager : GameStateMachine
{
    private void Start()
    {
        SetState(new StartState(this));
    }
}
