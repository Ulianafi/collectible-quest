using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI : MonoBehaviour
{
    public static UI Instance;
    public TMP_Text collectibleText;
    public TMP_Text messageText;

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateCollectibleCount(int collected, int total)
    {
        collectibleText.text = $"Collected: {collected}/{total}";

        if (collected == 5)
        {
            ShowMessage("Chest is open!");
        }
    }

    public void ShowMessage(string message)
    {
        messageText.text = message;
        StartCoroutine(ClearMessageAfterDelay(4f));
    }

    private IEnumerator ClearMessageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        messageText.text = "";
    }
}
