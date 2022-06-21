using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NotificationController : MonoBehaviour
{
    public GameObject Camera;

    private const long DISPLAY_DURATION = 5;
    private DateTime displayStart;

    void Start()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (DateTime.Now > (displayStart.AddSeconds(DISPLAY_DURATION))) 
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    void LateUpdate()
    {
        transform.position = Camera.transform.position;
        transform.rotation = Camera.transform.rotation;
    }

    public void ShowNotification(string message) 
    {   
        transform.GetChild(0).gameObject.SetActive(true);
        GetComponentsInChildren<UnityEngine.UI.Text>()[0].text = message;
        transform.GetChild(0).gameObject.SetActive(true);
        displayStart = DateTime.Now;
    }
}
