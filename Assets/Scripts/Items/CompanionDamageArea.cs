using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionDamageArea : MonoBehaviour {

    public float timeBetweenDamage;
    public float Damage;

    float timer;

    private void Start()
    {
        GetComponent<FollowTarget>().target = GameManager.instance.player.transform;
        timer = timeBetweenDamage;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        EnemyHealth health = other.GetComponent<EnemyHealth>();
        if(health != null && timer == 0f)
        {
            health.TakeDamage(Damage);
            timer = timeBetweenDamage;
        }
    }

    private void Update()
    {
        if (timer > 0f) timer -= Time.deltaTime;
        else timer = 0f;
    }

}
