using NRKernal;
using UnityEngine;

/// <summary> A trackable found test. </summary>
public class TrackableFoundTest : MonoBehaviour
{
    /// <summary> The observer. </summary>
    public TrackableObserver Observer;
    /// <summary> The object. </summary>
    public GameObject Obj;
    //
    // Roel: This object is what will be in front of the detected image (The visualizer)
    //

    /// <summary> Starts this object. </summary>
    void Start()
    {
#if !UNITY_EDITOR
        Destroy(GameObject.Find("EmulatorRoom"));
#endif
        Obj.SetActive(false);
        Observer.FoundEvent += Found;
        Observer.LostEvent += Lost;
    }

    /// <summary> Founds. </summary>
    /// <param name="pos"> The position.</param>
    /// <param name="qua"> The qua.</param>
    private void Found(Vector3 pos, Quaternion qua)
    {
        Obj.transform.position = new Vector3(pos.x, pos.y + 1.0f, pos.z); // this is the position where the object will be placed
        Obj.transform.rotation = qua;
        Obj.SetActive(true);
        // possibly: return position to the script that Justin made
    }

    /// <summary> Losts this object. </summary>
    private void Lost()
    {
        Obj.SetActive(false);
    }
}
