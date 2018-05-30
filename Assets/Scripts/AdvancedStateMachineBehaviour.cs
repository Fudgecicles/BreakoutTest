using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// State machine that injects a name we can use to lookup state machines with 
/// </summary>
public class AdvancedStateMachineBehaviour : StateMachineBehaviour
{
    public string name;

    protected AnimatorStateInfo stateInfo;
    public AnimatorStateInfo StateInfo
    {
        get { return stateInfo; }
    }

    // Use this for initialization

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        this.stateInfo = stateInfo;
    }
}

public static class Utilities
{
    public static T GetBehaviour<T>(this Animator animator, AnimatorStateInfo stateInfo) where T : AdvancedStateMachineBehaviour
    {
        T[] behaviors = animator.GetBehaviours<T>();
        for(int k=0; k < behaviors.Length; k++)
        {
            if (behaviors[k].StateInfo.fullPathHash == stateInfo.fullPathHash)
                return behaviors[k];
        }
        return null;
    }

    public static T GetBehaviour<T>(this Animator animator, string stateName) where T : AdvancedStateMachineBehaviour
    {
        T[] behaviors = animator.GetBehaviours<T>();
        for (int k = 0; k < behaviors.Length; k++)
        {
            if (behaviors[k].name == stateName)
                return behaviors[k];
        }
        return null;
    }
}