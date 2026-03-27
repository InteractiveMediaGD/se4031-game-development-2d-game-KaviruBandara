using UnityEngine;

public class EnemyEyeLook : MonoBehaviour
{
    public Transform player;

    public Transform leftPupil;
    public Transform rightPupil;

    public float moveAmount = 0.05f;

    Vector3 leftStartPos;
    Vector3 rightStartPos;

    void Start()
    {
        leftStartPos = leftPupil.localPosition;
        rightStartPos = rightPupil.localPosition;
    }

    void Update()
    {
        if (player == null) return;

        MovePupil(leftPupil, leftStartPos);
        MovePupil(rightPupil, rightStartPos);
    }

    void MovePupil(Transform pupil, Vector3 startPos)
    {
        Vector3 direction = (player.position - pupil.position).normalized;

        pupil.localPosition = startPos + new Vector3(
            direction.x * moveAmount,
            direction.y * moveAmount,
            0f
        );
    }
}
