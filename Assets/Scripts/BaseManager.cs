using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseManager : MonoBehaviour
{
    [SerializeField] protected float _health = 100f;
    [SerializeField] protected float _maxHealth = 100f;
    [SerializeField] protected Text _healthtext;

    protected virtual void Start()
    {
        updateHealthText();

    }

    public abstract void TakeTurn();
    protected abstract void EndTurn();

    public void updateHealthText()
    {
        if (_healthtext !=null)
        {
            _healthtext.text = "HP: "+_health.ToString("n0");
        }
    }

    public void DealDamage(float damage)
    {
        _health = Mathf.Max(_health-damage,0);
        if (_health <= 0)
        {
            _health = 0;
            Debug.Log("Someone died!");
        }
        updateHealthText();
    }

    public void Heal(float heal)
    {
        _health = Mathf.Min(_health+heal, _maxHealth);
        updateHealthText();
    }

}
