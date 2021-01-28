using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Block : MonoBehaviour
{
    //config params
    [SerializeField] AudioClip breakSound;
    [SerializeField] GameObject blockSparklesVFX;
    [SerializeField] Sprite[] hitLevels;

    //debug
    [SerializeField] TextMeshProUGUI debugtext;

    //cached reference
    Level level;
    GameSession gameStatus;

    //state vars
     int hitTimes; // Serialized for debug

    private void Start()
    {
        gameObject.tag = "Breakable";
        CountBreakableBlocks();
        gameStatus = FindObjectOfType<GameSession>();
    }

    private void CountBreakableBlocks()
    {
        level = FindObjectOfType<Level>();

        if (gameObject.tag == "Breakable")
        {
            level.CountBlocks();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.tag == "Breakable")
        {
            //debugtext.text = "Inifloop";
            HandleHit();
        }
        
    }
  /*  private void OnTriggerEnter(Collider other)
    {
        //Check to see if the tag on the collider is equal to Enemy
        if (other.tag == "Breakable")
        {
            Debug.Log("Triggered by Enemy");
        }
    }
  */

    private void HandleHit()
    {
        hitTimes++;
        int maxHits = hitLevels.Length + 1;
        if (hitTimes >= maxHits)
        {
           
            DestroyBlock();
        }
        else
        {
            ShowNextHitSprite();
        }
    }

    private void ShowNextHitSprite()
    {
        int spriteIndex = hitTimes - 1;
        if(hitLevels[spriteIndex])
        {
            GetComponent<SpriteRenderer>().sprite = hitLevels[spriteIndex];
        }
        else
        {
            Debug.LogError("Block Sprite element is missing from array" + gameObject.name);
        }
    }

    private void DestroyBlock()
    {
       
        
        TriggerSparklesVFX();
        PlayDestroyBlockSFX();        
        level.BlocksDestroyed();
        gameStatus.AddToScore();
        //gameObject.SetActive(false);
        Destroy(gameObject);
    }

    private void PlayDestroyBlockSFX()
    {
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
    }

    private void TriggerSparklesVFX()
    {
        GameObject sparkles = Instantiate(blockSparklesVFX, transform.position, transform.rotation);
        Destroy(sparkles, 2f);
    }
}
