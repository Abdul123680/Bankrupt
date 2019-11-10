using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    private int _currentCardIndex;
    protected Direction direction = Direction.west;

    private Occupation _occupation;
    private Car _car;
    private bool _fired;
    private float _temporarySalaryMultiplier = 1.0f;
    private int _temporarySalaryIncrease;
    private int _temporarySalaryMultiplierMonthsLeft;
    private int _temporarySalaryMonthsLeft;
    private float _balance;
    private float _debt;
    private float _salary;
    private int _creditScore;
    private int _monthlyPayments; // remember car payments

    private int _placesToGo;
    private AnimatedTranslation _translation;

    protected void Update()
    {
        if (_placesToGo > 0)
        {
            if (_translation != null)
            {
                _translation.Execute(then: new Then(() =>
                {
                    _placesToGo--;
                    _currentCardIndex++;
                    if (_currentCardIndex % 4 == 0)
                    {
                        _handleRotation();
                    }
                    if (_placesToGo > 0)
                    {
                        _translation = new AnimatedTranslation(GetComponent<Transform>(), GetNextLocation(), 5);
                        _handlePassiveEffect(World.Instance.Cards[_currentCardIndex]);
                    }
                    else
                    {
                        _translation = null;
                        _handleFullEffect(World.Instance.Cards[_currentCardIndex]);
                    }
                }));
            }
        }
    }
    
    public void Move(int places)
    {
        _placesToGo = places;
        _translation = new AnimatedTranslation(GetComponent<Transform>(), GetNextLocation(), 5);
    }

    protected abstract Vector3 GetNextLocation();

    private void _handleFullEffect(CardObject card)
    {
        // to get type: card.Type
        // example:
        // if (card.Type == CardType.end_of_month)
        if (card.Type == CardType.lottery_winner)
        {
            _balance += 5000;
        }
    }

    private void _handlePassiveEffect(CardObject card)
    {
        // to get type: card.Type
        // example:
        // if (card.Type == CardType.end_of_month)
        if (card.Type == CardType.pay_day)
        {
            _balance += _salary  * _temporarySalaryMultiplier + _temporarySalaryIncrease;
        }

        if (card.Type == CardType.grocery_store)
        {
            _debt += 100;
        }

        if (card.Type == CardType.shop_at_mall)
        {
            _debt += 200;
        }

        if (card.Type == CardType.income_tax)
        {
            _debt += _salary * .15f;
        }

        if (card.Type == CardType.fired)
        {
            _fired = true;
        }
    }

    private void _handleRotation()
    {
        if (direction == Direction.west) 
        { 
            direction = Direction.north;
        }
        else if (direction == Direction.north) 
        { 
            direction = Direction.east;
        }
        else if (direction == Direction.east)
        { 
            direction = Direction.south;
        }
        else
        {
            direction = Direction.west;
        }
    }
}
