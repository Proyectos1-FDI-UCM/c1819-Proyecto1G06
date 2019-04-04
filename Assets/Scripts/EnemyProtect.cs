using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProtect : MonoBehaviour
{
    public Transform Amigo;
    private Transform player;

    RaycastHit2D vectorEJ;

    // Use this for initialization
    void Start()
    {
        player = GameManager.instance.player.transform;

    }

    // Update is called once per frame
    void Update()
    {
        vectorEJ = Physics2D.Linecast(player.position, Amigo.position);
        Debug.DrawLine(player.position, Amigo.position);


        transform.position = vectorEJ.transform.position / 2;

        /*vectorEJ = Physics2D.Linecast(player.position, Amigo.position);
        Debug.DrawLine(player.position, Amigo.position);
        if (transform.position != vectorEJ.transform.position / 2)
        {
            transform.position =
        }
        else
        {
            transform.position = vectorEJ.transform.position / 2;
        }*/

    }
}
