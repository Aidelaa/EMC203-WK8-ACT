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
        TryGetComponent<PlayerHealth>(out this.playerHealth);
        TryGetComponent<PlayerMovement>(out this.playerMovement);
        TryGetComponent<Object3D>(out this.object3D);
    }

    private void Update()
    {
        if (this.playerMovement.jumping) return;
        foreach (Object3D obstacle in this.spawner.objects)
        {
            if (!obstacle) continue;
            
            // this might be non performant but ye
            ObstacleDefiner obstacleDefiner = obstacle.GetComponent<ObstacleDefiner>();
            if (!obstacleDefiner)
                continue;
                
            if (obstacleDefiner.currentLane == this.playerMovement.currentLane)
            {
                float distance = obstacle.itemPosition.z - this.object3D.transform.position.z;
                
                if (distance is > -150f and < -100f)
                {
                    this.playerHealth.currentHealth -= 5f;
                    Destroy(obstacle.gameObject);
                }
            }
        }    
    }

    private void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = 12;
        foreach (Object3D obstacle in this.spawner.objects)
        {
            if (!obstacle) continue;
            
            float distance = (obstacle.itemPosition.z - this.object3D.transform.position.z);
            Handles.Label(new Vector3(obstacle.transform.position.x, obstacle.transform.position.y, 0), $"{Mathf.Round(distance)}", style);
        }    
    }
}
