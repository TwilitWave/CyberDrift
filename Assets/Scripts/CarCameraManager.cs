using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarCameraManager : MonoBehaviour
{

    private HoverControlScheme controls;

    private void Awake()
    {
        controls = GetComponent<HoverControlScheme>();
    }

    // Update is called once per frame
    void Update()
    {
        //selectGeneralCamera();

        if(Input.GetKeyDown(KeyCode.M))
        {
            CameraRigManager.Instance.SwitchTo(CameraRigManager.Camera.LookBehind);
        }
        //else
        //{
        //    CameraRigManager.Instance.SwitchTo(CameraRigManager.Camera.Back);
        //}
       
    }

    void selectGeneralCamera()
    {

        if (controls.turn_input == HoverControlScheme.InputState.Positive)
        {
            CameraRigManager.Instance.SwitchTo(CameraRigManager.Camera.Wide);
        }
        else if (controls.turn_input == HoverControlScheme.InputState.Negative)
        {
            CameraRigManager.Instance.SwitchTo(CameraRigManager.Camera.Right);
        }
        else if (controls.ascend_input == HoverControlScheme.InputState.Positive)
        {
            CameraRigManager.Instance.SwitchTo(CameraRigManager.Camera.Down);
        }
        else if (controls.ascend_input == HoverControlScheme.InputState.Negative)
        {
            CameraRigManager.Instance.SwitchTo(CameraRigManager.Camera.Up);
        }
        else
        {
            CameraRigManager.Instance.SwitchTo(CameraRigManager.Camera.Back);
        }
    }
}
