using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Object3D object3D;
    
    [SerializeField] private Animator animator;
    [SerializeField] public Lanes currentLane;
    [SerializeField] private int currentLaneInt = 0;

    private bool jumpCooldown = false;
    private float jumpCooldownTimer = .25f;
    private float jumpTimer = 1f;

    public bool jumping = false;
    private float defaultYAxis = -4f;

    private Vector2 minMax;
    private void Awake()
    {
        TryGetComponent<Object3D>(out this.object3D);
        this.currentLane = (Lanes)this.currentLaneInt;

        (int, int) list = LaneClassifiers.GetMinMaxLaneValues();
        this.minMax = new Vector2(list.Item1, list.Item2);
        
        this.object3D.itemPosition.y = this.defaultYAxis;
    }

    private void MoveToLane()
    {
        this.object3D.itemPosition.x = Mathf.Lerp(this.object3D.itemPosition.x,
            ((LaneClassifiers.laneGaps + 1f) * (int)this.currentLane), 32f * Time.deltaTime);
    }

    private IEnumerator JumpCooldown()
    {
        this.jumpCooldown = true;
        yield return new WaitForSeconds(this.jumpCooldownTimer);
        this.jumpCooldown = false;
    }

    private IEnumerator JumpDown()
    {
        float elapsedTime = 0f;

        for (float t = 0; t < 1f; t += Time.deltaTime / this.jumpCooldownTimer)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / this.jumpCooldownTimer;
            
            this.object3D.itemPosition.y = Mathf.Lerp(this.object3D.itemPosition.y, this.defaultYAxis, progress);
            yield return null;
        }

        this.jumping = false;
        this.object3D.itemPosition.y = this.defaultYAxis;
        StartCoroutine(JumpCooldown());
    }
    private IEnumerator JumpUp()
    {
        float elapsedTime = 0f;
        
        this.animator.SetTrigger("Jump");

        this.jumpCooldown = true;
        for (float t = 0; t < 1f; t += Time.deltaTime / this.jumpCooldownTimer)
        {
            if (t > .35f) this.jumping = true;
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / this.jumpCooldownTimer;
            
            this.object3D.itemPosition.y = Mathf.Lerp(this.object3D.itemPosition.y, this.defaultYAxis + 5f, progress);
            yield return null;
        }
        
        StartCoroutine(JumpDown());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            this.currentLaneInt = (int)Mathf.Clamp(this.currentLaneInt - 1, this.minMax.x, this.minMax.y);
            this.animator.SetTrigger("MoveLeft");
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            this.currentLaneInt = (int)Mathf.Clamp(this.currentLaneInt + 1, this.minMax.x, this.minMax.y);
            this.animator.SetTrigger("MoveRight");
        }

        if (Input.GetKeyDown(KeyCode.Space) && !this.jumpCooldown)
            StartCoroutine(JumpUp());
            
        this.currentLane = (Lanes)this.currentLaneInt;
        MoveToLane();
    }
}
