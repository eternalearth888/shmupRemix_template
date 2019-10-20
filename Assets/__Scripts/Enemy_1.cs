using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1 : Enemy
{
    [Header("Set in Inspector: Enemy_1")]
    // #seconds for a full sine wave
    public float waveFrequency = 2;
    // sine wave width in meters
    public float waveWidth = 4;
    public float waveRotx = 45;

    // The initial value of the x pos
    public float y0;

    public float birthTime;

    // Start is called before the first frame update
    // This is valid because it does not use the enemy superclass
    void Start()
    {
        //Set x0 to the inital position (remember, right to left)
        y0 = pos.y;

        birthTime = Time.time;
    }

    //override Move function in Enemy
    public override void Move()
    {
        Vector3 tempPos = pos;
        //theta adjusts 
        float age = Time.time - birthTime;
        float theta = Mathf.PI * 2 * age / waveFrequency;
        float sin = Mathf.Sin(theta);
        // move from right to left
        tempPos.y = y0 - waveWidth * sin;
        pos = tempPos;

        //rotate about y
        Vector3 rot = new Vector3(sin * waveRotx, 0, 0);
        this.transform.rotation = Quaternion.Euler(rot);

        base.Move();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
