using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2 : Enemy
{
    [Header("Set in Inspector: Enemy_2")]
    // Determine how much the sine wave will affect movement
    public float sinEccentricity = 0.6f;
    public float lifeTime = 10;

    [Header("Set Dynamically: Enemy_2")]
    // uses sine wave to modify a 2 point linear interpolation
    public Vector3 p0;
    public Vector3 p1;
    public float birthTime;

    // Start is called before the first frame update
    void Start()
    {
        // Pick any point on the top of the screen
        p0 = Vector3.zero;
        p0.y = -bndCheck.camHeight - bndCheck.radius;
        p0.x = Random.Range(-bndCheck.camWidth, bndCheck.camWidth);

        // Pick any point at the top of the screen
        p1 = Vector3.zero;
        p1.y = bndCheck.camHeight + bndCheck.radius;
        p1.x = Random.Range(-bndCheck.camWidth, bndCheck.camWidth);

        // Possibly swap sides
        if (Random.value > 0.5f)
        {
            p0.y *= -1;
            p1.y *= -1;
        }

        // Set birthTime to the current time
        birthTime = Time.time;
    }

    public override void Move()
    {
        // Bezier curves
        float u = (Time.time - birthTime) / lifeTime;

        // lifetime is longer than birthtime if u > 1
        if (u > 1)
        {
            // This enemy_2 has finishd its life
            Destroy(this.gameObject);
            return;
        }

        // adjust u otherwise
        u = u + sinEccentricity * (Mathf.Sin(u * Mathf.PI * 2));

        // interpolate the two linear interpolation points
        pos = (1 - u) * p0 + u * p1;
   }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
}
