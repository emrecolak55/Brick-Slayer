using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] float screenWidthUnits = 16f;
    [SerializeField] float minX = 1f;
    [SerializeField] float maxX = 15f;

    //cache references
    GameSession gameSession;
    Ball myball;


    // Start is called before the first frame update
    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        myball = FindObjectOfType<Ball>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Input.mousePosition.x / Screen.width *screenWidthUnits); // Its gonna give values between 0 and 1
        Vector2 paddlePos = new Vector2(Input.mousePosition.x / Screen.width * screenWidthUnits, transform.position.y);
        float mouseXpos = Mathf.Clamp(GetXpos(), minX, maxX);
        paddlePos.x = mouseXpos;
        transform.position = paddlePos;
    }

    private float GetXpos()
    {
        if (gameSession.IsAutoPlayEnabled())
        {
            return myball.transform.position.x;
        }
        else
        {
            return Input.mousePosition.x / Screen.width * screenWidthUnits;
        }
    }

}
