namespace NRKernal
{
    using System.Collections.Generic;
    using UnityEngine;
    
    /// <summary> Controller for TrackingImage example. </summary>
    [HelpURL("https://developer.nreal.ai/develop/unity/image-tracking")]
    public class ImageTrackingController : MonoBehaviour
    {
        /// <summary> A prefab for visualizing an TrackingImage. </summary>
        public StudentVisualizer StudentVisualizerPrefab;

        public DisplayMode DisplayMode;

        public GameObject Camera;

        public string classID;

        /// <summary> The visualizers. </summary>
        private Dictionary<int, StudentVisualizer> m_Visualizers
            = new Dictionary<int, StudentVisualizer>();

        /// <summary> The temporary tracking images. </summary>
        private List<NRTrackableImage> m_TempTrackingImages = new List<NRTrackableImage>();


        private Student[] students;

        public void Start()
        {
            ServerCommunication.Instance.startSimulation(
                classID,
                studentArray => {students = studentArray;},
                message => {Debug.Log(message);}
            );
        }


        /// <summary> Updates this object. </summary>
        public void Update()
        {
            if(students != null) {


#if !UNITY_EDITOR
                // Check that motion tracking is tracking.
                if (NRFrame.SessionStatus != SessionState.Running)
                {
                    return;
                }
    #endif
                // Get updated augmented images for this frame.
                NRFrame.GetTrackables<NRTrackableImage>(m_TempTrackingImages, NRTrackableQueryFilter.All);

                // Create visualizers and anchors for updated augmented images that are tracking and do not previously
                // have a visualizer. Remove visualizers for stopped images.
                foreach (var image in m_TempTrackingImages)
                {
                    StudentVisualizer visualizer = null;
                    m_Visualizers.TryGetValue(image.GetDataBaseIndex(), out visualizer);
                    if (image.GetTrackingState() != TrackingState.Stopped && visualizer == null)
                    {
                        NRDebugger.Info("Create new TrackingImageVisualizer!");
                        // Create an anchor to ensure that NRSDK keeps tracking this augmented image.

                        visualizer = (StudentVisualizer)Instantiate(StudentVisualizerPrefab);
                        // TODO move the visualizer up in the y dimension so its above the tracker.
                        visualizer.transform.position = image.GetCenterPose().position;
                        visualizer.name = "Student_" + image.GetDataBaseIndex();
                        visualizer.StudentID = GetStudentID(image.GetDataBaseIndex());
                        visualizer.Mode = DisplayMode;
                        visualizer.LookAt = Camera;
                        m_Visualizers.Add(image.GetDataBaseIndex(), visualizer);
                    }
                }
            }
        }


        private int GetStudentID(int ImageIndex) 
        {
            // TODO finish implementation!!!
            if (ImageIndex < students.Length)
            {
                return int.Parse(students[ImageIndex].id);
            }
            else 
            {
                Debug.Log("Scanned more images than there are students.");
                return 0;
            }
        }

        /// <summary> Enables the image tracking. </summary>
        public void EnableImageTracking()
        {
            var config = NRSessionManager.Instance.NRSessionBehaviour.SessionConfig;
            config.ImageTrackingMode = TrackableImageFindingMode.ENABLE;
            NRSessionManager.Instance.SetConfiguration(config);
        }

        /// <summary> Disables the image tracking. </summary>
        public void DisableImageTracking()
        {
            var config = NRSessionManager.Instance.NRSessionBehaviour.SessionConfig;
            config.ImageTrackingMode = TrackableImageFindingMode.DISABLE;
            NRSessionManager.Instance.SetConfiguration(config);
        }
    }
}
