using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCombat : MonoBehaviour
{
    [SerializeField] GameObject _combatCanvas;
    private void OnCollisionEnter2D(Collision2D collision)
    {

        bananaScript aiMove = collision.gameObject.GetComponent<bananaScript>();

        if (aiMove == null)
        {
            return;
        }
        Debug.Log("AI Collision");

        _combatCanvas.SetActive(true);
        Time.timeScale = 0;
    }
}
