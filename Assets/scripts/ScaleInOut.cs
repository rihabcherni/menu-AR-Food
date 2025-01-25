//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class ScaleInOut : MonoBehaviour
//{
//    public GameObject Object;

//    private bool _ZoomIn;
//    private bool _ZoomOut;
//    public float Scale = 0.01f;
//    void Update()
//    {
//        if (_ZoomIn)
//        {
//            Object.transform.localScale += new Vector3(Scale, Scale, Scale);
//        }

//        if (_ZoomOut)
//        {
//            Object.transform.localScale -= new Vector3(Scale, Scale, Scale);
//        }
//    }
//    public void OnPressZoomIn()
//    {
//        _ZoomIn = true;
//    }

//    public void OnReleaseZoomIn()
//    {
//        _ZoomIn = false;
//    }

//    //Make object scaled small
//    public void OnPressZoomOut()
//    {
//        _ZoomOut = true;
//    }

//    public void OnReleaseZoomOut()
//    {
//        _ZoomOut = false;
//    }
//}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleInOut : MonoBehaviour
{
    public GameObject Object; // The object to scale
    private bool _ZoomIn;
    private bool _ZoomOut;

    public float Scale = 0.01f; // Scaling increment/decrement value
    public Vector3 MinScale = new Vector3(0.1f, 0.1f, 0.1f); // Minimum scale limits
    public Vector3 MaxScale = new Vector3(3f, 3f, 3f); // Maximum scale limits

    void Update()
    {
        if (_ZoomIn && Object.transform.localScale.x < MaxScale.x &&
            Object.transform.localScale.y < MaxScale.y &&
            Object.transform.localScale.z < MaxScale.z)
        {
            Object.transform.localScale += new Vector3(Scale, Scale, Scale);
        }

        if (_ZoomOut && Object.transform.localScale.x > MinScale.x &&
            Object.transform.localScale.y > MinScale.y &&
            Object.transform.localScale.z > MinScale.z)
        {
            Object.transform.localScale -= new Vector3(Scale, Scale, Scale);
        }
    }

    public void OnPressZoomIn()
    {
        _ZoomIn = true;
    }

    public void OnReleaseZoomIn()
    {
        _ZoomIn = false;
    }

    public void OnPressZoomOut()
    {
        _ZoomOut = true;
    }

    public void OnReleaseZoomOut()
    {
        _ZoomOut = false;
    }
}
