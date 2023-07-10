using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    //Player
    PlayerStats pStats;


    //Interface
    GameObject HUD;
    GameObject wandBar;
    Image wBarSprite;
    Image lBarSprite;
    Image cBarSprite;

    public bool isPaused;

    public List<NPCs> NPCList = new List<NPCs>();

    private void Awake()
    {
        HUD = GameObject.Find("HUD").gameObject;
        pStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        wandBar = GameObject.Find("WandBar").gameObject;
        wBarSprite = GameObject.Find("Wandfill").GetComponent<Image>();
        lBarSprite = GameObject.Find("Lifefill").GetComponent<Image>();
        cBarSprite = GameObject.Find("Calfill").GetComponent<Image>();
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Level1" || SceneManager.GetActiveScene().name == "Level2F")
        {
            HUD.gameObject.SetActive(true);
            pStats.canChangeWpn = true;
        }
        else
        { 
            HUD.gameObject.SetActive(false);
            pStats.canChangeWpn = false;
        }

        isPaused = false;

       // StartCoroutine(DebugNPCs());
    }


    void Update()
    {
        #region "Barras"
        // Barra del Cooldown del baculo
        if (pStats.fireBallsCount >= 3)
        {
            wandBar.gameObject.SetActive(true);
        }
        else
        {
            wandBar.gameObject.SetActive(false);
        }

        wBarSprite.fillAmount = pStats.fireWandCooldown / pStats.maxFireWandCooldown;


        // Barra de Vida
        lBarSprite.fillAmount = pStats._pLife / 100;

        // Barra de Calcio
        cBarSprite.fillAmount = pStats._calcio / 100;

        #endregion

        if (Input.GetKeyDown(KeyCode.K))
        {
            SceneManager.LoadScene(5);
        }


        if (isPaused)
        {
            pStats.canShoot = false;
            Time.timeScale = 0f; 
        }
        else
        {
            pStats.canShoot = true;
            Time.timeScale = 1f; 
        }
    }


    private IEnumerator DebugNPCs()
    {
        foreach(NPCs npc in NPCList)
        {
            Debug.Log(npc.name + " Soy del gManager");
        }
        Debug.Log("//////////////////////");
        yield return new WaitForSeconds(5);
        StartCoroutine(DebugNPCs());
    }
}

