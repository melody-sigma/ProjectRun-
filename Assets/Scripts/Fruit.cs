using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public float heal = 5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            /*PlayerController player = other.GetComponent<PlayerController>();
            if (player != null) {
                player.Heal(heal);
            }
            */
            Destroy(gameObject);
        }
    }
}
