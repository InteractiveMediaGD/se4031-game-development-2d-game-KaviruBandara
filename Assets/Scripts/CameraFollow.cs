using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // The player to follow
    public float smoothSpeed = 5f; // How smooth the camera moves
    public Vector3 offset = new Vector3(0f, 2f, -10f); // Default offset for a 2D game
    
    [HideInInspector]
    public Vector3 shakeOffset; // Modified by CameraShake to add shake effect without breaking the follow

    void LateUpdate()
    {
        if (target == null) return;

        // Calculate the base desired position
        Vector3 baseDesiredPosition = target.position + offset;
        
        // Smoothly move the camera towards the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position - shakeOffset, baseDesiredPosition, smoothSpeed * Time.deltaTime);
        
        // Apply the base position and then add the screen shake offset on top
        transform.position = smoothedPosition + shakeOffset;
    }
}
