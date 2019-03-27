using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidVision : MonoBehaviour
{
    Transform playerHead { get { return GameManager.instance.player.transform.GetChild(0); } }
    VoluntarioController controller;

    void Awake()
    {
        controller = GetComponent<VoluntarioController>();
    }

    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(playerHead.position, playerHead.right, 100, LayerMask.GetMask("Enemies"));
        if (hit.transform != null)
        {
            Vector2 perpendicular = new Vector2(-playerHead.right.y, playerHead.right.x);
            if(Vector3.Angle(perpendicular, playerHead.right - transform.position) > 90)
            {
                controller.AvoidDirection(perpendicular);
            } else
            {
                controller.AvoidDirection(new Vector2(playerHead.right.y, -playerHead.right.x));
            }
        }
    }
}
