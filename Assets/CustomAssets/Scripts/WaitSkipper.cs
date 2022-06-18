using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class WaitSkipper : MonoBehaviour
{
    public int maxWaitSeconds;
    public string classID;


    private DateTime lastUpdated;

    private const int UPDATE_DELAY = 10;

    // Start is called before the first frame update
    void Start()
    {
        lastUpdated = DateTime.Now.AddSeconds(-UPDATE_DELAY);
    }

    // Update is called once per frame
    void Update()
    {
        if(shouldUpdate()) 
        {
            ServerCommunication.Instance.timeToNextUpdate(classID,
                seconds => {
                    if(seconds > maxWaitSeconds) 
                    {
                        ServerCommunication.Instance.skipToNextQuestion(classID,
                            error => Debug.Log(error)
                            );
                    }
                },
                error => Debug.Log(error)
            );
        }
    }

    private bool shouldUpdate()
    {
        if (DateTime.Now > (lastUpdated.AddSeconds(UPDATE_DELAY))) 
        {
            lastUpdated = DateTime.Now;
            return true;
        }

        return false;
    }
}
