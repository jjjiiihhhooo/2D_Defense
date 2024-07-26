using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickManager : MonoBehaviour
{
    [SerializeField] private FixedJoystick joy;
    [SerializeField] private float joyVertical;
    [SerializeField] private float joyHorizontal;
    [SerializeField] private float angle;

    public float Angle { get => angle; }

    public void Init()
    {

    }

    private void Update()
    {
        InputJoystick();
        
    }

    private void InputJoystick()
    {
        if (joy == null) return;

        if(joy.Vertical != 0 || joy.Horizontal != 0)
        {
            joyVertical = joy.Vertical;
            joyHorizontal = joy.Horizontal;

            angle = Mathf.Atan2(joyVertical, joyHorizontal) * Mathf.Rad2Deg;
        }
        else
        {
            angle = 0f;
        }
    }

    public void GetJoystick(FixedJoystick joystick)
    {
        joy = joystick;
    }
}   
