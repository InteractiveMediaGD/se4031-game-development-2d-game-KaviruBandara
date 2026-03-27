using UnityEngine;

public class DontDestroyMusic : MonoBehaviour
{
    private static DontDestroyMusic instance;

    void Awake()
    {
        // If an instance of the music already exists (e.g., we went back to the Main Menu from the game),
        // destroy this duplicate so we don't have multiple tracks playing at once over each other!
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        // If this is the very first time the music loads, set it as the official instance 
        // and tell Unity never to destroy it when changing scenes!
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
