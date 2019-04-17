using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCanvas : MonoBehaviour
{
    public void LoadLevel(string name)
    {
        GameManager.instance.LoadScene(name);
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
