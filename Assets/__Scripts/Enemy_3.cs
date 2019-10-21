using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_3 : Enemy
{
    [Header("Set in Inspector: Enemy_3")]
    public float lifeTime = 5;

    [Header("Set Dynamically: Enemy_3")]
    public Vector3[] points;
    public float birthTime;

    // Start is called before the first frame update
    void Start()
    {
        points = new Vector3[3];

        // the start position has aready been set by Main.SpawnEnemy()
        points[0] = pos;

        // set xMin and xMax the same way that Main.SpawnEnemy() does
        float xMin = -bndCheck.camWidth + bndCheck.radius;
        float xMax = bndCheck.camWidth - bndCheck.radius;

        Vector3 v;
        // Pick a random middle position in the bottom half
        v = Vector3.zero;
        v.y = Random.Range(xMin, xMax);
        points[1] = v;

        // set the birthtime to the current time
        birthTime = Time.time;
    }

    public override void Move()
    {
        // bezier curve based on a u value between 0 and 1
        float u = (Time.time - birthTime) / lifeTime;

        if (u > 1)
        {
            // This Enemy_3 has finished its life
            Destroy(this.gameObject);
            return;
        }

        // interpolate the three points
        Vector3 p01, p12;
        p01 = (1 - u) * points[0] + u * points[1];
        p12 = (1 - u) * points[1] + u * points[2];
        pos = (1 - u) * p01 + u * p12;
    }

    // Update is called once per frame
    void Update()
    {
        Move();   
    }
}
