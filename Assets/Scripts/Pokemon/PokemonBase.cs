using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Pokemon", menuName = "Pokemon/Make New Pokemon", order = 0)]
public class PokemonBase : ScriptableObject 
{
  [Header("# Pokemon Info")]
  [SerializeField] private string pokemonName;

  [SerializeField][TextArea]
  private string description;

  [SerializeField] private GameObject pokemonPrefab;
  [SerializeField] private EPokemonType pokemonType1;
  [SerializeField] private EPokemonType pokemonType2;

  // Base Stats
  [Header("# Pokemon Status")]
  [SerializeField] private int maxHp;
  [SerializeField] private int attack;
  [SerializeField] private int defense;
  [SerializeField] private int spAttack;
  [SerializeField] private int spDefense;
  [SerializeField] private int speed;

  [Header("# Pokemon Skills")]
  [SerializeField] private List<LearnableSkill> learnableSkills;

  // getters
  public string GetPokemonName => pokemonName;
  public string GetDescription => description;
  public GameObject GetPokemonObj => pokemonPrefab;

  public int GetMaxHp => maxHp;
  public int GetAttack => attack;
  public int GetDefense => defense;
  public int GetSpAttack => spAttack;
  public int GetSpDefense => spDefense;
  public int GetSpeed => speed;

  public List<LearnableSkill> GetLearnableSkills => learnableSkills;
}

public enum EPokemonType
{
  None, Normal, Fire, Water, Electric, Grass, Ice, Fighting, Poison, Ground, Flying, Psychic, Bug, Rock, Ghost, Dragon 
}

[System.Serializable]
public class LearnableSkill
{
  [SerializeField] private SkillBase skillBase;
  [SerializeField] private int level;

  // getters
  public SkillBase GetSkillBase => skillBase;
  public int GetLevel => level;
}

