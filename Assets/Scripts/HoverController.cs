using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverController : MonoBehaviour
{

    public float speed = 90f;
    public float turn_speed = 5f;

    public float hover_force = 65f;
    public float hover_height = 3.5f;

    public float ascend_max = 60;
    private float ascend_min = 2;
    public float ascend_amount = 0.25f;

    public float drag_speed = 5f;

    public bool engine_on = false;

    private InputState power_input;
    private InputState turn_input;
    private InputState ascend_input;
    private InputState rotate_input;
    private InputState drag_input;
    
    private Rigidbody car_rb;

    public enum InputState { None, Negative, Positive };

    public KeyCode Forward = KeyCode.W;
    public KeyCode Backward = KeyCode.S;
    public KeyCode Left = KeyCode.A;
    public KeyCode Right = KeyCode.D;

    public KeyCode Rotate_Left = KeyCode.Q;
    public KeyCode Rotate_Right = KeyCode.E;

    public float rotate_speed = 3f;
    public float max_rotate = 35f;

    public KeyCode Ascend = KeyCode.UpArrow;
    public KeyCode Descend = KeyCode.DownArrow;
    public KeyCode Drag_Left = KeyCode.LeftArrow;
    public KeyCode Drag_Right = KeyCode.RightArrow;

    public KeyCode Toggle_Engine = KeyCode.F;

    private void Awake()
    {
        car_rb = GetComponent<Rigidbody>();
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
            car_rb.AddRelativeForce(0f, 0f, InputStateToInt(power_input) * speed);

            //rotate left and right
            car_rb.AddRelativeTorque(0f, 0f, InputStateToInt(rotate_input) * rotate_speed);

            //drag left and right 
            car_rb.AddRelativeForce(InputStateToInt(drag_input) * drag_speed, 0f, 0f);

            //ascend up and down
            ModifyHoverHeight(ascend_amount);

        }

        //turn left and right
        car_rb.AddRelativeTorque(0f, InputStateToInt(turn_input) * turn_speed, 0f);

        
        
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
        if (ascend_input == InputState.Positive)
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
        else if (ascend_input == InputState.Negative)
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
            if (Input.GetKey(Forward))
            {
                power_input = InputState.Positive;
            }
            else if (Input.GetKey(Backward))
            {
                power_input = InputState.Negative;
            }
            else
            {
                power_input = InputState.None;
            }

            if (Input.GetKey(Left))
            {
                turn_input = InputState.Negative;
            }
            else if (Input.GetKey(Right))
            {
                turn_input = InputState.Positive;
            }
            else
            {
                turn_input = InputState.None;
            }

            if (Input.GetKey(Ascend))
            {
                ascend_input = InputState.Positive;
            }
            else if (Input.GetKey(Descend))
            {
                ascend_input = InputState.Negative;
            }
            else
            {
                ascend_input = InputState.None;
            }

            if (Input.GetKey(Rotate_Left))
            {
                rotate_input = InputState.Negative;
            }
            else if (Input.GetKey(Rotate_Right))
            {
                rotate_input = InputState.Positive;
            }
            else
            {
                rotate_input = InputState.None;
            }

            if (Input.GetKey(Drag_Left))
            {
                drag_input = InputState.Negative;
            }
            else if (Input.GetKey(Drag_Right))
            {
                drag_input = InputState.Positive;
            }
            else
            {
                drag_input = InputState.None;
            }
        }


        if (Input.GetKeyDown(Toggle_Engine))
        {
            ToggleEngine();
        }
    }

    public int InputStateToInt(InputState state)
    {
        if (state == InputState.None)
        {
            return 0;
        }
        else if (state == InputState.Negative)
        {
            return -1;
        }
        else if (state == InputState.Positive)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
}
