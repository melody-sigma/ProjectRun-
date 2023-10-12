using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // Start is called before the first frame update
    public int coinValue = 100;
    public AudioClip coinGet;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.GetScore(coinValue);
            }

            PlaycoinGet();


            Destroy(gameObject);
        }
    }

    private void PlaycoinGet()
    {
        if (coinGet != null)
        {
            AudioSource.PlayClipAtPoint(coinGet, transform.position);
        }
    }
}
