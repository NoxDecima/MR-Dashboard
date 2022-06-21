using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    private DateTime lastUpdated;
    private DisplayMode.Mode lastMode;
    private Graph graph;

    private const int UPDATE_DELAY = 10;

    // Start is called before the first frame update
    void Start()
    {
        graph = GetComponentsInChildren<Graph>()[0];

        lastUpdated = DateTime.Now.AddSeconds(-UPDATE_DELAY);
    }

    // Update is called once per frame
    void Update()
    {
        // Face towards LookAt object.
        Vector3 target = LookAt.transform.position;
        target.y = transform.position.y;
        transform.rotation = Quaternion.LookRotation (transform.position - target);
        
        // Make sure to update when the mode is switched.
        DisplayMode.Mode currentMode = Mode.getMode();
        if(currentMode != lastMode) {
            lastUpdated = DateTime.Now.AddSeconds(-UPDATE_DELAY);
            lastMode = currentMode;
            graph.ResetGraph();
            GetComponentsInChildren<UnityEngine.UI.Text>()[0].text = "...";
        }
        
        switch(currentMode)
        {
            case DisplayMode.Mode.NAME: 
                if(shouldUpdate()) {
                    GetComponentsInChildren<MeshRenderer>()[0].material = Names_mat;
                    setName(StudentID);
                }
                break;                    

            case DisplayMode.Mode.TOPIC: 
                if (shouldUpdate())
                {
                    GetComponentsInChildren<MeshRenderer>()[0].material = Topic_mat;
                    ServerCommunication.Instance.getTopic(StudentID, setTopic, error);
                }
                break;
                
            case DisplayMode.Mode.PROGRESS_C: 
                if (shouldUpdate())
                {
                    GetComponentsInChildren<MeshRenderer>()[0].material = Cognitive_mat;
                    ServerCommunication.Instance.getProgress(StudentID, setProgress, error);
                }
                break;

            case DisplayMode.Mode.PROGRESS_M: 
                if (shouldUpdate())
                {
                    GetComponentsInChildren<MeshRenderer>()[0].material = Metacognitive_mat;
                    ServerCommunication.Instance.getEloHistory(StudentID, setCognitive, error);
                }
                
                break;
                
            case DisplayMode.Mode.MOOD: 
                if (shouldUpdate())
                {
                    GetComponentsInChildren<MeshRenderer>()[0].material = Emotional_mat;
                    ServerCommunication.Instance.getEmotional(StudentID, setEmotional, error);
                }
                break;
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
        GetComponentsInChildren<UnityEngine.UI.Text>()[0].text = " ";
        graph.ResetGraph();
        graph.ShowGraph(eloPath.ToList<int>());
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
