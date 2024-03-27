using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public BaseState activeState;

    public void Initialise() 
    {
        ChangeState(new PatrolState());
    }

    // Update is called once per frame
    void Update()
    {
        if(activeState != null)
        {
            activeState.Perform();
        }
    }

    public void ChangeState(BaseState newState)
    {
        // check activeState != null
        if (activeState != null) {
            // run cleanup on active state
            activeState.Exit();
        }
        // change to new state
        activeState = newState;

        // fail-safe null check to make sure new state wasnt null
        if (activeState != null)
        {
            //setup new state
            activeState.stateMachine = this;
            activeState.enemy = GetComponent<BaseEnemy>();
            activeState.Enter();
        }
    }
}
