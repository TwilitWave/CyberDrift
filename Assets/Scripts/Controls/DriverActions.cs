using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class DriverActions : PlayerActionSet
{
    public PlayerAction Turn_Left;
    public PlayerAction Turn_Right;

    public PlayerAction Tilt_Forward;
    public PlayerAction Tilt_Backward;

    public PlayerAction Rotate_Left;
    public PlayerAction Rotate_Right;

    public PlayerAction Accelerate;
    public PlayerAction Reverse;

    public PlayerAction Drift;
    public PlayerAction Stall;


    public DriverActions()
    {
        Turn_Left = CreatePlayerAction("Turn Left");
        Turn_Right = CreatePlayerAction("Turn Right");

        Tilt_Backward = CreatePlayerAction("Ascend");
        Tilt_Forward = CreatePlayerAction("Descend");

        Rotate_Left = CreatePlayerAction("Rotate Left");
        Rotate_Right = CreatePlayerAction("Rotate Right");

        Accelerate = CreatePlayerAction("Accelerate");
        Reverse = CreatePlayerAction("Reverse");

        Drift = CreatePlayerAction("Drift");
        Stall = CreatePlayerAction("Stall");
        
    }
}
