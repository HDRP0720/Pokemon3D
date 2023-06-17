using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  [SerializeField] private float walkSpeed = 3f;
  [SerializeField] private float runSpeed = 6f;
  [SerializeField] private float angularSpeed = 500f;

  private Quaternion targetRotation;
  private Transform playerCamera;
  private CharacterController characterController;
  private Animator animator;

  private bool bIsRunning;
  private bool bInAction;

  private void Awake() 
  {
    playerCamera = Camera.main.transform;
    characterController = GetComponent<CharacterController>();
    animator = GetComponent<Animator>();
  }
  private void Update()
  {
    if(bInAction) return;

    float h = Input.GetAxis("Horizontal");
    float v = Input.GetAxis("Vertical");

    if(Input.GetButtonDown("Run"))
      bIsRunning = !bIsRunning;

    if (Input.GetButtonDown("Jump"))    
      StartCoroutine(DoAction("Dive"));

    float moveAmount = Mathf.Clamp01(Mathf.Abs(h) + Mathf.Abs(v));

    var moveInput = new Vector3(h, 0, v);
    float camYRotation = playerCamera.rotation.eulerAngles.y;
    var moveDir = Quaternion.Euler(0, camYRotation, 0) * moveInput;

    float moveSpeed = bIsRunning ? runSpeed : walkSpeed;

    characterController.Move(moveDir * moveSpeed * Time.deltaTime);   

    if(moveAmount > 0)
      targetRotation = Quaternion.LookRotation(moveDir);

    transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, angularSpeed * Time.deltaTime);

    animator.SetFloat("moveAmount", moveAmount * moveSpeed / runSpeed, 0.2f, Time.deltaTime);
  }

  private IEnumerator DoAction(string animName)
  {
    bInAction = true;
    animator.CrossFade(animName, 0.2f);

    yield return null; // wait for 1 frame before get state info

    var animState = animator.GetNextAnimatorStateInfo(0);

    float timer = 0f;
    while(timer <= animState.length)
    {
      timer += Time.deltaTime;
      if(animator.IsInTransition(0) && timer > 0.4f) // Check when is in transition time between two animations
        break;

      yield return null; // hold every 1 frame to find when animation is in transition state
    }
    bInAction = false;
  }
}