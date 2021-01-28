using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSession : MonoBehaviour
{
    //config params
    [Range(0.1f , 10f)] [SerializeField] float gameSpeed;
    [SerializeField] int pointsPerBlockDestroyed = 10;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] bool isAutoPlayEnabled;
    [SerializeField] GameObject[] hearts;
    [SerializeField] int hearts_number = 3;

    // state variables
    [SerializeField] int currentScore =0;

    //cached ref
    LoseCollider lose_collider;

    private void Awake()
    {
        int gameStatusCount = FindObjectsOfType<GameSession>().Length; // DIFFERENT THAN FINDOBEJCTOFTYPE   ( THERE IS 'S' ) 
        if( gameStatusCount > 1)
        {
            gameObject.SetActive(false); // Bu kod olmazsa Destroy olana kadar bir anlık 2 tane gamestatus oluyor.
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    private void Start()
    {
        //scoreText = FindObjectOfType<TextMeshProUGUI>();
        scoreText.text = currentScore.ToString();

        lose_collider = FindObjectOfType<LoseCollider>(); /// olmazsa sil

    }



    void Update()
    {
        Time.timeScale = gameSpeed;
        
    }
     public void AddToScore()
    {
        
        currentScore += pointsPerBlockDestroyed;
        scoreText.text = currentScore.ToString();
    } 
    public void DestroyItself()
    {
        Destroy(gameObject);
    }
    public bool IsAutoPlayEnabled()
    {
        return isAutoPlayEnabled;
    }
   
    public void DecreaseHeart()
    {
        hearts_number--;
        Destroy(hearts[hearts_number]);
    }
    public int GetHeart()
    {
        return hearts_number;
    }
   
    
}
