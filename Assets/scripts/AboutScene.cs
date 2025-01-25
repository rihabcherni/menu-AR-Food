using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AboutScene : MonoBehaviour
{
    public void AcceuilBack()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex > 0)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            Debug.Log("No previous scene in build index.");
        }
    }
}
