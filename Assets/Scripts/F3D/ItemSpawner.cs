using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ItemSpawner : MonoBehaviour
{
    public List<Object3D> objects = new List<Object3D>();

    [SerializeField] private Object3D object3DToSpawn;
    [SerializeField] private float speed = 0.01f;
    [SerializeField] private Transform parent;

    [SerializeField] private Vector2 xRandomness;
    [SerializeField] private float yAxis;
    [SerializeField] private float zSpawnLocation;

    private void Start()
    {
        InvokeRepeating(nameof(BeginSpawn), 1f, 0.3f);
    }

    private void BeginSpawn()
    {
        Object3D spawnedObject3D = Instantiate(object3DToSpawn, parent);
        spawnedObject3D.itemPosition = new Vector3(spawnedObject3D.itemPosition.x, yAxis, zSpawnLocation);
        objects.Add(spawnedObject3D);
    }

    private void MoveObjects()
    {
        objects.RemoveAll(item => item == null); // Efficiently removes null objects

        foreach (Object3D item in objects)
        {
            ItemMover(item);
        }
    }

    private void SetOrderIndex()
    {
        List<Transform> children = new List<Transform>();

        foreach (Transform child in transform)
        
