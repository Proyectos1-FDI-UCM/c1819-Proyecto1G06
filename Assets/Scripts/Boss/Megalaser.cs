using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Megalaser : MonoBehaviour {

    public Transform shootingPoint;
    public float movingTime;

    LineRenderer lr;
    Transform player;
    FollowDirection follow;
    float timer;
    bool shot = false;
    bool check = false;
    Animator anim;
    RaycastHit2D hit;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        follow = GetComponent<FollowDirection>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        player = GameManager.instance.player.transform;
    }

    private void OnEnable()
    {
        timer = movingTime;
        shot = false;
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (Mathf.Approximately(transform.position.x, player.transform.position.x))
            {
                follow.HardStop();
            }
            else follow.MoveTowards(- new Vector2(transform.position.x, 0) + new Vector2(player.transform.position.x, 0));
        }
        else if (!shot)
        {
            shot = true;
            follow.HardStop();
            anim.SetTrigger("Laser");
            lr.enabled = true;
            lr.SetPosition(0, shootingPoint.position);
            lr.SetPosition(1, new Vector2(shootingPoint.position.x, -100));
        }
        else if(check) CheckCollisions();

    }

    private void CheckCollisions()
    {
        hit = Physics2D.CircleCast(shootingPoint.position, lr.endWidth / 2, Vector2.down, 100, LayerMask.GetMask("Player"));
        if(hit.transform.GetComponent<PlayerHealth>() != null)
        {
            player.GetComponent<PlayerHealth>().TakeDamage();
        }
    }

    private void OnDisable()
    {
        lr.enabled = false;
    }

    public void StartChecking()
    {
        check = true;
    } 
}
