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

  private bool isRunning;

  private void Awake() 
  {
    playerCamera = Camera.main.transform;
    characterController = GetComponent<CharacterController>();
    animator = GetComponent<Animator>();
  }
  private void Update()
  {
    float h = Input.GetAxis("Horizontal");
    float v = Input.GetAxis("Vertical");

    if(Input.GetButtonDown("Run"))
      isRunning = !isRunning;

    float moveAmount = Mathf.Clamp01(Mathf.Abs(h) + Mathf.Abs(v));

    var moveInput = new Vector3(h, 0, v);
    float camYRotation = playerCamera.rotation.eulerAngles.y;
    var moveDir = Quaternion.Euler(0, camYRotation, 0) * moveInput;

    float moveSpeed = isRunning ? runSpeed : walkSpeed;

    characterController.Move(moveDir * moveSpeed * Time.deltaTime);
    // transform.position += moveDir * moveSpeed * Time.deltaTime;

    if(moveAmount > 0)
      targetRotation = Quaternion.LookRotation(moveDir);

    transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, angularSpeed * Time.deltaTime);

    animator.SetFloat("moveAmount", moveAmount * moveSpeed / runSpeed, 0.2f, Time.deltaTime);
  }     
}