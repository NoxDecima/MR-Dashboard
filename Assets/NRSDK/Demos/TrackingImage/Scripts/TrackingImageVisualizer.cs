// Done by Roel

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NRKernal;
using System;

public class TrackingImageVisualizer : MonoBehaviour
{
    public NRTrackableImage image;
    public GameObject cube;

    void Update()
    {
        Console.WriteLine("UPDATE! function called");
        if(image == null)
        {
            cube.SetActive(false);
            return;
        }
        Console.WriteLine("Image detected!");
        var center = image.GetCenterPose();
        transform.position = center.position;
        transform.rotation = center.rotation;
        cube.SetActive(true); 
    }
}