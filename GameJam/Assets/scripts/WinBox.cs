using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinBox : MonoBehaviour
{
    [SerializeField, Min(0)] private int NextSceneIndex;
    [SerializeField] private string playerTag = "Player";
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
        {
            Debug.Log("Win, change scene");
            UnityEngine.SceneManagement.SceneManager.LoadScene(NextSceneIndex);
        }
    }
}
