using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerStats : MonoBehaviour
{
    //Luis
    [SerializeField] Movement pMov;
    [SerializeField] public float _pLife;
    public bool enemyKilled;
    public bool isDead;
    public float _calcio;

    [SerializeField] GameObject bow;
    [SerializeField] GameObject fireWand;

    public bool wpnIsBow;

    private void Start()
    {

        pMov = GetComponent<Movement>();
        _pLife = 100f;
        _calcio = 100f;
        enemyKilled = false;
        wpnIsBow = true;
    }
    

    void Update()
    {
        //El calcio baja
        _calcio -= Time.deltaTime;

        //Limites del calcio
        _calcio = Mathf.Clamp(_calcio, 0, 100f);

        if (enemyKilled)
        {
            _calcio += 20;
            enemyKilled = false;
        }

        if (Input.GetKeyUp(KeyCode.Q))
        {
            if (!wpnIsBow)
                wpnIsBow = true;
            else
                wpnIsBow = false;
        }

        if(wpnIsBow)
        {
            bow.SetActive(true);
            fireWand.SetActive(false);
        }
        else
        {
            bow.SetActive(false);
            fireWand.SetActive(true);
        }
       

    }

    public void TakeDamage(float damage)
    {
        _pLife += damage;
        // Muerto? xd
        if (_pLife <= 0f)
        {
            Die();
        }

       
    }
    public void Die()
    {
        FindObjectOfType<AudioManager>().Play("Muerte");
        pMov._rb.simulated = false;
        //pMov._coll.enabled = false;
        //Efectivamente murio
        Debug.Log("Muerto");
        isDead = true;
        GameObject.Find("Guns").SetActive(false);
        
    }

    
}
