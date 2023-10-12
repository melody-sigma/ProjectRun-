using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public float heal = 5f;
    public AudioClip fruitGet;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {

            PlaycoinGet();

            Destroy(gameObject);
        }
    }
    private void PlaycoinGet()
    {
        if (fruitGet != null)
        {
            AudioSource.PlayClipAtPoint(fruitGet, transform.position);
        }
    }
}
