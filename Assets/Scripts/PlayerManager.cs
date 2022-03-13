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
        if (_aiManager == null)
        {
            Debug.LogError("AIManager not found");
        }
        if (_buttonGroup == null)
        {
            Debug.LogError("Canvas _buttonGroup not attached");
        }
    }

    public override void TakeTurn()
    {
        _buttonGroup.interactable = true;
    }

    protected override void EndTurn()
    {
        _buttonGroup.interactable = false;
        _aiManager.TakeTurn();
    }

    public void Splash()
    {
        Debug.Log("You use splash!");
        _aiManager.DealDamage(30);
        EndTurn();
    }
    public void IronTail()
    {
        Debug.Log("You use iron tail!");
        _aiManager.DealDamage(10.1f);
        EndTurn();
    }
    public void Rest()
    {
        Debug.Log("You rested!");
        Heal(30);
        EndTurn();
    }
    public void SelfDestruct()
    {
        Debug.Log("You self destructed!");
        DealDamage(_maxHealth);
        _aiManager.DealDamage(80);
        EndTurn();
    }
}
