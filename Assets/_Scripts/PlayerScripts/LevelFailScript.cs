using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFailScript : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag=="Player" && this.gameObject.tag=="LevelFail"){

            if(GameManager.instance.playerLives==0){
                
            GameManager.instance.CheckLevelFailorSuccessfull(true);

            }else{
                Debug.Log(GameManager.instance.playerLives);
                GameManager.instance.playerLives= GameManager.instance.playerLives-1;
                Debug.Log(GameManager.instance.playerLives);

                GameManager.onChangeHealth?.Invoke();
            }
        }
        if(other.gameObject.tag=="Player" && this.gameObject.tag=="LevelComplete"){

            GameManager.instance.CheckLevelFailorSuccessfull(false);
        }
    }
}
