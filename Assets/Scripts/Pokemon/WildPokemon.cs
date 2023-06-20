using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildPokemon : MonoBehaviour
{
  [SerializeField] private Pokemon pokemon;

  private void Start() 
  {
    pokemon.Init();
  }
}
