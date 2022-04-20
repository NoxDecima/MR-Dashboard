using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changemat : MonoBehaviour
{
    public Material Topic;
	public Material Cognitive;
	public Material Metacognitive;
	public Material Emotional;
	public Material Names;
    public GameObject Object;
	
	// Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("t"))
        {
            Object.GetComponent<MeshRenderer>().material = Topic;
        }
		if (Input.GetKeyDown("c"))
        {
            Object.GetComponent<MeshRenderer>().material = Cognitive;
        }
		if (Input.GetKeyDown("m"))
        {
            Object.GetComponent<MeshRenderer>().material = Metacognitive;
        }
		if (Input.GetKeyDown("e"))
        {
            Object.GetComponent<MeshRenderer>().material = Emotional;
        }
		if (Input.GetKeyDown("n"))
        {
            Object.GetComponent<MeshRenderer>().material = Names;
        }
    }
}
