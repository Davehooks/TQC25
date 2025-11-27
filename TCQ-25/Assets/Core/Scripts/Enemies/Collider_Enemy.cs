using System.Collections;
using UnityEngine;


public class Collider_Enemy : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            PlayerCollider playerCollider = collision.gameObject.GetComponent<PlayerCollider>();
            
        }
    }

    

}

