using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverAudio : MonoBehaviour
{
    private AudioSource clip;
    private float pitch;
    private const float low_pitch = 0.1f;
    private const float high_pitch = 2.0f;
    private const float speed_to_revs = 0.01f;

    private Vector3 velocity;
    private Rigidbody car_rb;

    private void Awake()
    {
        car_rb = GetComponent<Rigidbody>();
        clip = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        clip.mute = false;

        velocity = car_rb.velocity; //get velocity of car
        float forward_speed = transform.InverseTransformDirection(car_rb.velocity).z;
        float engine_revs = Mathf.Abs(forward_speed) * speed_to_revs;
        clip.pitch = Mathf.Clamp(engine_revs, low_pitch, high_pitch);
    }
}
