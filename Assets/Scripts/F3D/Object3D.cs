using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object3D : MonoBehaviour
{
    public Vector3 itemPosition;

    private void Update()
    {
        float perspective = CameraComponent.focalLength / (CameraComponent.focalLength + this.itemPosition.z);
        
        this.transform.localScale = Vector3.one * perspective;
        this.transform.position = new Vector2(this.itemPosition.x, this.itemPosition.y) * perspective;
    }
}
