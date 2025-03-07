using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    public Lanes currentLane;
    private Object3D object3D;

    private void Awake()
    {
        this.object3D = GetComponent<Object3D>();

        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = Random.Range(0, 2) == 1;
        }
    }

    private void Start()
    {
        if (object3D == null) return;

        currentLane = LaneClassifiers.GetRandomLane();
        object3D.itemPosition.x = LaneClassifiers.laneGaps * (int)currentLane;
    }
}
