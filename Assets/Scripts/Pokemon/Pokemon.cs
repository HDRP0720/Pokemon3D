using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Pokemon
{
  [SerializeField] private PokemonBase _base;
  [SerializeField] private int level;

  public int CurrentHp { get; set; }
  public List<Skill> Skills { get; set; }

  // (baseStat * level / 100) + 5
  public int MaxHp => Mathf.FloorToInt(_base.GetMaxHp * level / 100) + 10;
  public int Attack => Mathf.FloorToInt(_base.GetAttack * level / 100) + 5;
  public int Defense => Mathf.FloorToInt(_base.GetDefense * level / 100) + 5;
  public int SpAttack => Mathf.FloorToInt(_base.GetSpAttack * level / 100) + 5;
  public int SpDefense => Mathf.FloorToInt(_base.GetSpDefense * level / 100) + 5;
  public int Speed => Mathf.FloorToInt(_base.GetSpeed * level / 100) + 5;

  // getters
  public PokemonBase Base => _base;
  public int Level => level;

  public void Init()
  {
    CurrentHp = MaxHp;

    // Generate skills based on the level
    Skills = new List<Skill>();
    foreach (var skill in _base.GetLearnableSkills.OrderByDescending(skill => skill.GetLevel))
    {
      if(skill.GetLevel <= level)
        Skills.Add(new Skill(skill.GetSkillBase));
      
      if(Skills.Count == 4) break;
    }
  }
}