using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill
{
  public SkillBase Base {get; set;}
  public int CurrentUsageCount {get; set;}

  public Skill (SkillBase _base)
  {
    Base = _base;
    CurrentUsageCount = _base.GetMaxUsageCount;
  }
}
