using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [Header("Set in Inspector: Enemy")]
    public float speed = 10f;   // speed in m/s
    public float fireRate = .3f;    // Seconds/shots (unused)
    public float health = 10;
    public int score = 100; // Points earned for destroying this

    protected BoundsCheck bndCheck;

    private void Awake()
    {
        bndCheck = GetComponent<BoundsCheck>();
    }

    // This is a Property : A method that acts like a field
    public Vector3 pos
    {
        get
        {
            return (this.transform.position);
        }
        set
        {
            this.transform.position = value;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        if (bndCheck != null && !bndCheck.isOnScreen)
        {
            // read top to bottom, artemis will rain down
            if (pos.y < 0 - bndCheck.radius)
            {
                Destroy(gameObject);
            }
        }
    }

    public virtual void Move()
    {
        // top to bottom
        Vector3 tempPos = pos;
        tempPos.y -= speed * Time.deltaTime;
        pos = tempPos;
    }

    private void OnCollisionEnter(Collision coll)
    {
        GameObject otherGO = coll.gameObject;
        if (otherGO.tag == "Hero")
        {
            Destroy(gameObject);    // Destroy the powerup
            ScoreManager.EVENT(eScoreType.powerUp);
            //Poor man's debugging
            //print("Enemy hit by ProjectileHero: " + otherGO.name);
        }
        else
        {
            print("Enemy hit by non-ProjectileHero: " + otherGO.name);
        }
    }


}
