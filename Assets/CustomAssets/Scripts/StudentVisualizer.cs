using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using NRKernal;

public class StudentVisualizer : MonoBehaviour
{
    public int StudentID;

    public DisplayMode Mode;

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
        DisplayMode.Mode currentMode = Mode.getMode();
        
        // Make sure to update when the mode is switched.
        if(currentMode != lastMode) {
            lastUpdated = 0;
            lastMode = currentMode;
        }
        
        switch(currentMode)
        {
            case DisplayMode.Mode.NAME: 
                GetComponentsInChildren<MeshRenderer>()[0].material = Names_mat;
                if(shouldUpdate()) {
                    setText(StudentID);
                }
                break;                    

            case DisplayMode.Mode.TOPIC: 
                GetComponentsInChildren<MeshRenderer>()[0].material = Topic_mat;
                if (shouldUpdate())
                {
                    ServerCommunication.Instance.getTopic(StudentID, setText, error);
                }
                break;
                
            case DisplayMode.Mode.PROGRESS_C: 
                GetComponentsInChildren<MeshRenderer>()[0].material = Cognitive_mat;
                if (shouldUpdate())
                {
                    ServerCommunication.Instance.getProgress(StudentID, setText, error);
                }
                break;

            case DisplayMode.Mode.PROGRESS_M: 
                GetComponentsInChildren<MeshRenderer>()[0].material = Metacognitive_mat;
                if (shouldUpdate())
                {
                    ServerCommunication.Instance.getEloHistory(StudentID, setText, error);
                }
                
                break;
                
            case DisplayMode.Mode.MOOD: 
                GetComponentsInChildren<MeshRenderer>()[0].material = Emotional_mat;
                if (shouldUpdate())
                {
                    ServerCommunication.Instance.getEmotional(StudentID, setText, error);
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
        GetComponentsInChildren<UnityEngine.UI.Text>()[0].text = studentID.ToString();
    }

    private void setTopic(string topic) {
        // TODO @Justin
        GetComponentsInChildren<UnityEngine.UI.Text>()[0].text = topic.ToString();
    }

    private void setProgress(float eloScale) {
        // TODO @Justin
        GetComponentsInChildren<UnityEngine.UI.Text>()[0].text = eloScale.ToString();
    }

    private void setCognitive(int[] eloPath) {
        // TODO @Justin
        GetComponentsInChildren<UnityEngine.UI.Text>()[0].text = eloPath.ToString();
    }

    private void setEmotional(float correctRatio) {
        // TODO @Justin
        GetComponentsInChildren<UnityEngine.UI.Text>()[0].text = correctRatio.ToString();
    }

    private void error(string str) 
    {
        Debug.Log(str);
    }
}
