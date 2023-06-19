using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonParty : MonoBehaviour
{
  [SerializeField] private List<Pokemon> partyMembers;

  private void Awake() 
  {
    foreach (var pokemon in partyMembers)
    {
      pokemon.Init();
    }
  }

  // getter
  public List<Pokemon> GetPartyMembers => partyMembers;
}
