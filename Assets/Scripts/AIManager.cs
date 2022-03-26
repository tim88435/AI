using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : BaseManager
{
    
    public enum State // turn-state statemachine for ai
    {
        HighHP,
        LowHP,
        Dead
    }
    public bananaScript _enemyScript;
    public PlayerManager _playerManager;
    public State currentState;
    [SerializeField] protected Animator _anim; // animation reference

    protected override void Start()
    {
        base.Start(); //to basemanager start
        if (_enemyScript == null) //check if enemyscript is attached
        {
            Debug.Log("bananaScript not found");
        }
        _playerManager = GetComponent<PlayerManager>();//check if player manager script is attached
        if (_playerManager == null)
        {
            Debug.Log("Playermanager not found");
        }
    }

    void HighHPState()
    {
        if (_health<40)// checks if low hp
        {
            currentState = State.LowHP;
            LowHPState();
            return;
        }
        int randomattack = Random.Range(0,10);

        switch (randomattack) // attacks when high hp
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
        if (_health >= 40)// checks if high hp
        {
            currentState = State.HighHP;
            HighHPState();
            return;
        }
        else if (_health <= 0)// checks if dead
        {
            currentState = State.Dead;
            Debug.Log("EE");
            DeadState();
            return;
        }
        int randomattack = Random.Range(0, 10);

        switch (randomattack)// attacks when low hp
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
        Debug.Log("YOU WIN");//exit combat
        _enemyScript.EndCombat();// since player needs to be invunerable to getting immidiately back into the main game, turning invunerablity off needs to be outside of canvas
    }

    

    public override void TakeTurn()
    {
        StartCoroutine(WaitStart());//this should not exist, and be replaced by WaitStart() (I'm lazy)
    }
    private IEnumerator WaitStart()//give time for animation to end
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
                Debug.Log("EE");
                DeadState();
                break;
            default:
                break;
        }
    }

    private IEnumerator WaitEnd()//give player time to see what AI has done
    {
        yield return new WaitForSecondsRealtime(2);
        _playerManager.TakeTurn();
    }
    

    protected override void EndTurn()//this should also not exist
    {
        StartCoroutine(WaitEnd());
    }

    public void Splash()//attack type
    {
        Debug.Log("AI uses splash!");
        _playerManager.DealDamage(30);
        _anim.SetTrigger("Splash");
        EndTurn();
    }
    public void IronTail()//attack type
    {
        Debug.Log("AI uses iron tail!");
        _playerManager.DealDamage(10);
        _anim.SetTrigger("Iron Tail");
        EndTurn();
    }
    public void Rest()//action type
    {
        Debug.Log("AI rested!");
        Heal(30);
        _anim.SetTrigger("Rest");
        EndTurn();
    }
    public void SelfDestruct()//attack type
    {
        Debug.Log("AI self destructed!");
        DealDamage(_maxHealth);
        _playerManager.DealDamage(80);
        _anim.SetTrigger("Self Destruct");
        Debug.Log("EE");
        DeadState();
    }






}
