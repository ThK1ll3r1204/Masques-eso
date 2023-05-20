using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor.UIElements;
using UnityEditor.UI;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] public float _pLife = 100f;
    public bool enemyKilled;
    public float _calcio;
    public Image calcioBarra;
    public Image vidaBarra;

    private void Start()
    {
        calcioBarra = GameObject.Find("CalcioBarra").GetComponent<Image>();
        vidaBarra = GameObject.Find("VidaBarra").GetComponent<Image>();
        enemyKilled = false;
    }

    void Update()
    {
        //El calcio baja
        _calcio -= Time.deltaTime * 2.1f;

        //Limites del calcio
        _calcio = Mathf.Clamp(_calcio, 0, 100f);

        float calcification = _calcio / 100f;
        calcioBarra.fillAmount = calcification;

        float vidavidation = _pLife / 100f;
        vidaBarra.fillAmount = vidavidation;

        if (enemyKilled)
        {
            _calcio += 20;
            enemyKilled = false;
        }

        if (_pLife <= 0f)
        {
            Die();
        }
    }


    public void Die()
    {
        //Efectivamente murio
        Debug.Log("Muerto");
        SceneManager.LoadScene(0);
    }
}
