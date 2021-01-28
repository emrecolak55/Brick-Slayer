using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseCollider : MonoBehaviour
{
    // [SerializeField] int loseTimes = 0;

    //cached refs
    SceneLoader sceneLoader;
    GameSession gameSession;
    int hearts;
    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
        gameSession = FindObjectOfType<GameSession>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        hearts = gameSession.GetHeart();
        if ( hearts <= 1)
        {
            SceneManager.LoadScene("GameOver");
        }
        else
        {
            sceneLoader.LoadCurrentScene();
            gameSession.DecreaseHeart();
        }
       
    }
    
   
}
