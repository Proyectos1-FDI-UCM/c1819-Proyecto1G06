using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionDisarmArea : MonoBehaviour {

    public float disarmDuartion;

    private void Start()
    {
        GetComponent<FollowTarget>().target = GameManager.instance.player.transform;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Shooting shooting = collision.GetComponentInChildren<Shooting>();
        if (shooting != null)
        {
            shooting.Disarm(disarmDuartion);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Shooting shooting = collision.GetComponentInChildren<Shooting>();
        if (shooting != null)
        {
            shooting.Rearm();
        }
    }
}