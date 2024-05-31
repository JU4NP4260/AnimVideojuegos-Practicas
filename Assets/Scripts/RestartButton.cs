using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneReloader : MonoBehaviour
{
    void Update()
    {
        // Check if the "R" key is pressed
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadCurrentScene();
        }
    }

    // Call this function to reload the current scene
    public void ReloadCurrentScene()
    {
        // Get the active scene
        Scene currentScene = SceneManager.GetActiveScene();

        // Reload the active scene
        SceneManager.LoadScene(currentScene.name);
    }
}
