using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  [SerializeField] private float moveSpeed = 5f;
  [SerializeField] private float angularSpeed = 500f;

  private Quaternion targetRotation;
  private Transform playerCamera;

  private void Awake() 
  {
    playerCamera = Camera.main.transform;
  }
  private void Update()
  {
    float h = Input.GetAxis("Horizontal");
    float v = Input.GetAxis("Vertical");

    float moveAmount = Mathf.Abs(h) + Mathf.Abs(v);

    var moveInput = new Vector3(h, 0, v);
    float camYRotation = playerCamera.rotation.eulerAngles.y;
    var moveDir = Quaternion.Euler(0, camYRotation, 0) * moveInput;

    transform.position += moveDir * moveSpeed * Time.deltaTime;

    if(moveAmount > 0)
      targetRotation = Quaternion.LookRotation(moveDir);

    transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, angularSpeed * Time.deltaTime);
  }     
}