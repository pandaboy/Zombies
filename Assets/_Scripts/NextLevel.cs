using UnityEngine;

public class NextLevel : MonoBehaviour 
{
    public string nextLevel = "";

    public void LoadNextLevel()
    {
        Debug.Log("Next Level!");
        Application.LoadLevel(nextLevel);
    }

    public void RestartLevel()
    {
        Debug.Log("Restarting!");
        Application.LoadLevel(Application.loadedLevelName);
    }
}
