using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  [Header("# Movement Parameter")]
  [SerializeField] private float walkSpeed = 3f;
  [SerializeField] private float runSpeed = 6f;
  [SerializeField] private float angularSpeed = 500f;

  [Header("# Cinemachine Camera")]
  [Tooltip("virtual camera for transition while aim animation")]
  [SerializeField] private GameObject aimCamera;
  [SerializeField] private Transform aimTarget;

  [SerializeField] private Projectile pokeballPrefab;
  [SerializeField] private float throwRange = 15f;
  
  private Transform playerCameraTransform;
  private Camera playerCamera;
  private Quaternion targetRotation;
  private CharacterController characterController;
  private Animator animator;

  private bool bIsRunning;
  private bool bIsAiming;
  private bool bInAction;

  private float aimAngle = 0f;

  private void Awake() 
  {
    playerCameraTransform = Camera.main.transform;
    playerCamera = Camera.main;
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
    {
      StartCoroutine(DoAction("Dive"));
    }      
    else if(Input.GetButtonDown("Throw"))
    {
      Aim();
    }
    else if(Input.GetButtonUp("Throw") && bIsAiming)
    {
      bIsAiming = false; 
      animator.SetBool("isAiming", false);

      StartCoroutine(DoAction("Throw", ()=>aimCamera.SetActive(false)));
    }

    float moveAmount = Mathf.Clamp01(Mathf.Abs(h) + Mathf.Abs(v));

    var moveInput = new Vector3(h, 0, v);
    float camYRotation = playerCameraTransform.rotation.eulerAngles.y;
    var moveDir = Quaternion.Euler(0, camYRotation, 0) * moveInput;

    float moveSpeed = bIsRunning ? runSpeed : walkSpeed;

    characterController.Move(moveDir * moveSpeed * Time.deltaTime);   

    if(bIsAiming) // rotate camera during aim mode
    {
      // Horizontal Aiming - Rotate the player
      float rotationY = transform.rotation.eulerAngles.y;
      rotationY += Input.GetAxis("Camera X");
      targetRotation = Quaternion.Euler(0, rotationY, 0);

      // Vertical Aiming - Rotate the Aim Target
      aimAngle += Input.GetAxis("Camera Y");
      aimAngle = Mathf.Clamp(aimAngle, -40f, 40f);
      aimTarget.localRotation = Quaternion.Euler(aimAngle, 0, 0);
    }
    else
    {
      if (moveAmount > 0)
        targetRotation = Quaternion.LookRotation(moveDir);
    }    

    transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, angularSpeed * Time.deltaTime);

    animator.SetFloat("moveAmount", moveAmount * moveSpeed / runSpeed, 0.2f, Time.deltaTime);
  }  

  private IEnumerator DoAction(string animName, Action onOver=null)
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

    onOver?.Invoke();
  } 

  Projectile pokeballObj;
  private void Aim()
  {
    bIsAiming = true;
    animator.SetBool("isAiming", true);

    aimCamera.SetActive(true);

    pokeballObj = Instantiate(pokeballPrefab, animator.GetBoneTransform(HumanBodyBones.RightHand));
  }

  // Animation event function
  Vector3 targetPos;
  private void ThrowPokeball()
  {
    Vector3 rayOrigin = playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f));
    if (Physics.Raycast(rayOrigin, playerCamera.transform.forward, out RaycastHit hit, throwRange))
    {
      targetPos = hit.point;
    }
    else
    {
      targetPos = rayOrigin + playerCamera.transform.forward * throwRange;
    }

    pokeballObj.LaunchToTarget(targetPos);
  }
}