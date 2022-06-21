using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NRKernal
{
    public class DisplayMode : MonoBehaviour
    {
        public enum Mode
        {
            NAME, TOPIC, PROGRESS_C, PROGRESS_M, MOOD
        }
        
        private Mode _displayMode;
        
        // Start is called before the first frame update
        void Start()
        {
            this._displayMode = Mode.NAME;
        }

        void Update()
        {
            if(NRInput.GetButtonDown(ControllerButton.APP))
            {
                cycleMode();
            }
        }

        public void setMode(Mode mode)
        {
            this._displayMode = mode;
        }

        public Mode getMode()
        {
            return _displayMode;
        }

        public void cycleMode() 
            {
                switch(_displayMode)
                {
                    case Mode.NAME: 
                        setMode(Mode.TOPIC);
                        break;                    

                    case Mode.TOPIC: 
                        setMode(Mode.PROGRESS_C);
                        break;
                    
                    case Mode.PROGRESS_C: 
                        setMode(Mode.PROGRESS_M);
                        break;

                    case Mode.PROGRESS_M: 
                        setMode(Mode.MOOD);
                        break;
                    
                    case Mode.MOOD: 
                        setMode(Mode.NAME);
                        break;
                }
            }
    }
}