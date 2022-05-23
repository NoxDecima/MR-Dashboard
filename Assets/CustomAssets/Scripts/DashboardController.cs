using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashboardController : MonoBehaviour
{
    public DisplayMode Mode;

    public Material Topic_mat;
    public Material Cognitive_mat;
    public Material Metacognitive_mat;
    public Material Emotional_mat;
    public Material Names_mat;
    public GameObject Object;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DisplayMode.Mode currentMode = Mode.getMode();
        
        if (currentMode == DisplayMode.Mode.TOPIC)
        {
            Object.GetComponent<MeshRenderer>().material = Topic_mat;
        };
        
        if (currentMode == DisplayMode.Mode.PROGRESS_C)
        {
            Object.GetComponent<MeshRenderer>().material = Cognitive_mat;
        };
        
        if (currentMode == DisplayMode.Mode.PROGRESS_M)
        {
            Object.GetComponent<MeshRenderer>().material = Metacognitive_mat;
        };
        
        if (currentMode == DisplayMode.Mode.MOOD)
        {
            Object.GetComponent<MeshRenderer>().material = Emotional_mat;
        };
        
        if (currentMode == DisplayMode.Mode.NAME)
        {
            Object.GetComponent<MeshRenderer>().material = Names_mat;
        };
    }
}
