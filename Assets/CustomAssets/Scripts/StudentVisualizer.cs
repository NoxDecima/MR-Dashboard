using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NRKernal 
{
    public class StudentVisualizer : MonoBehaviour
    {
        public int StudentID;

        public DisplayMode Mode;

        public Material Topic_mat;
        public Material Cognitive_mat;
        public Material Metacognitive_mat;
        public Material Emotional_mat;
        public Material Names_mat;

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            DisplayMode.Mode currentMode = Mode.getMode();
            
            switch(currentMode)
            {
                case DisplayMode.Mode.NAME: 
                    GetComponentsInChildren<MeshRenderer>()[0].material = Names_mat;
                    GetComponentsInChildren<UnityEngine.UI.Text>()[0].text = "Names";
                    break;                    

                case DisplayMode.Mode.TOPIC: 
                    GetComponentsInChildren<MeshRenderer>()[0].material = Topic_mat;
                    GetComponentsInChildren<UnityEngine.UI.Text>()[0].text = "Topic";
                    break;
                    
                case DisplayMode.Mode.PROGRESS_C: 
                    GetComponentsInChildren<MeshRenderer>()[0].material = Cognitive_mat;
                    GetComponentsInChildren<UnityEngine.UI.Text>()[0].text = "Cognitive";
                    break;

                case DisplayMode.Mode.PROGRESS_M: 
                    GetComponentsInChildren<MeshRenderer>()[0].material = Metacognitive_mat;
                    GetComponentsInChildren<UnityEngine.UI.Text>()[0].text = "Metacognitive";
                    
                    break;
                    
                case DisplayMode.Mode.MOOD: 
                    GetComponentsInChildren<MeshRenderer>()[0].material = Emotional_mat;
                    GetComponentsInChildren<UnityEngine.UI.Text>()[0].text = "Emotional";
                    break;
            }
        }
    }
}
