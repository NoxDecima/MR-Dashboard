using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayMode : MonoBehaviour
{
    public enum Mode
    {
        NAME, TOPIC, PROGRESS_C, PROGRESS_M, MOOD
    }
    
    private Mode _displayMode;
    
    // Start is called before the first frame update
    void Start()
    {
        this._displayMode = Mode.NAME;
    }

    public void setMode(Mode mode)
    {
        this._displayMode = mode;
    }

    public Mode getMode()
    {
        return _displayMode;
    }
}