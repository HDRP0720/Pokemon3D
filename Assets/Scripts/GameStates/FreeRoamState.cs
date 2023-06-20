using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Utils.StateMachine;

public class FreeRoamState : State<GameController>
{
  public static FreeRoamState instance { get; private set; }

  private GameController gc;

  private void Awake() 
  {
    instance = this;
  }

  public override void Enter(GameController owner)
  {
    gc = owner;
    Debug.Log("Entered FreeRoam State");
  }

  public override void Execute()
  {
    Debug.Log("Executing FreeRoam State");

    if(Input.GetKeyDown(KeyCode.Return))
      gc.StateMachine.ChangeState(DialogueState.instance);
  }

  public override void Exit()
  {
    Debug.Log("Exiting FreeRoam State");
  }
}
