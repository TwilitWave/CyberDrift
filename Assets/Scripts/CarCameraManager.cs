using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarCameraManager : MonoBehaviour
{

    private Rigidbody rb;
    public float x_axis_threshold;
    public float y_axis_threshold; //at what point we decide to switch cameras from the standard back camera

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        CameraRigManager.Camera Camera = CameraRigManager.Camera.Back;

        //set camera based on the velocity of the car
        float x = rb.velocity.x; //left and right turning
        float y = rb.velocity.y; //up and down turning
        float z = rb.velocity.z; //this will typically be the largest value is the car is moving


        if (Mathf.Abs(x) > x_axis_threshold)
        {
            if (x < 0)
            {
                Camera = CameraRigManager.Camera.Right;
            }
            else
            {
                Camera = CameraRigManager.Camera.Left;
            }

        }
        else
        {
            if (Mathf.Abs(z) > Mathf.Abs(x) && Mathf.Abs(z) > Mathf.Abs(y))
            {
                Camera = CameraRigManager.Camera.Back;
            }

        }

        CameraRigManager.Instance.SwitchTo(Camera);


    }
}
