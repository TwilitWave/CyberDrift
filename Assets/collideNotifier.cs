using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collideNotifier : MonoBehaviour
{

    public float ID;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if(other.gameObject.name.Contains("CarChassis"))
        {
            CameraCollidersManager.Instance.handleCameraSwitching(this);
        }
    }
}
