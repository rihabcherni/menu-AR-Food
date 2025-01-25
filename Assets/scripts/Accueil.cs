using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Diagnostics;

public class Accueil : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);

    }

    public void Quit()
    {
        Process.GetCurrentProcess().Kill();
        UnityEngine.Debug.Log("Application forcefully terminated.");
    }
}
