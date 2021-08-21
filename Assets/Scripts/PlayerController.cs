using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public List<Ability> Abilities;
    public Transform SpawnPoint;
    public GameObject Enemy;
    public int stamina;
    public int health;

    List<Ability> AbilityQueue;
    Ability currentAbility;
    Animation anim;
    GameObject abilityShard;

    // Start is called before the first frame update
    void Start()
    {
        AbilityQueue = new List<Ability>();
        EventManager.updateStamina(stamina);

        //Add each ability's animation to the player's animation clip
        anim = GetComponent<Animation>();
        LoadAnimationClips();
    }

    public void LoadAnimationClips()
    {
        foreach(Ability ability in Abilities)
        {
            anim.AddClip(ability.CastAnimation, ability.CastAnimation.name);
        }
    }

    //Add ability to queue
    public bool AddAbility(Ability ability)
    {
        //Check if the stamina cost is less than the remaining stamina
        if (ability.StaminaCost <= stamina)
        {
            AbilityQueue.Add(ability);
            stamina -= ability.StaminaCost;
            EventManager.updateStamina.Invoke(stamina);
            return true;
        }

        else
        {
            return false;
        }
    }

    //Remove ability from queue
    public void RemoveAbility(int index)
    {
        stamina += AbilityQueue[index].StaminaCost;
        AbilityQueue.RemoveAt(index);
        EventManager.updateStamina.Invoke(stamina);
    }

    public IEnumerator ExecuteAbilities()
    {
        //Execute all abilities in the queue
        foreach(Ability ability in AbilityQueue)
        {
            //If enemy died stop shooting
            if(Enemy == null)
            {
                EventManager.updateQueue(true);
                break;
            }

            //Update the current ability being played
            currentAbility = ability;

            //Start the animation to shoot the shard
            anim.Play(ability.CastAnimation.name);
            EventManager.updateQueue.Invoke(false);
            yield return new WaitUntil(() => abilityShard == null && !anim.IsPlaying(ability.CastAnimation.name));
        }

        //Empty the queue and reset the stamina
        AbilityQueue.Clear();
        stamina = 10;
        EventManager.updateStamina.Invoke(stamina);
    }

    //Shoot the shard called from the ability animation
    void ShootShard()
    {
        abilityShard = Instantiate(currentAbility.AbilityShard);
        abilityShard.transform.position = SpawnPoint.position;
        abilityShard.GetComponent<Bullet>().InitializeBullet(Enemy, currentAbility.AbilityDamage);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        //Player died destroy it
        if(health <= 0)
        {
            Destroy(this.gameObject);
        }

        //Change color to show that player has half of his health remaining
        else if(health < 5)
        {
            GetComponent<Renderer>().material.SetColor("_Color", Color.gray);
        }
    }
}