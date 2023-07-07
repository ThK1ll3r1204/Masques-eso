using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuAudioManager : MonoBehaviour
{
    public string TutorialScene;
    public string MenuScene; // Escena donde va a continuar el sonido
    public string CreditScene;

    private static MenuAudioManager instance;
    private bool audioPlayed = false;
    private bool creditsPassed = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == CreditScene)
        {
            audioPlayed = false;
            creditsPassed = true;
        }

        if (SceneManager.GetActiveScene().name == MenuScene && creditsPassed)
        {
            audioPlayed = true;
        }

        if (audioPlayed && SceneManager.GetActiveScene().name != MenuScene)
        {
            Destroy(instance.gameObject);
        }

        if (SceneManager.GetActiveScene().name == TutorialScene)
        {
            Destroy(instance.gameObject);
        }
    }
}
