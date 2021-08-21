using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityButtonController : MonoBehaviour
{
    public Text DescriptionText;

    public void Initialize(Ability ability , bool inQueue)
    {
        //Add ability to queue
        if(!inQueue)
        {
            GetComponent<Image>().sprite = ability.AbilitySprite;
            DescriptionText.text = ability.Description();

            //Add the ability to the queue once selected
            GetComponent<Button>().onClick.AddListener(() =>
            {
                GameManager.Instance.AbilitySelected(ability);
            });
        }

        //Remove ability from queue
        else
        {
            GetComponent<Image>().sprite = ability.AbilitySprite;
            GetComponent<Button>().onClick.AddListener(() =>
            {
                UIManager.Instance.RemoveAbility(GetComponent<RectTransform>());
            });
        } 
    }
}