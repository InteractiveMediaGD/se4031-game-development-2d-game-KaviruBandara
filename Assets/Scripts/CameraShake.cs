using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;
    private CameraFollow cameraFollow;

    void Awake()
    {
        instance = this;
        cameraFollow = GetComponent<CameraFollow>();
    }

    public void Shake(float duration, float magnitude)
    {
        StopAllCoroutines(); // Reset shake if one is already going
        StartCoroutine(ShakeRoutine(duration, magnitude));
    }

    System.Collections.IEnumerator ShakeRoutine(float duration, float magnitude)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            // Apply shake safely through the follow script
            if (cameraFollow != null)
            {
                cameraFollow.shakeOffset = new Vector3(x, y, 0);
            }
            else
            {
                transform.localPosition = new Vector3(x, y, transform.localPosition.z);
            }

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Reset offset when done
        if (cameraFollow != null)
        {
            cameraFollow.shakeOffset = Vector3.zero;
        }
    }
}