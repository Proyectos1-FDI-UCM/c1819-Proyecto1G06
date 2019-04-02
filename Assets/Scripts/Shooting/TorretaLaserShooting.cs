using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorretaLaserShooting : Shooting {

   Vector2 startAngle;

    public float timeStartedLerping, lerpTime;

    public Transform endPosition;

    public Vector3 startPosition;

    public bool found = false;

    private void Start()
    {
        ResetCooldown();
        startAngle = transform.eulerAngles;

        timeStartedLerping = Time.time;
        startPosition = player.transform.position;     
    }

    // Update is called once per frame
    public override void Update () {

        if (!found)
        {
            Vector2 lookDirection = Lerp(startPosition - transform.position, (player.transform.position) - transform.position, timeStartedLerping, lerpTime);
            float angle = Mathf.Atan(lookDirection.y / lookDirection.x) * (180 / Mathf.PI);

            transform.eulerAngles = new Vector3(0, 0, angle + (lookDirection.x < 0f ? 180f : 0f));

            if (shootCooldown == 0)
                ResetCooldown();

            if ((Time.time - timeStartedLerping) / lerpTime == 1)
                timeStartedLerping = Time.time;

        }  
    }

    public Vector3 Lerp(Vector3 start, Vector3 end, float timeStartedLerping, float lerpTime)
    {
        float timeSinceStarted = Time.time - timeStartedLerping;
        float percentageComplete = timeSinceStarted / lerpTime;

        Vector3 result = Vector3.Lerp(start, end, percentageComplete);

        return result;
    }
}

