using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CambioBoton : MonoBehaviour
{
    Button button;
    public int sceneIndex;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ChangeSceneXindex);
    }

    public void ChangeSceneXindex()
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void exit()
    {
        Application.Quit();
    }

}

