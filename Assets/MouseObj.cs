using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseObj : MonoBehaviour
{
    
    void Update()
    {
        this.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
