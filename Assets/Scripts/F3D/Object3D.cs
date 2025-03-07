using UnityEngine;

public class Object3D : MonoBehaviour
{
    public Vector3 itemPosition;

    private void Update()
    {
        // Ensure CameraComponent exists before using it
        if (CameraComponent.focalLength <= 0f)
        {
            Debug.LogWarning("CameraComponent.focalLength is not properly set!");
            return;
        }

        // Calculate perspective scaling
        float depthFactor = CameraComponent.focalLength / (CameraComponent.focalLength + itemPosition.z);

        // Apply transformations
        transform.localScale = Vector3.one * depthFactor;
        transform.position = new Vector3(itemPosition.x * depthFactor, itemPosition.y * depthFactor, transform.position.z);
    }
}
