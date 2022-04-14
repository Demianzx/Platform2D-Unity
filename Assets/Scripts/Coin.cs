using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] AudioClip coinPickupSFX;
    [SerializeField] int pointsForCoinPickup = 100;
    bool isPickedUp = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !isPickedUp)
        {
            isPickedUp = true;
            GetComponent<AudioSource>().PlayOneShot(coinPickupSFX);
            FindObjectOfType<GameSession>().AddToScore(pointsForCoinPickup);
            //gameObject.SetActive(false);
            Destroy(gameObject, 0.1f);
            
        }
    }
}
