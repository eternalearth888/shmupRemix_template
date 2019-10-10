using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    private BoundsCheck bndCheck;

    private void Awake()
    {
        bndCheck = GetComponent<BoundsCheck>();
    }

    // Update is called once per frame
    void Update()
    {
        // if a projectile goes off the top of the screen, then destroy it
        if (bndCheck.offUp)
        {
            Destroy(gameObject);
        }
    }
}
