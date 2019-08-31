using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnParent : MonoBehaviour
{
    public void ChangeEnemySpawnsActivity()
    {
        foreach (EnemySpawn spawner in GetComponentsInChildren<EnemySpawn>())
        {
            spawner.ChangeThisSpawnActivity();
        }
    }
}
