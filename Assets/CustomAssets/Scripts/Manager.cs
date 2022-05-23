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

        // Handles user input
        void Update()
        {
            if(NRInput.GetButtonDown(ControllerButton.APP))
            {
                displayMode.cycleMode();
            }
        }
        
        void addNewStudentAsset(Vector3 position, int studentID)
        {
            GameObject newObject = Instantiate(bluePrint);
            newObject.name = "Student_" + studentID;
            newObject.transform.position = position;
            studentAssets.Add(newObject);
        }
    }
}