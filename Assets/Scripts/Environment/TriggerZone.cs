using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerZone : MonoBehaviour
{
    private enum Mode
    {
        Win,
        Death
    }
    [SerializeField] private Mode mode = Mode.Win;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player == null) return;
        if (mode == Mode.Win)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            player.Die();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log("no collide :(");
    }

}
