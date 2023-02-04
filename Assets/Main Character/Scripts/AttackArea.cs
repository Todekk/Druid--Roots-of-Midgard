using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    public int damage = 3;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<Health>() != null)
        {
            Health enemyHealth = collider.GetComponent<Health>();
            enemyHealth.TakeDamage(damage);
        }
    }
}
