using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : BaseManager
{
    public AIManager _aiManager;
    [SerializeField] protected CanvasGroup _buttonGroup;
    protected override void Start()
    {
        base.Start();
        _aiManager = GetComponent<AIManager>();
        if (_aiManager == null)//check if AIManager is attached
        {
            Debug.LogError("AIManager not found");
        }
        if (_buttonGroup == null)//check if _buttonGroup is attached
        {
            Debug.LogError("Canvas _buttonGroup not attached");
        }
    }

    public override void TakeTurn()//allow buttons
    {
        _buttonGroup.interactable = true;
    }

    protected override void EndTurn()//hide buttons
    {
        _buttonGroup.interactable = false;
        _aiManager.TakeTurn();
    }

    public void Splash()//attack type
    {
        Debug.Log("You use splash!");
        _aiManager.DealDamage(30);
        EndTurn();
    }
    public void IronTail()//attack type
    {
        Debug.Log("You use iron tail!");
        _aiManager.DealDamage(10);
        EndTurn();
    }
    public void Rest()//action type
    {
        Debug.Log("You rested!");
        Heal(30);
        EndTurn();
    }
    public void SelfDestruct()//attack type
    {
        Debug.Log("You self destructed!");
        DealDamage(_maxHealth);
        _aiManager.DealDamage(80);
        EndTurn();
    }
}
