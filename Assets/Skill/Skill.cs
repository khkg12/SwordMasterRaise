using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]   
public abstract class Skill : ScriptableObject
{
    public GameObject skillObj;
    public Sprite sprite;
    public string skillName;
    public int requiredLevel;    
    public int skillNum;
    public string skillText;
    public float coolTime;
    public float rate;
    public abstract void Use(Character character);    
}

public abstract class RotateSkill : Skill { }
public abstract class FixSkill : Skill { }






