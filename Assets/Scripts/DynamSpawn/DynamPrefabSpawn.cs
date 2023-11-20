using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamPrefabSpawn : MonoBehaviour
{
    [SerializeField] GameObject[] objectList; //The prefabs we intend to spawn, will pick a random one from this list. Make sure they're dynamic
    [SerializeField] int spawnRarity = 5; //How rare it is, smaller number means it spawns more often. 1 means always spawn. WANRING: IF 0 IT WILL NOT SPAWN
    private int spawnTarget = 1;
    public bool eligable = false; //the DynamEnabler will turn this on, and allow this script to do it's function
    public bool occupied = false; //the DynamDisabler will turn this of, and allow this script to resume it's function
    private bool attemptMade;
    
    void Update()
    {
        if(eligable && !occupied)
        {
            AttemptSpawn();
        }
        else
        {
            if (attemptMade)
            {
                attemptMade = false;
            }
        }
    }

    /// <summary>
    /// Make a chance based attempt to spawn the prefab. If the chance fails, increase the success rate for next attempt and turn off eligable bool. If the attempt succeeds, reset the success rate and spawn the object
    /// </summary>
    void AttemptSpawn()
    {
        if (!attemptMade)
        {
            int trySpawn = Random.Range(1, (spawnRarity + 1));
            if (trySpawn <= spawnTarget)
            {
                spawnTarget = 1;
                DoSpawn();
            }
            else
            {
                spawnTarget += 1;
            }
            attemptMade = true;
        }
        
    }
    /// <summary>
    /// Define the coordinates to the camera's current position and offset to the edge of that camera, then spawn the object
    /// </summary>
    void DoSpawn()
    {
        Vector3 spawnPosition = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        int pickObject = Random.Range(0, objectList.Length);
        Instantiate(objectList[pickObject], spawnPosition, Quaternion.identity);
        occupied = true;
    }
}
