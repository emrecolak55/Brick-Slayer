using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] float velocityX = 1f;
    [SerializeField] float velocityY = 15f;
    [SerializeField] Paddle paddle1;
    [SerializeField] AudioClip[] ballSounds;
    [SerializeField] float randomFactor = 0.2f;
    Vector2 paddleToBallVector;

    bool hasStarted = false;

    // cached references
    AudioSource myAudioSource;
    Rigidbody2D myRigidBody2D;

    // Start is called before the first frame update
    
    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
        paddleToBallVector = transform.position - paddle1.transform.position;
        myRigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if ( !hasStarted)
        {
            LockBallToPaddle();
        }
        
        LaunchOnMouseClick();
    }
    private void LaunchOnMouseClick()
    {
        
        if (Input.GetMouseButtonDown(0) && !hasStarted)
        {
            myRigidBody2D.velocity = new Vector2(velocityX, velocityY);
            hasStarted = true;
        }
            

    }
    private void LockBallToPaddle()
    {
        
        Vector2 paddlepos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlepos + paddleToBallVector;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 velocityTweak;
        if (gameObject.transform.position.y >= 11.5)
        {
            velocityTweak = new Vector2(Random.Range(-randomFactor, 0f), Random.Range(-randomFactor, 0f));
        }
        else
        {
            velocityTweak = new Vector2(Random.Range(0f, randomFactor), Random.Range(0f, randomFactor));
        }
        
        if(hasStarted)
        {
            AudioClip clip = ballSounds[UnityEngine.Random.Range(0, ballSounds.Length)];
            myAudioSource.PlayOneShot(clip);
            myRigidBody2D.velocity += velocityTweak; // ? ? ? 
        }        
    }
}
