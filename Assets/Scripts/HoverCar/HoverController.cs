﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(HoverAudio))]
[RequireComponent(typeof(HoverControlScheme))]

public class HoverController : MonoBehaviour
{
    public bool engine_on = false;

    public float thruster_speed = 50f;
    public float reverse_speed = 25f;
    public float turn_speed = 5f;

    public float hover_force = 65f;
    public float hover_height = 110f;

    public float ascend_max = 550;
    public float ascend_min = 100;
    public float ascend_amount = 1f;

    public float drag_speed = 40f;

    public float rotate_speed = 3f;
    public float max_rotate = 35f;
    private float current_rotate = 0;
    private bool rotating = false;

    public float ascend_angle = 35f;
    public float descend_angle = -35;
    private float current_tilt = 0;
    public float tilt_speed = 0.05f;

    public enum Speed { Slow, Normal, Fast };
    public float Slow_modifier = 0.5f;
    public float Normal_modifier = 1.0f;
    public float Fast_modifier = 2.0f;
    public Speed Current_Speed = Speed.Normal;

    public float Angvel_Break;

    private Rigidbody car_rb;
    private DriverActions driverActions;
    private HoverControlScheme controls;



    private void DoControlBindings()
    {
        driverActions = new DriverActions();

        driverActions.Turn_Left.AddDefaultBinding(Key.A);
        driverActions.Turn_Left.AddDefaultBinding(InputControlType.LeftStickLeft);

        driverActions.Turn_Right.AddDefaultBinding(Key.D);
        driverActions.Turn_Right.AddDefaultBinding(InputControlType.LeftStickRight);

        driverActions.Tilt_Forward.AddDefaultBinding(Key.DownArrow);
        driverActions.Tilt_Forward.AddDefaultBinding(InputControlType.LeftTrigger);

        driverActions.Tilt_Backward.AddDefaultBinding(Key.UpArrow);
        driverActions.Tilt_Backward.AddDefaultBinding(InputControlType.RightTrigger);

        driverActions.Rotate_Left.AddDefaultBinding(Key.Q);
        driverActions.Rotate_Left.AddDefaultBinding(InputControlType.LeftBumper);

        driverActions.Rotate_Right.AddDefaultBinding(Key.E);
        driverActions.Rotate_Right.AddDefaultBinding(InputControlType.RightBumper);

        driverActions.Accelerate.AddDefaultBinding(Key.W);
        driverActions.Accelerate.AddDefaultBinding(InputControlType.Action1);

        driverActions.Reverse.AddDefaultBinding(Key.S);
        driverActions.Reverse.AddDefaultBinding(InputControlType.Action4);

        driverActions.Drift.AddDefaultBinding(Key.Space);
        driverActions.Drift.AddDefaultBinding(InputControlType.Action2);

        driverActions.Stall.AddDefaultBinding(Key.F);
        driverActions.Stall.AddDefaultBinding(InputControlType.Action3);
    }

    private void Awake()
    {
        car_rb = GetComponent<Rigidbody>();
        controls = GetComponent<HoverControlScheme>();
        DoControlBindings();
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

            CheckSpeedChange();

            //hover off ground
            DoHover();

            //forward/backward
            ApplyThrust();

            //ascend up and down
            ModifyHoverHeight(ascend_amount);
            //set angle
            DoTiltAngle(ascend_amount);

            Rotate();

            car_rb.transform.eulerAngles = new Vector3(current_tilt, car_rb.transform.rotation.eulerAngles.y, current_rotate);
        }

        //turn left and right
        car_rb.AddRelativeTorque(0f, HoverControlScheme.InputStateToInt(controls.turn_input) * turn_speed, 0f);
       
