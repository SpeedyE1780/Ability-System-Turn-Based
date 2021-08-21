using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[CreateAssetMenu(menuName ="Abilities/New Ability")]
public class Ability : ScriptableObject
{
    public string AbilityName;
    public AnimationClip CastAnimation;
    public GameObject AbilityShard;
    public int AbilityDamage;
    public int StaminaCost;
    public Sprite AbilitySprite;

    public string Description()
    {
        StringBuilder description = new StringBuilder();
        
        description.Append($"Name: {AbilityName}\n");
        description.Append($"Damage: {AbilityDamage}\n");
        description.Append($"Stamina Cost: {StaminaCost}\n");

        return description.ToString();
    }
}