using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverControlScheme : MonoBehaviour
{
    public enum InputState { None, Negative, Positive };

    public InputState power_input = InputState.None;
    public InputState turn_input = InputState.None;
    public InputState ascend_input = InputState.None;
    public InputState rotate_input = InputState.None;
    public InputState drag_input = InputState.None;

    public static int InputStateToInt(InputState state)
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
