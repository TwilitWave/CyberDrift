using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverController : MonoBehaviour
{

    public float speed = 90f;
    public float turn_speed = 5f;

    public float hover_force = 65f;
    public float hover_height = 3.5f;

    private float power_input;
    private float turn_input;
    private Rigidbody car_rb;

    private void Awake()
    {
        car_rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        power_input = Input.GetAxis("Vertical");
        turn_input = Input.GetAxis("Horizontal");
       // Debug.Log(power_input);
    }

    private void FixedUpdate()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;

        //Cast ray, if car distance to ground < hover height, apply some force
        if(Physics.Raycast(ray, out hit, hover_height))
        {
            //push away from ground, in proportion to distance to the ground 
            float proportional_height = (hover_height- hit.distance)/ hover_height;

            //scale hover force based on prop. height 
            Vector3 applied_hover_force = Vector3.up * proportional_height * hover_force;

            //apply force to rigid body, ignore the mass of the car (for better hover)
            car_rb.AddForce(applied_hover_force, ForceMode.Acceleration);
        }

        car_rb.AddRelativeForce(0f, 0f, power_input * speed);
        car_rb.AddRelativeTorque(0f, turn_input * turn_speed, 0f);

    }
}
