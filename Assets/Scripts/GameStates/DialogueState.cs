using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Utils.StateMachine;

public class DialogueState : State<GameController>
{
  public static DialogueState instance { get; private set; }

  private void Awake()
  {
    instance = this;
  }

  public override void Enter(GameController owner)
  {
    Debug.Log("Entered DialogueState State");
  }

  public override void Execute()
  {
    Debug.Log("Executing DialogueState State");
  }

  public override void Exit()
  {
    Debug.Log("Exiting DialogueState State");
  }
}
