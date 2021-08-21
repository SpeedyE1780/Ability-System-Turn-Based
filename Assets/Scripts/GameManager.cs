using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerController Player1;

    //Singleton
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    private void OnEnable()
    {
        if (_instance == null)
        {
            _instance = this;
        }

        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        UIManager.Instance.UpdateAbilityTab(Player1.Abilities);
    }

    //Add selected ability to the player queue
    public void AbilitySelected(Ability ability)
    {
        if(Player1.AddAbility(ability))
        {
            UIManager.Instance.UpdateAbilityQueue(ability);
        }     
    }

    //Remove ability from the player queue
    public void RemoveAbility(int index)
    {
        Player1.RemoveAbility(index);
    }

    public void ShootAbilities()
    {
        Player1.StartCoroutine("ExecuteAbilities");
    }
}