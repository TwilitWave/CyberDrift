using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollidersManager : MonoBehaviour
{
    public BoxCollider aferDolly;


    public static CameraCollidersManager Instance;


    private void Awake()
    {
        Instance = this;
    }



    public void handleCameraSwitching(collideNotifier cn)
    {
        if(cn.ID==1)
        {
            CameraRigManager.Instance.SwitchTo(CameraRigManager.Camera.Back);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
