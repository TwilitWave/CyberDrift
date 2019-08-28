using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public class CivilianSplineTweener : MonoBehaviour
{

    private Spline spline;
    private SplineFollower follower;
    [Range(0,0.03f)]
    public float speed = 0.001f;
    public bool RandomSpeed = true;
    public bool loop = true;
    private bool playedOnce = false;

    private void Awake()
    {
        spline = GetComponentInChildren<Spline>();
        follower = spline.followers[0];

        if (RandomSpeed)
        {
            speed = Random.Range(0.001f, 0.01f);
        }
    }

    public void Reset()
    {
        playedOnce = false;
        follower.percentage = 0;
    }

    private void Update()
    {
        if(!playedOnce)
        {
            float percentage = follower.percentage;

            if (percentage + speed > 1.0f)
            {
                if (loop)
                {
                    percentage = 0;
                }
                else
                {
                    playedOnce = true;
                }

            }
            else
            {
                percentage += speed;
            }

            follower.percentage = percentage;
        }

        
    }
}
