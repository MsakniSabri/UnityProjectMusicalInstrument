using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    public void Awake()
    {
        current = this;
    }

    public event Action<string> onInstrumentTrigger;
    public void InstrumentTrigger(string note)
    {
        if (onInstrumentTrigger != null)
        {
            onInstrumentTrigger(note);
        }
    }
}
