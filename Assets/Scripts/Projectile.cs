using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
  // [SerializeField] private GameObject pokemonPrefab;
  [SerializeField] private LayerMask terrainLayer;
  [SerializeField] private GameObject spawnEffectPrefab;

  public Pokemon PokemonToSpawn { get; set; }

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

    // float h = Mathf.Abs(diffY) + 0.5f;
    float h = diffY + 0.1f * diffXZ.magnitude;
    h = Mathf.Clamp(h, 0f, diffY + 2f);
    float g = Physics.gravity.y;

    var velocityY = Vector3.up * Mathf.Sqrt(-2 * g * h);
    var velocityXZ = diffXZ / (Mathf.Sqrt(-2 * h / g) + Mathf.Sqrt(2 * (diffY - h) / g));

    return velocityY + velocityXZ;
  }

  // if pokeball doesn't hit wild pokemon
  private void OnCollisionEnter(Collision other) 
  {
    if(other.gameObject.tag != "Player")
    {
      rb.isKinematic = true;
      StartCoroutine(SpawnPokemon(other.collider));
    }      
  }

  // if pokeball hit wild pokemon
  private void OnTriggerEnter(Collider other) 
  {
    if (other.gameObject.tag != "Player")
    {
      rb.isKinematic = true;
      StartCoroutine(SpawnPokemon(other));
    }     
  }

  private IEnumerator SpawnPokemon(Collider other)
  {
    Vector3 spawnPos = transform.position;
    Vector3 dirToPlayerPokemon = Vector3.zero;
    var wildPokemon = other.GetComponent<WildPokemon>();

    if(wildPokemon != null)
    {
      // calculate direction from camera to wild pokemon
      var dirToCam = (cameraTransform.position - wildPokemon.transform.position).normalized;
      dirToCam.y = 0;

      dirToPlayerPokemon = Quaternion.Euler(0, -30, 0) * dirToCam; // to spawn pokemon right side from player for the angle

      spawnPos = wildPokemon.transform.position + dirToPlayerPokemon * 3f;
      wildPokemon.transform.forward = dirToPlayerPokemon;
    }

    var rayOrigin = spawnPos + Vector3.up;
    if (Physics.Raycast(rayOrigin, Vector3.down, out RaycastHit hit, 10f, terrainLayer))
    {
      // calculate direction from camera to spawn point
      var dirToCam = (cameraTransform.position - hit.point).normalized;
      dirToCam.y = 0;

      // face spawn effect to player
      var spawnEffect = Instantiate(spawnEffectPrefab, hit.point + Vector3.up * 0.5f, Quaternion.identity);
      spawnEffect.transform.forward = dirToCam;

      yield return new WaitForSeconds(0.2f);

      // face pokemon to player    
      var pokemonObj = Instantiate(PokemonToSpawn.Base.GetPokemonObj, hit.point, Quaternion.identity);
      pokemonObj.transform.forward = (wildPokemon != null) ? -dirToPlayerPokemon : dirToCam;
    }

    Destroy(gameObject);
  }
}
