using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFailScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag=="Player" && this.gameObject.tag=="LevelFail"){

            GameManager.instance.CheckLevelFailorSuccessfull(true);
        }
        if(other.gameObject.tag=="Player" && this.gameObject.tag=="LevelComplete"){

            GameManager.instance.CheckLevelFailorSuccessfull(false);
        }
    }
}
