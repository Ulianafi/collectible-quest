using UnityEngine;
using UnityEngine.SceneManagement;

public class Chest : MonoBehaviour
{
    public string sceneToLoad;
    public int requiredCrystals = 5;

    private void OnMouseDown()
    {
        if (Collectible.collectedCount >= requiredCrystals)
        {
            // Load the new scene
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            // Show message about lack of crystals
            if (UI.Instance != null)
            {
                UI.Instance.ShowMessage("Not enough crystals to open the chest!");
            }
        }
    }
}