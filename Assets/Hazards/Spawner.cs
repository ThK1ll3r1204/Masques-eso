using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject goblins;
    float timer;
    [SerializeField] float timermax;
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            GameObject Goblins = Instantiate(goblins, transform.position, Quaternion.identity);
            timer = timermax;
        }
    }
}
