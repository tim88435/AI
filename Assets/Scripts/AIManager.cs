using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : BaseManager
{
    public PlayerManager _playerManager;
    public enum State
    {
        HighHP,
        LowHP,
        Dead
    }
    public State currentState;
    protected PlayerManager playerManager;
    [SerializeField] protected Animator _anim;

    protected override void Start()
    {
        base.Start();
        _playerManager = GetComponent<PlayerManager>();
        if (_playerManager == null)
        {
            Debug.Log("Playermanager not found");
        }
    }

    void HighHPState()
    {
        if (_health<40)
        {
            currentState = State.LowHP;
            LowHPState();
            return;
        }
        int randomattack = Random.Range(0,10);

        switch (randomattack)
        {
            case int i when i >= 0 & i < 1:
                Splash();
                break;
            case int i when i >= 1 & i < 8:
                IronTail();
                break;
            case int i when i >= 8 & i <= 9:
                SelfDestruct();
                break;
        }
    }

    void LowHPState()
    {
        if (_health >= 40)
        {
            currentState = State.HighHP;
            HighHPState();
            return;
        }
        else if (_health <= 0)
        {
            currentState = State.Dead;
            DeadState();
            return;
        }
        int randomattack = Random.Range(0, 10);

        switch (randomattack)
        {
            case int i when i >= 0 & i < 1:
                Splash();
                break;
            case int i when i >= 1 & i < 4:
                IronTail();
                break;
            case int i when i >= 4 & i < 7:
                Rest();
                break;
            case int i when i >= 7 & i <= 9:
                SelfDestruct();
                break;
        }
    }



    void DeadState()
    {
        Debug.Log("YOU WIN");
    }

    public override void TakeTurn()
    {
        StartCoroutine(WaitStart());
        /*switch (currentState)
        {
            case State.HighHP:
                HighHPState();
                break;
            case State.LowHP:
                LowHPState();
                break;
            case State.Dead:
                DeadState();
                break;
            default:
                break;
        }*/
    }
    private IEnumerator WaitStart()
    {
        yield return new WaitForSecondsRealtime(2);
        switch (currentState)
        {
            case State.HighHP:
                HighHPState();
                break;
            case State.LowHP:
                LowHPState();
                break;
            case State.Dead:
                DeadState();
                break;
            default:
                break;
        }
    }

    private IEnumerator WaitEnd()
    {
        yield return new WaitForSecondsRealtime(2);
        _playerManager.TakeTurn();
    }


    protected override void EndTurn()
    {
        StartCoroutine(WaitEnd());
    }

    public void Splash()
    {
        Debug.Log("AI uses splash!");
        _playerManager.DealDamage(30);
        _anim.SetTrigger("Splash");
        EndTurn();
    }
    public void IronTail()
    {
        Debug.Log("AI uses iron tail!");
        _playerManager.DealDamage(10.1f);
        _anim.SetTrigger("Iron Tail");
        EndTurn();
    }
    public void Rest()
    {
        Debug.Log("AI rested!");
        Heal(30);
        _anim.SetTrigger("Rest");
        EndTurn();
    }
    public void SelfDestruct()
    {
        Debug.Log("AI self destructed!");
        DealDamage(_maxHealth);
        _playerManager.DealDamage(80);
        _anim.SetTrigger("Self Destruct");
        EndTurn();
    }






}