using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected MainCharacterScript characterScript;
    public PlayerState(MainCharacterScript _characterScript)
    {
        this.characterScript = _characterScript;
    }

    public virtual void StateStart()
    {

    }

    public virtual void StateEnd()
    {

    }

    public virtual void UpdateFunc()
    {

    }

    public virtual void MouseClick()
    {

    }
}
