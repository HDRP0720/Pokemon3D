using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Pokemon/Make New Skill", order = 0)]
public class SkillBase : ScriptableObject 
{
  [SerializeField] 
  private string skillName;

  [SerializeField][TextArea]
  private string description;

  [SerializeField]
  private EPokemonType skillType;

  [SerializeField] private int power;
  [SerializeField] private int accuracy;
  [SerializeField] private int maxUsageCount;

  // getters
  public string GetSkillName => skillName;
  public string GetDescription => description;
  public EPokemonType GetSkillType=> skillType;
  public int GetPower => power;
  public int GetAccuracy => accuracy;
  public int GetMaxUsageCount => maxUsageCount;
}

