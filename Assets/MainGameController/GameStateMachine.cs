using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateMachine : MonoBehaviour
{
    protected GameState State;

    public void SetState(GameState _state)
    {
        State = _state;
        State.StateStart();
    }
}
