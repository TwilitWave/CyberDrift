using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraRigManager : MonoBehaviour
{

    public enum Camera { Wide, Back, Left, Right, Up, Down }
    public Camera CurrentCamera;
    private Camera PreviousCamera;

    public CinemachineVirtualCamera Wide;
    public CinemachineVirtualCamera Back;
    public CinemachineVirtualCamera Left;
    public CinemachineVirtualCamera Right;
    public CinemachineVirtualCamera Up;
    public CinemachineVirtualCamera Down;

    private HashSet<CinemachineVirtualCamera> Cameras;

    private void Build()
    {

        Cameras = new HashSet<CinemachineVirtualCamera>();

        foreach (CinemachineVirtualCamera camera in GetComponentsInChildren<CinemachineVirtualCamera>())
        {
            Cameras.Add(camera);
        }

        Debug.Log(this.name + ": Found " + Cameras.Count + " cameras");

        SwitchTo(CurrentCamera);

    }

    private static CameraRigManager _instance;

    public static CameraRigManager Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        Build();
    }

    private void Update()
    {
        if(PreviousCamera != CurrentCamera)
        {
            SwitchTo(CurrentCamera);
        }
    }


    public void SwitchTo(Camera camera)
    {
        if(camera == CurrentCamera) { return; }

        AllCamerasOff();
        ToggleCamera(camera, true);
        PreviousCamera = camera;
        CurrentCamera = camera;

        Debug.Log(this.name + " : Switching to camera: " + CurrentCamera.ToString());
    }

    public void AllCamerasOff()
    {
        foreach(CinemachineVirtualCamera cam in Cameras)
        {
            cam.enabled = false;
        }
    }

    private void ToggleCamera(Camera camera, bool flag)
    {
        if(camera == Camera.Wide)
        {
            Wide.enabled = flag;
        }
        else if(camera == Camera.Back)
        {
            Back.enabled = flag;
        }
        else if(camera == Camera.Left)
        {
            Left.enabled = flag;
        }
        else if(camera == Camera.Right)
        {
            Right.enabled = flag;
        }
        else if(camera == Camera.Up)
        {
            Up.enabled = flag;
        }
        else if(camera == Camera.Down)
        {
            Down.enabled = flag;
        }
    }


}
