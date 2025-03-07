using System;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Object3D object3D;
    [SerializeField] private Animator animator;
    [SerializeField] public Lanes currentLane;
    private int currentLaneInt;
    private bool jumpCooldown = false;
    private float defaultYAxis = -4f;
    private Vector2 laneBounds;

    private void Awake()
    {
        object3D = GetComponent<Object3D>();
        currentLaneInt = (int)currentLane;
        (int min, int max) = LaneClassifiers.GetMinMaxLaneValues();
        laneBounds = new Vector2(min, max);
        object3D.itemPosition.y = defaultYAxis;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) MoveLane(-1);
        else if (Input.GetKeyDown(KeyCode.D)) MoveLane(1);
        if (Input.GetKeyDown(KeyCode.Space) && !jumpCooldown) StartCoroutine(JumpSequence());
        MoveToLane();
    }

    private void MoveLane(int direction)
    {
        currentLaneInt = Mathf.Clamp(currentLaneInt + direction, (int)laneBounds.x, (int)laneBounds.y);
        animator.SetTrigger(direction < 0 ? "MoveLeft" : "MoveRight");
        currentLane = (Lanes)currentLaneInt;
    }

    private void MoveToLane()
    {
        object3D.itemPosition.x = Mathf.Lerp(object3D.itemPosition.x, 
            ((LaneClassifiers.laneGaps + 1f) * (int)currentLane), 32f * Time.deltaTime);
    }

    private IEnumerator JumpSequence()
    {
        jumpCooldown = true;
        animator.SetTrigger("Jump");
        float jumpHeight = defaultYAxis + 5f;

        yield return LerpPositionY(defaultYAxis, jumpHeight, 0.25f);
        yield return new WaitForSeconds(0.1f);
        yield return LerpPositionY(jumpHeight, defaultYAxis, 0.25f);

        jumpCooldown = false;
    }

    private IEnumerator LerpPositionY(float startY, float endY, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            object3D.itemPosition.y = Mathf.Lerp(startY, endY, elapsed / duration);
            yield return null;
        }
    }
}
