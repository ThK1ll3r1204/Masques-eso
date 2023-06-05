using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerStats : MonoBehaviour
{
    //Luis

    [SerializeField] public float _pLife;
    public bool enemyKilled;
    public bool isDead;
    public float _calcio;

    

    private void Start()
    {
        _pLife = 100f;
        enemyKilled = false;
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
        //Efectivamente murio
        Debug.Log("Muerto");
        isDead = true;
        GameObject.Find("Guns").SetActive(false);
    }

    
}
