using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{
    [HideInInspector] public float deathWait = 0.0f;
    private void Update()
    {
        if (deathWait > 0.0f)
        {
            deathWait -= Time.deltaTime;
            if (deathWait <= 0.0f)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }

        }
    }
}
