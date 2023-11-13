using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamPrefabSpawn : MonoBehaviour
{
    [SerializeField] GameObject Object; //The prefab we intend to spawn
    public int spawnRarity = 5; //How rare it is, smaller number means it spawns more often
    public float spawnDelay = 2; // How long to wait between each attempt to spawn
    private int spawnTarget = 0;
    private bool freeze = true;
    private float timePass = 0f;
    [SerializeField] float cameraEdgeX = 9f; //The camera size for the X axis (left and right)
    [SerializeField] float cameraEdgeY = 6f; //The camera size for the Y axis (up and down)
    
    void Update()
    {
        if(freeze)
        {
            Freeze();
        }
        else
        {
            AttemptSpawn();
        }
    }

    /// <summary>
    /// Make a chance based attempt to spawn the prefab. If the chance fails, increase the success rate for next attempt and turn on freeze bool. If the attempt succeeds, reset the success rate and spawn the object
    /// </summary>
    void AttemptSpawn()
    {
        int trySpawn = Random.Range(1, (spawnRarity + 1));
        if(trySpawn <= spawnTarget)
        {
            spawnTarget = 0;
            DoSpawn();
        }
        else
        {
            freeze = true;
            spawnTarget += 1;
        }
    }
    /// <summary>
    /// Wait the specified amount of time before turning freeze bool off.
    /// </summary>
    void Freeze()
    {
        if(spawnDelay <= timePass)
        {
            freeze = false;
            timePass = 0;
        }
        else
        {
            timePass += Time.deltaTime;
        }
    }
    /// <summary>
    /// Define the coordinates to the camera's current position and offset to the edge of that camera, then spawn the object
    /// </summary>
    void DoSpawn()
    {
        Vector3 positionBuffer = new Vector3(this.transform.parent.position.x, this.transform.parent.position.y, 0);
        Vector3 spawnPosition = positionBuffer;
        int pickEdge = Random.Range(0, 4);
        if(pickEdge == 0) //left
        {
            spawnPosition += new Vector3(-cameraEdgeX, Random.Range(-cameraEdgeY, cameraEdgeY), 0);
        }
        else if(pickEdge == 1) //right
        {
            spawnPosition += new Vector3(cameraEdgeX, Random.Range(-cameraEdgeY, cameraEdgeY), 0);
        }
        else if(pickEdge == 2) //up
        {
            spawnPosition += new Vector3(Random.Range(-cameraEdgeX, cameraEdgeX), -cameraEdgeY, 0);
        }
        else if (pickEdge == 3) //down
        {
            spawnPosition += new Vector3(Random.Range(-cameraEdgeX, cameraEdgeX), cameraEdgeY, 0);
        }
        Instantiate(Object, spawnPosition, Quaternion.identity);
    }
}
