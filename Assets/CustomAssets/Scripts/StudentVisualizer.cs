using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using NRKernal;

public class StudentVisualizer : MonoBehaviour
{
    public int StudentID;

    public DisplayMode Mode;

    public GameObject LookAt;

    public Material Topic_mat;
    public Material Cognitive_mat;
    public Material Metacognitive_mat;
    public Material Emotional_mat;
    public Material Names_mat;

    private long lastUpdated;
    private DisplayMode.Mode lastMode;

    private const int UPDATE_DELAY = 10;

    // Start is called before the first frame update
    void Start()
    {
        lastUpdated = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Face towards LookAt object.
        Vector3 target = LookAt.transform.position;
        target.y = transform.position.y;
        transform.rotation = Quaternion.LookRotation (target - transform.position);
        
        // Make sure to update when the mode is switched.
        DisplayMode.Mode currentMode = Mode.getMode();
        if(currentMode != lastMode) {
            lastUpdated = 0;
            lastMode = currentMode;
        }
        
        switch(currentMode)
        {
            case DisplayMode.Mode.NAME: 
                if(shouldUpdate()) {
                    setName(StudentID);
                }
                break;                    

            case DisplayMode.Mode.TOPIC: 
                if (shouldUpdate())
                {
                    ServerCommunication.Instance.getTopic(StudentID, setTopic, error);
                }
                break;
                
            case DisplayMode.Mode.PROGRESS_C: 
                if (shouldUpdate())
                {
                    ServerCommunication.Instance.getProgress(StudentID, setProgress, error);
                }
                break;

            case DisplayMode.Mode.PROGRESS_M: 
                if (shouldUpdate())
                {
                    ServerCommunication.Instance.getEloHistory(StudentID, setCognitive, error);
                }
                
                break;
                
            case DisplayMode.Mode.MOOD: 
                if (shouldUpdate())
                {
                    ServerCommunication.Instance.getEmotional(StudentID, setEmotional, error);
                }
                break;
        }
    }

    private bool shouldUpdate()
    {
        if (DateTime.Now.Second > (lastUpdated + UPDATE_DELAY)) 
        {
            lastUpdated = DateTime.Now.Second;
            return true;
        }

        return false;
    }

    private void setName(int studentID) {
        // TODO @Justin
        GetComponentsInChildren<MeshRenderer>()[0].material = Names_mat;
        GetComponentsInChildren<UnityEngine.UI.Text>()[0].text = studentID.ToString();
    }

    private void setTopic(string topic) {
        // TODO @Justin
        GetComponentsInChildren<MeshRenderer>()[0].material = Topic_mat;
        GetComponentsInChildren<UnityEngine.UI.Text>()[0].text = topic.ToString();
    }

    private void setProgress(float eloScale) {
        // TODO @Justin
        GetComponentsInChildren<MeshRenderer>()[0].material = Cognitive_mat;
        GetComponentsInChildren<UnityEngine.UI.Text>()[0].text = eloScale.ToString();
    }

    private void setCognitive(int[] eloPath) {
        // TODO @Justin
        GetComponentsInChildren<MeshRenderer>()[0].material = Metacognitive_mat;
        GetComponentsInChildren<UnityEngine.UI.Text>()[0].text = eloPath.ToString();
    }

    private void setEmotional(float correctRatio) {
        // TODO @Justin
        GetComponentsInChildren<MeshRenderer>()[0].material = Emotional_mat;
        GetComponentsInChildren<UnityEngine.UI.Text>()[0].text = correctRatio.ToString();
    }

    private void error(string str) 
    {
        Debug.Log(str);
    }
}
