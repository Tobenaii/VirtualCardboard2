using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;

using UnityEngine;

public class CardSetup : MonoBehaviour
{
    public Animator targetObject;
    public AnimatorController cont; 
    public AnimationClip targetAnim;

    [Range(0f, 1f)]
    public float normTime;

    private void OnValidate()
    {
        if (targetObject == null || targetAnim == null)
            return;

        AnimatorStateMachine asm = cont.layers[0].stateMachine;

        asm.defaultState.motion = targetAnim;

        targetObject.speed = 0f;
        targetObject.Play("default",0, normTime);
        //player._animator.Play(animationName, layer, normalizedTime);
        targetObject.Update(Time.deltaTime);
    }
}
