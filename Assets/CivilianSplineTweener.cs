using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public class CivilianSplineTweener : MonoBehaviour
{

    private Spline spline;
    private SplineFollower follower;
    [Range(0,0.01f)]
    public float speed = 0.001f;
    public bool RandomSpeed = true;

    private void Awake()
    {
        spline = GetComponentInChildren<Spline>();
        follower = spline.followers[0];

        if (RandomSpeed)
        {
            speed = Random.Range(0.001f, 0.01f);
        }
    }

    private void Update()
    {
        float percentage = follower.percentage;

        if (percentage + speed > 1.0f)
        {
            percentage = 0;
        }
        else
        {
            percentage += speed;
        }

        follower.percentage = percentage;
    }
}
