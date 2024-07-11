using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Window : MonoBehaviour
{

    public event Action IsOpen;
    public event Action IsClosed;
    public virtual void Open()
    {
        Cursor.visible = true;
        IsOpen?.Invoke();
        Time.timeScale = 0.0f;
    }
    public virtual void Close()
    {
        IsClosed?.Invoke();
        Time.timeScale = 1.0f;
        Cursor.visible = false;
    }
}
