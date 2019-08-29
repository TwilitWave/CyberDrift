using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public class CopAnimSleep : MonoBehaviour
{
    public float Sleep_Seconds = 4f;

    IEnumerator LockObjectForTime(float time)
    {
        
        this.gameObject.GetComponentInChildren<Spline>().enabled = false;
       yield return new WaitForSeconds(time);
       this.gameObject.GetComponentInChildren<Spline>().enabled = true;
        this.gameObject.GetComponent<CarSplineTweener>().Reset();

    }

    private void Awake()
    {
            StartCoroutine(LockObjectForTime(Sleep_Seconds));
    }
}
