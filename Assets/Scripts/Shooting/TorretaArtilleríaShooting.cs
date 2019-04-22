using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorretaArtilleríaShooting : Shooting {

    public AudioClip shootClip;
    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Start()
    {
        ResetCooldown();
    }

    /// <summary>
    /// Apunta al jugador y le dispara
    /// </summary>  
    public override void Update ()
    {
        Vector2 lookDirection = (player.position) - transform.position;
        float angle = Mathf.Atan(lookDirection.y / lookDirection.x) * (180 / Mathf.PI) + (lookDirection.x < 0f ? 180f : 0f);

        if (angle > 90 || angle < -90) GetComponent<SpriteRenderer>().flipY = true; // Hacer que no tenga un movimiento poco natural
        else GetComponent<SpriteRenderer>().flipY = false;

        transform.eulerAngles = new Vector3(0, 0, angle);
    }

    public override void Shoot()
    {
        base.Shoot();
        if (shootCooldown <= 0)
        audioSource.PlayOneShot(shootClip);
    }
}
