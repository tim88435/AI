using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCombat : MonoBehaviour
{
    [SerializeField] GameObject _combatCanvas;
    public AIManager _aiManager;
    public PlayerManager _playerManager;
    private void OnCollisionEnter2D(Collision2D collision)//when collides with something
    {

        bananaScript aiMove = collision.gameObject.GetComponent<bananaScript>();//makes aiMove equal to the collided object's bananaScript

        if (aiMove == null)//checks if bananaScript exists
        {
            return;//exit OnCollisionEnter2D
        }
        //Debug.Log("AI Collision");

        _combatCanvas.SetActive(true);//turns on canvas
        Time.timeScale = 0;//stops time
        _aiManager.Heal(100);//heals AI
        _playerManager.Heal(100);//heals player
        _playerManager.TakeTurn();//starts player's turn
        _aiManager.currentState = AIManager.State.HighHP;//sets AI state to HighHP
    }
}
