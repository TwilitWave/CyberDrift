using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarCameraManager : MonoBehaviour
{

    public bool LookBackMode = false;


    private HoverControlScheme controls;



    private void Awake()
    {
        controls = GetComponent<HoverControlScheme>();
    }

    // Update is called once per frame
    void Update()
    {
        DoCamera();

        //if(Input.GetKeyDown(KeyCode.M))
        //{
        //    CameraRigManager.Instance.SwitchTo(CameraRigManager.Camera.LookBehind);
        //}
        //if (Input.GetKeyDown(KeyCode.N))
        //{
        //    CameraRigManager.Instance.SwitchTo(CameraRigManager.Camera.Back);
        //}
        //else
        //{
        //    CameraRigManager.Instance.SwitchTo(CameraRigManager.Camera.Back);
        //}

    }

    void DoCamera()
    {
      
        if (Input.GetKeyDown(KeyCode.V))
        {
           
            LookBackMode = true;
            CameraRigManager.Instance.SwitchTo(CameraRigManager.Camera.LookBehind);
        }
        if (LookBackMode)
        {
            if (Input.GetKeyUp(KeyCode.V))
            {
                LookBackMode = false;
             
                CameraRigManager.Instance.SwitchTo(CameraRigManager.Camera.Back);
            }
        }
        

        //if (!LookBackMode)
        //{
        //    if (controls.turn_input == HoverControlScheme.InputState.Positive)
        //    {
        //        CameraRigManager.Instance.SwitchTo(CameraRigManager.Camera.Wide);
        //    }
        //    else if (controls.turn_input == HoverControlScheme.InputState.Negative)
        //    {
        //        CameraRigManager.Instance.SwitchTo(CameraRigManager.Camera.Right);
        //    }
        //    else if (controls.ascend_input == HoverControlScheme.InputState.Positive)
        //    {
        //        CameraRigManager.Instance.SwitchTo(CameraRigManager.Camera.Down);
        //    }
        //    else if (controls.ascend_input == HoverControlScheme.InputState.Negative)
        //    {
        //        CameraRigManager.Instance.SwitchTo(CameraRigManager.Camera.Up);
        //    }
        //    else
        //    {
        //        CameraRigManager.Instance.SwitchTo(CameraRigManager.Camera.Back);
        //    }
        //}
    }
}
