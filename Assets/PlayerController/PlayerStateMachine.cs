using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    protected PlayerState State;
    public void SetState(PlayerState _state)
    {
        State = _state;
        State.StateStart();
    }
}
