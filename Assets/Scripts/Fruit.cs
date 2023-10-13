using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public float heal = 30f;
    public AudioClip fruitGet;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {

            PlaycoinGet();

            //  Destroy(gameObject);
            gameObject.SetActive(false);
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
