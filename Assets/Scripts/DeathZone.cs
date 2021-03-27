using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public Canvas lostScreen;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.gameObject.GetComponentInParent<PlayerController>();
        if(player)
        {
            Destroy(player.gameObject);
            lostScreen.gameObject.SetActive(true);
        }
    }
}
