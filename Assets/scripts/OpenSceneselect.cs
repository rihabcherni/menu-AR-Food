using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenSceneselect : MonoBehaviour
{ 
    [Header("Name of the Scene to Load")]
    public string sceneName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("op")) 
        {
            if (!string.IsNullOrEmpty(sceneName))
            {
                SceneManager.LoadScene(sceneName);
                Debug.LogError("pppppppppppppppppppppppp");
            }
            else
            {
                Debug.LogError("Scene name is not set. Please assign a scene name in the inspector.");
            }
        }
    }
}
