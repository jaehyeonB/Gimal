using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndButton : MonoBehaviour
{
    public Button button;
    private void Awake()
    {
        button = GetComponent<Button>();
    }

    public void QuitGame()
    {
        Debug.Log("!!");
        Application.Quit();
    }
}