        DoAngularVelocityBreak();
    }

    void DoAngularVelocityBreak()
    {
       // Debug.Log("Rotation: " + car_rb.angularVelocity);
        Vector3 aV = car_rb.angularVelocity;
        Vector3 newAngVel = aV;
        if (aV.y != 0)
        {
            float cal = Angvel_Break * Time.deltaTime;
            float AngularBreak_val = aV.y < 0 ? cal : -cal;
            //if (aV.y > 0)
            //{
            //    newAngVel.y = newAngVel.y - Angvel_Break * Time.deltaTime;
            //}
            //else
            //{
            //    newAngVel.y = newAngVel.y + Angvel_Break * Time.deltaTime;
            //}
            newAngVel.y = newAngVel.y + AngularBreak_val;
        }
        car_rb.angularVelocity = newAngVel;
    }

    private void DoTiltAngle(float ascend_amount)
    {
        if (driverActions.Tilt_Forward.IsPressed)
        {
           
            current_tilt = Mathf.Lerp(current_tilt, ascend_angle, tilt_speed * Time.deltaTime);
         
        }
        else if (driverActions.Tilt_Backward.IsPressed)
        {
            current_tilt = Mathf.Lerp(current_tilt, descend_angle, tilt_speed * Time.deltaTime);
         
        }
        else
        {
            current_tilt = Mathf.Lerp(current_tilt, 0, tilt_speed * Time.deltaTime);
            
        }

    }

    private void Rotate()
    {

        //return back to original position
        if (driverActions.Rotate_Left)
        {

        }
        else if (driverActions.Rotate_Right)
        {

        }
        else if (rotating == false)
        {
            current_rotate = Mathf.Lerp(current_rotate, 0, rotate_speed * Time.deltaTime);
        }

        
        
    }

    private void ApplyThrust()
    {
        if (driverActions.Accelerate.IsPressed)
        {
            car_rb.AddRelativeForce(0f, 0f, GetSpeedModifier() * HoverControlScheme.InputStateToInt(controls.power_input) * thruster_speed);
        }
        else if (driverActions.Reverse.IsPressed)
        {
            car_rb.AddRelativeForce(0f, 0f, GetSpeedModifier() * HoverControlScheme.InputStateToInt(controls.power_input) * reverse_speed);
        }
        
    }

    private float GetSpeedModifier()
    {
        if (Current_Speed == Speed.Fast)
        {
            return Fast_modifier;
        }
        else if (Current_Speed == Speed.Normal)
        {
            return Normal_modifier;
        }
        else if(Current_Speed == Speed.Slow)
        {
            return Slow_modifier;
        }
        else
        {
            return 0;
        }
    }

    public void CheckSpeedChange()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)){
            Current_Speed = Speed.Slow;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)){
            Current_Speed = Speed.Normal;

        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Current_Speed = Speed.Fast;
        }
    }

    private void Deprecated_DoHover()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;

        //Cast ray, if car distance to ground < hover height, apply some force
        if (Physics.Raycast(ray, out hit, hover_height))
        {
            //push away from ground, in proportion to distance to the ground 
            float proportional_height = (hover_height - hit.distance) / hover_height;

            //scale hover force based on prop. height 
            Vector3 applied_hover_force = Vector3.up  * hover_force; //* proportional_height

            //apply force to rigid body, ignore the mass of the car (for better hover)
            car_rb.AddForce(applied_hover_force, ForceMode.Acceleration);
        }
    }

    private void DoHover()
    {
        car_rb.MovePosition(new Vector3(car_rb.transform.position.x, hover_height, car_rb.transform.position.z));
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
            car_rb.useGravity = true;
        }
        else
        {
            engine_on = true;
            car_rb.useGravity = false;
        }
    }

    private void GetInput()
    {
        if (engine_on)
        {
            if (driverActions.Accelerate.IsPressed)
            {
                controls.power_input = HoverControlScheme.InputState.Positive;
            }
            else if (driverActions.Reverse.IsPressed)
            {
                controls.power_input = HoverControlScheme.InputState.Negative;
            }
            else
            {
                controls.power_input = HoverControlScheme.InputState.None;
            }

            if (driverActions.Turn_Left.IsPressed)
            {
                controls.turn_input = HoverControlScheme.InputState.Negative;
            }
            else if (driverActions.Turn_Right.IsPressed)
            {
                controls.turn_input = HoverControlScheme.InputState.Positive;
            }
            else
            {
                controls.turn_input = HoverControlScheme.InputState.None;
            }

            if (driverActions.Tilt_Forward)
            {
                controls.ascend_input = HoverControlScheme.InputState.Negative;
               
            }
            else if (driverActions.Tilt_Backward.IsPressed)
            {
                controls.ascend_input = HoverControlScheme.InputState.Positive;
            }
            else
            {
                controls.ascend_input = HoverControlScheme.InputState.None;
            }
        }


        if (driverActions.Stall.WasPressed)
        {
            Debug.Log("Button pressed " + driverActions.Stall.Name);
            ToggleEngine();
        }
    }

    
}
