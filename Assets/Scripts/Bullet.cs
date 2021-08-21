using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    GameObject target;
    Vector3 direction;
    float speed = 5;
    int damage;

    //Set the bullet's target and damage
    public void InitializeBullet(GameObject t , int dmg)
    {
        target = t;
        damage = dmg;
        direction = (target.transform.position - this.transform.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        //Move the bullet towards its target
        this.transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Bullet collided with target
        if (other.gameObject == target)
        {
            other.GetComponent<PlayerController>().TakeDamage(damage);
            Destroy(this.gameObject);
        }
    }
}