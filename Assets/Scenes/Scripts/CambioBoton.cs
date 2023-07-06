using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CambioBoton : MonoBehaviour
{
    Button button;
    public int sceneIndex;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(sceneIndex);
        }
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

