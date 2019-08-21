using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(HoverControlScheme))]
[RequireComponent(typeof(HoverAudio))]

public class HoverController : MonoBehaviour
{
    public bool engine_on = false;

    public float thruster_speed = 90f;
    public float turn_speed = 5f;

    public float hover_force = 65f;
    public float hover_height = 3.5f;

    public float ascend_max = 60;
    private float ascend_min = 2;
    public float ascend_amount = 0.25f;

    public float drag_speed = 5f;
    public float rotate_speed = 3f;
    public float max_rotate = 35f;

    private Rigidbody car_rb;
    private HoverControlScheme controls;
    
    

    private void Awake()
    {
        car_rb = GetComponent<Rigidbody>();
        controls = GetComponent<HoverControlScheme>();
    }


    // Dont use this update for physics based stuff!
    void Update()
    {
        GetInput();
    }

    //...use this one
    private void FixedUpdate()
    {
        if (engine_on)
        {
            //hover off ground
            DoHover();

            //forward/backward
            ApplyThrust();

            //rotate left and right
            Rotate();

            //drag left and right 
            car_rb.AddRelativeForce(HoverControlScheme.InputStateToInt(controls.drag_input) * drag_speed, 0f, 0f);

            //ascend up and down
            ModifyHoverHeight(ascend_amount);
        }

        //turn left and right
        car_rb.AddRelativeTorque(0f, HoverControlScheme.InputStateToInt(controls.turn_input) * turn_speed, 0f);
    }

    private void Rotate()
    {
        float car_angle = car_rb.transform.rotation.eulerAngles.z;
        float calculated_angle = HoverControlScheme.InputStateToInt(controls.rotate_input) * rotate_speed;
        
        if (car_angle > max_rotate)
        {
            //Debug.Log("Correcting rotation!");
            //car_rb.angularVelocity = new Vector3(0, 0, 0);
            car_rb.transform.eulerAngles.Set(car_rb.transform.eulerAngles.x, car_rb.transform.eulerAngles.y, max_rotate);
        }
        else if(car_angle < -1 * max_rotate)
        {
            //Debug.Log("Correcting rotation!");
            //car_rb.angularVelocity = new Vector3(0, 0, 0);
            car_rb.transform.eulerAngles.Set(car_rb.transform.eulerAngles.x, car_rb.transform.eulerAngles.y, -1 * max_rotate);
        }
        else
        {
            car_rb.AddRelativeTorque(0f, 0f, calculated_angle);
        }
        
    }

    private void ApplyThrust()
    {
        car_rb.AddRelativeForce(0f, 0f, HoverControlScheme.InputStateToInt(controls.power_input) * thruster_speed);
    }

    private void DoHover()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;

        //Cast ray, if car distance to ground < hover height, apply some force
        if (Physics.Raycast(ray, out hit, hover_height))
        {
            //push away from ground, in proportion to distance to the ground 
            float proportional_height = (hover_height - hit.distance) / hover_height;

            //scale hover force based on prop. height 
            Vector3 applied_hover_force = Vector3.up * proportional_height * hover_force;

            //apply force to rigid body, ignore the mass of the car (for better hover)
            car_rb.AddForce(applied_hover_force, ForceMode.Acceleration);
        }
    }

    private void ModifyHoverHeight(float amount)
    {
        if (controls.ascend_input == HoverControlScheme.InputState.Positive)
        {
            if (hover_height + amount > ascend_max)
            {
                hover_height = ascend_max;
            }
            else
            {
                hover_height += amount;
            }
        }
        else if (controls.ascend_input == HoverControlScheme.InputState.Negative)
        {
            if (hover_height - amount < ascend_min)
            {
                hover_height = ascend_min;
            }
            else
            {
                hover_height -= amount;
            }
        }
    }

    private void ToggleEngine()
    {
        if (engine_on)
        {
            engine_on = false;
        }
        else
        {
            engine_on = true;
        }
    }

    private void GetInput()
    {
        if (engine_on)
        {
            if (Input.GetKey(controls.Forward))
            {
                controls.power_input = HoverControlScheme.InputState.Positive;
            }
            else if (Input.GetKey(controls.Backward))
            {
                controls.power_input = HoverControlScheme.InputState.Negative;
            }
            else
            {
                controls.power_input = HoverControlScheme.InputState.None;
            }

            if (Input.GetKey(controls.Left))
            {
                controls.turn_input = HoverControlScheme.InputState.Negative;
            }
            else if (Input.GetKey(controls.Right))
            {
                controls.turn_input = HoverControlScheme.InputState.Positive;
            }
            else
            {
                controls.turn_input = HoverControlScheme.InputState.None;
            }

            if (Input.GetKey(controls.Ascend))
            {
                controls.ascend_input = HoverControlScheme.InputState.Positive;
            }
            else if (Input.GetKey(controls.Descend))
            {
                controls.ascend_input = HoverControlScheme.InputState.Negative;
            }
            else
            {
                controls.ascend_input = HoverControlScheme.InputState.None;
            }

            if (Input.GetKey(controls.Rotate_Left))
            {
                controls.rotate_input = HoverControlScheme.InputState.Positive;
            }
            else if (Input.GetKey(controls.Rotate_Right))
            {
                controls.rotate_input = HoverControlScheme.InputState.Negative;
            }
            else
            {
                controls.rotate_input = HoverControlScheme.InputState.None;
            }

            if (Input.GetKey(controls.Drag_Left))
            {
                controls.drag_input = HoverControlScheme.InputState.Negative;
            }
            else if (Input.GetKey(controls.Drag_Right))
            {
                controls.drag_input = HoverControlScheme.InputState.Positive;
            }
            else
            {
                controls.drag_input = HoverControlScheme.InputState.None;
            }
        }


        if (Input.GetKeyDown(controls.Toggle_Engine))
        {
            ToggleEngine();
        }
    }

    
}
