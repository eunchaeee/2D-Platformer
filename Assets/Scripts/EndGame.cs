using System;
using Unity.VisualScripting;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            gameManager.FinishGame();
        } 
    }
}
