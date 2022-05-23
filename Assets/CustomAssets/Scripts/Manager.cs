using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NRKernal{
    public class Manager : MonoBehaviour
    {
        public GameObject bluePrint;
        public DisplayMode displayMode;

        private List<GameObject> studentAssets = new List<GameObject>();
        
        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            if(NRInput.GetButtonDown(ControllerButton.APP))
            {
                cycleMode();
            }
        }
        
        void addNewStudentAsset(Vector3 position, int studentID)
        {
            GameObject newObject = Instantiate(bluePrint);
            newObject.name = "Student_" + studentID;
            newObject.transform.position = position;
            studentAssets.Add(newObject);
        }

        void cycleMode() 
        {
            switch(displayMode.getMode())
            {
                case DisplayMode.Mode.NAME: 
                    switchMode(DisplayMode.Mode.TOPIC);
                    break;
                
                case DisplayMode.Mode.TOPIC: 
                    switchMode(DisplayMode.Mode.PROGRESS_C);
                    break;
                
                case DisplayMode.Mode.PROGRESS_C: 
                    switchMode(DisplayMode.Mode.PROGRESS_M);
                    break;

                case DisplayMode.Mode.PROGRESS_M:
                    switchMode(DisplayMode.Mode.MOOD);
                    break;

                case DisplayMode.Mode.MOOD: 
                    switchMode(DisplayMode.Mode.NAME);
                    break;
            }
        }

        void switchMode(DisplayMode.Mode mode)
        {
            displayMode.setMode(mode);
            Debug.Log(mode);
        }
    }
}