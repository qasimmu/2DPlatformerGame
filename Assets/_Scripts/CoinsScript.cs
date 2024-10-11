using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsScript : MonoBehaviour
{


void OnTriggerEnter2D(Collider2D other)
{
                if(other.gameObject.CompareTag("Player")){
            Debug.Log("Coins hitted");
            GameManager.instance.CoinCollected();
            this.gameObject.GetComponent<Animator>().Play("CollectedCoin");
            Invoke("DisableNow",0.10f);
        }
}
public void DisableNow(){
            this.gameObject.SetActive(false);

}
}
