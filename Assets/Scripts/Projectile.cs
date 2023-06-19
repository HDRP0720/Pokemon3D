using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
  [SerializeField] private GameObject pokemonPrefab;
  [SerializeField] private LayerMask terrainLayer;

  private Transform cameraTransform;
  private Rigidbody rb;

  private void Awake() 
  {
    rb = GetComponent<Rigidbody>();
    cameraTransform = Camera.main.transform;
  }

  public void LaunchToTarget(Vector3 targetPos)
  {
    transform.parent = null;
    rb.isKinematic = false;
    rb.velocity = CalculateVelocity(targetPos);
  }
  private Vector3 CalculateVelocity(Vector3 targetPos)
  {
    var startPos = transform.position;

    float diffY = targetPos.y - startPos.y;
    Vector3 diffXZ = new Vector3(targetPos.x-startPos.x, 0, targetPos.z - startPos.z);

    float h = Mathf.Abs(diffY) + 0.5f;
    float g = Physics.gravity.y;

    var velocityY = Vector3.up * Mathf.Sqrt(-2 * g * h);
    var velocityXZ = diffXZ / (Mathf.Sqrt(-2 * h / g) + Mathf.Sqrt(2 * (diffY - h) / g));

    return velocityY + velocityXZ;
  }

  private void OnCollisionEnter(Collision other) 
  {
    if(other.gameObject.tag != "Player")
    {
      // Spawn Pokemon
      var rayOrigin = transform.position + Vector3.up;
      if(Physics.Raycast(rayOrigin, Vector3.down, out RaycastHit hit, 10f, terrainLayer))
      {
        var pokemonObj = Instantiate(pokemonPrefab, hit.point, Quaternion.identity);

        // face pokemon to player
        var dirToCam = (cameraTransform.position - hit.point).normalized;
        dirToCam.y = 0;
        pokemonObj.transform.forward = dirToCam;
      }

      Destroy(gameObject);
    }
  }
}
