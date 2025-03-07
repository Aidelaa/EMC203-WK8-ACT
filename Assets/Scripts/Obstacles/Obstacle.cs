using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    public Lanes currentLane;
    private Object3D object3D;
    
    private void Awake()
    {
        TryGetComponent<Object3D>(out this.object3D);
        
        this.spriteRenderer.flipX = Random.Range(0, 1) == 1;
    }

    private void Start()
    {
        if (this.object3D == null) return;
        Lanes lane = LaneClassifiers.GetRandomLane();
        
        this.currentLane = lane;
        this.object3D.itemPosition.x = LaneClassifiers.laneGaps * (int)lane;
    }
}
