using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * 
 * SCRIPT PLUS UTILISE CAR IMPLEMENTE DANS DOGBEHAVIOR
 * 
 * 
 * 
//Input Keys
public enum InputKeyboard{
    arrows =0, 
    wasd = 1
}
public class MoveWithKeyboardBehavior : AgentBehaviour
{
    public InputKeyboard inputKeyboard; 

    public override Steering GetSteering()
    {
        Debug.Log("ciuciuciu");
        float vertical;
        float horizontal;
        if(inputKeyboard == InputKeyboard.arrows)
        {
            vertical = Input.GetAxis("Vertical");
            horizontal = Input.GetAxis("Horizontal");
        } else
        {
            vertical = Input.GetAxis("VerButt");
            horizontal = Input.GetAxis("HorButt");
        }

        Steering steering = new Steering();
        //moving the cellulo 
        steering.linear = new Vector3(horizontal, 0, vertical) * agent.maxAccel;
        steering.linear = this.transform.parent.TransformDirection(Vector3.ClampMagnitude(steering.
        linear, agent.maxAccel));
        return steering;
    }

}
**/
