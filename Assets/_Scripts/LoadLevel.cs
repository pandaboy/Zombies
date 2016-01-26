using UnityEngine;

public class LoadLevel : MonoBehaviour
{
    public string levelName = "";

    public void NextLevel()
    {
        Application.LoadLevel(levelName);
    }
}
