using System;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(PlayerHealth), typeof(PlayerMovement), typeof(Object3D))]
public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private ItemSpawner spawner;

    private Object3D object3D;
    private PlayerHealth playerHealth;
    private PlayerMovement playerMovement;

    private void Awake()
    {
        playerHealth = GetComponent<PlayerHealth>();
        playerMovement = GetComponent<PlayerMovement>();
        object3D = GetComponent<Object3D>();
    }

    private void Update()
    {
        if (playerMovement.jumping) return;

        foreach (var obstacle in spawner.objects)
        {
            if (!obstacle) continue;
            var obstacleDefiner = obstacle.GetComponent<ObstacleDefiner>();
            if (!obstacleDefiner || obstacleDefiner.currentLane != playerMovement.currentLane) continue;

            float distance = obstacle.itemPosition.z - object3D.transform.position.z;
            if (distance is > -150f and < -100f)
            {
                playerHealth.Damage(5f);
                Destroy(obstacle.gameObject);
            }
        }
    }

    private void OnGUI()
    {
        GUIStyle style = new GUIStyle { fontSize = 12 };
        foreach (var obstacle in spawner.objects)
        {
            if (!obstacle) continue;
            float distance = obstacle.itemPosition.z - object3D.transform.position.z;
            Handles.Label(new Vector3(obstacle.transform.position.x, obstacle.transform.position.y, 0), 
                          Mathf.Round(distance).ToString(), style);
        }
    }
}
