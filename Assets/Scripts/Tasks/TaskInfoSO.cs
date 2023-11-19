using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "TaskInfoSo", menuName = "ScriptableObjects/TaskInfoSO", order = 1)]
public class TaskInfoSO : ScriptableObject
{ 
    [field: SerializeField] public string id { get; private set; }

    [Header("General")]

    public string displayName; // description of the what the task entails

    [Header("Requirements")]

    public int maximumTasks; // how many tasks can be taken at once

    public TaskInfoSO[] taskPrerequisites; // the tasks needed to be completed before starting this task

    [Header("Steps")]

    public GameObject[] taskStepPrefabs; // the steps needed to complete the task

    [Header("Rewards")]
    // these will likely come from the package type later

    public int moneyReward; // how much money the player will receive upon completion

    public float timeReward; // how much time the player will receive upon completion



    // make the id is always the name of the Scriptable object asset
    private void OnValidate()
    {
        #if UNITY_EDITOR
        id = this.name;
        UnityEditor.EditorUtility.SetDirty(this);
        #endif
    }
}
