using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Transform AbilityTab;
    public Transform AbilityQueue;
    public Text StaminaText;
    public GameObject AbilityButton;

    //Singleton
    private static UIManager _instance;
    public static UIManager Instance { get { return _instance; } }

    private void OnEnable()
    {
        if(_instance == null)
        {
            _instance = this;
        }

        else
        {
            Destroy(this);
        }

        EventManager.updateStamina += UpdateStaminaTxt;
        EventManager.updateQueue += EmptyQueue;
    }

    private void OnDisable()
    {
        EventManager.updateStamina -= UpdateStaminaTxt;
        EventManager.updateQueue -= EmptyQueue;
    }

    //Add the current player's ability to the tab
    public void UpdateAbilityTab(List<Ability> Abilities)
    {
        foreach(Ability ability in Abilities)
        {
            GameObject abilityButton = Instantiate(AbilityButton, AbilityTab);
            abilityButton.GetComponent<AbilityButtonController>().Initialize(ability , false);
        }
    }

    //Add selected abilities to the queue
    public void UpdateAbilityQueue(Ability ability)
    {
        GameObject abilityButton = Instantiate(AbilityButton, AbilityQueue);
        abilityButton.GetComponent<AbilityButtonController>().Initialize(ability , true);
    }

    //Remove selected abilities to the queue
    public void RemoveAbility(RectTransform abiltiy)
    {
        //Remove 20 to get multiples of 50 then divide by 50 to get which index it is
        int index = Mathf.FloorToInt((abiltiy.anchoredPosition.x - 20) / 50);
        Destroy(abiltiy.gameObject);

        //Remove the ability from the current player
        GameManager.Instance.RemoveAbility(index);
    }

    void EmptyQueue(bool empty)
    {
        //Empty the queue
        if(empty)
        {
            foreach (Transform child in AbilityQueue.transform)
            {
                Destroy(child.gameObject);
            }
        }
        
        //Remove the first element in the queue
        else
        {
            Destroy(AbilityQueue.GetChild(0).gameObject);
        }
    }

    //Update the Stamina Text
    public void UpdateStaminaTxt(int stamina)
    {
        StaminaText.text = $"Stamina: {stamina}";
    }
}