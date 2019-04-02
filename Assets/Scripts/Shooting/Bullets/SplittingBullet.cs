using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplittingBullet : MonoBehaviour {

    [Tooltip("La bala en la que se divide")] public BulletMovement generatedBulletPrefab;
    [Tooltip("El tiempo máximo que puede pasar hasta que se divida la bala")] public float maxLifetime;
    [Tooltip("El tiempo mínimo que puede pasar hasta que se divida la bala")] public float minLifetime;

    float lifetime;
    float curLifetime;

    /// <summary>
    /// El tiempo de vida es aleatorio
    /// </summary>
    private void Awake()
    {
        lifetime = Random.Range(minLifetime, maxLifetime);
    }

    /// <summary>
    /// Se divide si llega al fin de su tiempo de vida
    /// </summary>
    private void Update()
    {
        curLifetime += Time.deltaTime;
        if(curLifetime >= lifetime)
        {
            Split();
        }
    }

    /// <summary>
    /// Se divide si se choca con algo distinto a un enemigo
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Enemies"))
        {
            Split();
        }
    }

    /// <summary>
    /// Se divide la bala en dos perpendiculares a la dirección de esta
    /// </summary>
    void Split()
    {
        BulletMovement newBullet = Instantiate<BulletMovement>(generatedBulletPrefab, transform.position, Quaternion.identity, GameManager.instance.bulletPool);
        newBullet.Rotate(transform.up);
        newBullet = Instantiate<BulletMovement>(generatedBulletPrefab, transform.position, Quaternion.identity, GameManager.instance.bulletPool);
        newBullet.Rotate(-transform.up);
        Destroy(gameObject);
    }
}
