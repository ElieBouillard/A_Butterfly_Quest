using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrozenBlendTree : StateMachineBehaviour
{
    public bool freezeInTree;
    public string valueToWatch;
    public string CurrenParameterValue;
    public enum WatchedValueType {Float,Bool};
    public WatchedValueType watchedType;

    //OnStateEnter is called before OnStateEnter is called on any state inside this state machine
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (freezeInTree)
        {
            if (watchedType == WatchedValueType.Float)
            {
                animator.SetFloat(CurrenParameterValue, animator.GetFloat(valueToWatch));
            }
            if (watchedType == WatchedValueType.Bool)
            {
                animator.SetFloat(CurrenParameterValue, (animator.GetBool(valueToWatch) ? 1 : 0));
            }
        }
       
    }


    //OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(freezeInTree == false)
        {
            if (watchedType == WatchedValueType.Float)
            {
                animator.SetFloat(CurrenParameterValue, animator.GetFloat(valueToWatch));
            }
            if (watchedType == WatchedValueType.Bool)
            {
                animator.SetFloat(CurrenParameterValue, (animator.GetBool(valueToWatch) ? 1 : 0));
            }
        }
       
    }

    // OnStateExit is called before OnStateExit is called on any state inside this state machine
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called before OnStateMove is called on any state inside this state machine
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateIK is called before OnStateIK is called on any state inside this state machine
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMachineEnter is called when entering a state machine via its Entry Node
    //override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    //{
    //    
    //}

    // OnStateMachineExit is called when exiting a state machine via its Exit Node
    //override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    //{
    //    
    //}
}
