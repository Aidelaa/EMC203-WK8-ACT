using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine.Serialization;

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
        InvokeRepeating(nameof(BeginSpawn), 1, 0.3f);
    }

    private void BeginSpawn()
    {
        Object3D spawnedObject3D = Instantiate(this.object3DToSpawn, this.parent);
        spawnedObject3D.itemPosition = SetStarterLocation(spawnedObject3D.itemPosition);
            
        this.objects.Add(spawnedObject3D);
    }

    private Vector3 SetStarterLocation(Vector3 point)
    {
        return new Vector3(point.x, this.yAxis, this.zSpawnLocation);
    }

    private void MoveObjects()
    {
        foreach (Object3D item in this.objects)
        {
            if (!item)
            {
                this.objects.Remove(item);
                return;   
            }
            
            ItemMover(item);
        }
    }

    private void SetOrderIndex()
    {
        List<Transform> children = new List<Transform>();
        foreach(Transform child in this.transform)
            children.Add(child);
            
        children.OrderBy(x => x.position.z);
        
        for(int i = 0 ; i < children.Count; i++)
            children[i].SetSiblingIndex(i);
    }
    
    private void Update()
    {
        MoveObjects();
        SetOrderIndex();
    }

    private void ItemMover([CanBeNull] Object3D object3D)
    {
        if (!object3D) { return; }
        object3D.itemPosition.z -= (this.speed * Time.deltaTime);

        if (object3D.transform.localScale.x < 0f)
        {
            Destroy(object3D.gameObject);
        }
    }
}
