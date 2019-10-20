using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    static public Hero S; //Singleton

    [Header("Set in Inspector")]
    //The fields control the movement of the ship
    public float speed = 30;
   // public float rollMult = -45;
    public float pitchMult = 30;
    public float gameRestartDelay = 2f;

    // basic projectile - give the hero the ability to shoot
    public GameObject projectilePrefab;
    public float projectileSpeed = 40;

    // give the hero the ability to shoot laser beams
    public GameObject laserPrefab;
    public GameObject firePoint; // where we are going to spawn the laser
    private GameObject spawnedLaser;

    [Header("Set Dynamically")]
    private float _shieldLevel = 1;

    // This variable holds a reference to the last triggering GameObject
    private GameObject lastTriggerGo = null;

    void Awake()
    {
        if (S == null)
        {
            S = this; // Set the Singleton
        } else
        {
            Debug.LogError("Hero.Awake() - Attempted to assign Hero.S!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Pull in information from the Input class
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");

        // Change the transform.position based on the axes
        Vector3 pos = transform.position;
        pos.x += xAxis * speed * Time.deltaTime;
        pos.y += yAxis * speed * Time.deltaTime;
        transform.position = pos;

       
        // Allow the ship to fire
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TempFire();
        }

        // Laser beam prefab test?
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            TempLaser();
        }
        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            //destroy laser once not being used
            DestroyLaser();
        }
    }

    void TempFire()
    {
        GameObject projGO = Instantiate<GameObject>(projectilePrefab);
        projGO.transform.position = transform.position;
        Rigidbody rigiddB = projGO.GetComponent<Rigidbody>();
        rigiddB.velocity = Vector3.right * projectileSpeed;
    }

    void TempLaser()
    {
        // Raycast to grab mouse position???
        spawnedLaser = Instantiate<GameObject>(laserPrefab);
        spawnedLaser.SetActive(true);

    }

    void DestroyLaser()
    {
        spawnedLaser.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        Transform rootT = other.gameObject.transform.root;
        GameObject go = rootT.gameObject;
        //print("Triggered: " + go.name);

        // Make sure it;s not the same triggering go as last time
        if (go == lastTriggerGo)
        {
            return;
        }
        lastTriggerGo = go;

        if (go.tag == "Enemy")
        {
            shieldLevel--;
            Destroy(go);
        }
        else
        {
            print("Triggered by Non-Enemy: " + go.name);
        }
    }

    public float shieldLevel
    {
        get
        {
            return (_shieldLevel);
        }
        set
        {
            _shieldLevel = Mathf.Min(value, 4);
           // If the shield is going to be set to less than zero
           if (value < 0)
           {
               Destroy(this.gameObject);
               // Tell Main.S to restart the game after a delay
               Main.S.DelayedRestart(gameRestartDelay);
           }
        }
    }
}
