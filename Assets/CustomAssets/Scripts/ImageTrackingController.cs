namespace NRKernal
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary> Controller for TrackingImage example. </summary>
    [HelpURL("https://developer.nreal.ai/develop/unity/image-tracking")]
    public class ImageTrackingController : MonoBehaviour
    {
        /// <summary> A prefab for visualizing an TrackingImage. </summary>
        public StudentVisualizer StudentVisualizerPrefab;

        public DisplayMode DisplayMode;

        public Canvas Canvas_UI;
        public UnityEngine.UI.Image GraphWindow;

        /// <summary> The visualizers. </summary>
        private Dictionary<int, StudentVisualizer> m_Visualizers
            = new Dictionary<int, StudentVisualizer>();

        /// <summary> The temporary tracking images. </summary>
        private List<NRTrackableImage> m_TempTrackingImages = new List<NRTrackableImage>();

        /// <summary> Updates this object. </summary>
        public void Update()
        {
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
                    visualizer.transform.position = image.GetCenterPose().position;
                    visualizer.name = "Student_" + image.GetDataBaseIndex();
                    visualizer.StudentID = GetStudentID(image.GetDataBaseIndex());
                    visualizer.Mode = DisplayMode;

                    Canvas_UI = visualizer.GetComponentsInChildren<Canvas>()[0];
                    Canvas_UI.transform.localPosition = new Vector3(-0.35f, 0.02f, -0.02f);
                    Canvas_UI.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
                    var rectTransform = Canvas_UI.GetComponent<RectTransform>();
                    rectTransform.sizeDelta = new Vector2(10, 10);

                    GraphWindow = visualizer.GetComponentsInChildren<Image>()[0];
                    GraphWindow.transform.localPosition = new Vector3(35f, 0f, 0f);

                    m_Visualizers.Add(image.GetDataBaseIndex(), visualizer);
                }
            }
        }


        private int GetStudentID(int ImageIndex) 
        {
            // TODO finish implementation!!!
            return 383751;
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
