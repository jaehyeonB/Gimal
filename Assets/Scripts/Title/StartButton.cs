using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    public Button button;

    private void Start()
    {
        button = GetComponent<Button>();
    }
    public void Nextscene()
    {
        Debug.Log("!!");
        SceneManager.LoadScene("Scene1");
    }
}
