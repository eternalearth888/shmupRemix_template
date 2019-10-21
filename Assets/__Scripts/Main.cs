using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// GameController
public class Main : MonoBehaviour
{
    static public Main S;   // A singleton for Main

    [Header("Set in Inspector")]
    public GameObject[] prefabEnemies; // an array of the prefab enemies so that we can choose from them randomly later
    public float enemySpawnPerSecond = .5f;
    public float enemyDefaultPadding = 1.5f;

    // power up stuff
    public GameObject[] prefabPowerUps;
    public float powerUpSpawnPerSecond = .5f;
    public float powerUpDefaultPadding = 1.5f;

    private BoundsCheck bndCheck;

    void Awake()
    {
        // If we don't currently have a game, make it
        if (S == null)
        {
            S = this;
        }
        else if (S != this)
        {
            // Destory duplicate game objects
            Destroy(gameObject);
        }
        
        //Set bndCheck to reference the BoundsCheck component on this GameObject
        bndCheck = GetComponent<BoundsCheck>();
        // Invoke SpawnEnemy() once (in 2 seconds based on default values)
        Invoke("SpawnEnemy", 1f/enemySpawnPerSecond);
        // Invoke SpawnPowerUp() starting 10 seconds, and then every 10 seconds
        Invoke("SpawnPowerUp", 2f*(1f / powerUpSpawnPerSecond));
    }

    public void SpawnEnemy()
    {
        // Pick a random Enemy prefab to instantiate
        int ndx = Random.Range(0, prefabEnemies.Length);
        // go is the currently random chosen enemy prefab that has now become a game object in game
        GameObject go = Instantiate<GameObject>(prefabEnemies[ndx]);

        // Position the Enemy to the right the screen with a random y position
        float enemyPadding = enemyDefaultPadding;

        if (go.GetComponent<BoundsCheck>() != null)
        {
            // enemey padding is based on the current enemy's radius
            enemyPadding = Mathf.Abs(go.GetComponent<BoundsCheck>().radius);
        }

        // Set the initial position for the spawned enemy
        Vector3 pos = Vector3.zero;
        float yMax = -bndCheck.camHeight + enemyPadding;
        float yMin = bndCheck.camHeight - enemyPadding;
        pos.y = Random.Range(yMin, yMax);
        pos.x = bndCheck.camWidth + enemyPadding;
        // move them
        go.transform.position = pos;

        // Invoke SpawnEnemy() again
        // USing Invoke() instead of InvokeRepeating() makes spawning more dynamic
        Invoke("SpawnEnemy", 1f/enemySpawnPerSecond);
    }

    public void SpawnPowerUp()
    {
        // Pick a random Enemy prefab to instantiate
        int ndx = Random.Range(0, prefabPowerUps.Length);
        // go is the currently random chosen enemy prefab that has now become a game object in game
        GameObject go = Instantiate<GameObject>(prefabPowerUps[ndx]);

        // Position the Enemy to the right the screen with a random y position
        float powerUpPadding = powerUpDefaultPadding;

        if (go.GetComponent<BoundsCheck>() != null)
        {
            // power up padding is based on the current enemy's radius
            powerUpPadding = Mathf.Abs(go.GetComponent<BoundsCheck>().radius);
        }

        // Set the initial position for the spawned power up
        Vector3 pos = Vector3.zero;
        float xMin = -bndCheck.camHeight + powerUpDefaultPadding;
        float xMax = bndCheck.camHeight - powerUpDefaultPadding;
        pos.x = Random.Range(xMin, xMax);
        pos.y = bndCheck.camHeight + powerUpDefaultPadding;
        // move them
        go.transform.position = pos;

        // Invoke SpawnEnemy() again
        // USing Invoke() instead of InvokeRepeating() makes spawning more dynamic
        Invoke("SpawnPowerUp", 2f * (1f / powerUpSpawnPerSecond));
    }

    public void DelayedRestart(float delay)
    {
        // Invoke the Restart() method in delay seconds
        Invoke("Restart", delay);
    }

    public void Restart()
    {
        // Reload MainMenu to restart th game
        SceneManager.LoadScene("_Main_Menu");
    }
}
