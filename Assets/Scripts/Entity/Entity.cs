using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

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
    private float interest;
    private int _placesToGo;
    private AnimatedTranslation _translation;
    private bool _isInstanced = false;
    
    private Text _description;
    private Text _who;

    private void Start()
    {
        _description = GameObject.Find("Description").GetComponent<Text>();
        _who = GameObject.Find("Who").GetComponent<Text>();
    }

    private void Instance()
    {
        _occupation = RandomEnumValue<Occupation>();
        _salary = OccupationData.Salary[(int) _occupation];
        _debt = OccupationData.Debt[(int) _occupation];
        _creditScore = OccupationData.InitialCreditScore[(int) _occupation];
        _car = RandomEnumValue<Car>();
        _isInstanced = true;
    }

    protected void Update()
    {
        if (!_isInstanced)
        {
            Instance();
        }
        if (_description != null)
        {
            Debug.Log("Occupation: " + World.Instance.CurrentPlayer + " | " + _occupation + " | " + this);
            if (World.Instance.CurrentPlayer is Player && this is Player)
            {
                _description.text = "Occupation: " + _occupation + "\nSalary: " + _salary+ "\nCar: " + _car.ToString().Replace("_", " ") + "\nBalance: " + _balance + 
                                    "\nDebt: " + _debt + "\nCredit score: " + _creditScore + "";   
            }
            else if (World.Instance.CurrentPlayer is Computer && this is Computer)
            {
                _description.text = "Occupation: " + _occupation + "\nSalary: " + _salary+ "\nCar: " + _car.ToString().Replace("_", " ") + "\nBalance: " + _balance + 
                                    "\nDebt: " + _debt + "\nCredit score: " + _creditScore + "";
            }
        }

        if (_who != null)
        {
            if (World.Instance.CurrentPlayer is Player)
            {
                _who.text = "You";
            }
            else if (World.Instance.CurrentPlayer is Computer)
            {
                _who.text = "Computer";
            }   
        }

        if (_placesToGo > 0)
        {
            if (_translation != null)
            {
                _translation.Execute(then: new Then(() =>
                {
                    _placesToGo--;
                    _currentCardIndex++;
                    if (_currentCardIndex >= 16)
                    {
                        _currentCardIndex = 0;
                    }
                    if (_currentCardIndex % 4 == 0)
                    {
                        _handleRotation();
                    }

                    if (_placesToGo > 0)
                    {
                        _translation = new AnimatedTranslation(GetComponent<Transform>(), GetNextLocation(), 5);
                        _handlePassiveEffect(World.Instance.Cards[_currentCardIndex]);
                    }
                    else if (_placesToGo == 0)
                    {
                        _translation = null;
                        _handleFullEffect(World.Instance.Cards[_currentCardIndex]);
                        if (World.Instance.CurrentPlayer is Player)
                        {
                            World.Instance.CurrentPlayer = RollButtonScript.Computer;
                            Thread.Sleep(Random.Range(500, 2000));
                            RollButtonScript.RollDice();
                        } 
                        else if (World.Instance.CurrentPlayer is Computer)
                        {
                            World.Instance.CurrentPlayer = RollButtonScript.Player;
                        }
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

        if (card.Type == CardType.vacation)
        {
            _debt += 1500;
        }

        if (card.Type == CardType.part_time_job)
        {
            _temporarySalaryIncrease = 10000;
            _temporarySalaryMonthsLeft = 2;
        }

        if (card.Type == CardType.chance)
        {
            int ran = Random.Range(1, 9);
        }
        _handlePassiveEffect(card);
    }

    private void SetInterest()
    {
        interest = (850 - _creditScore) * .1f + 2;
    }

    private void _handlePassiveEffect(CardObject card)
    {
        // to get type: card.Type
        // example:
        // if (card.Type == CardType.end_of_month)
        if (card.Type == CardType.pay_day)
        {
            _balance += (_salary * _temporarySalaryMultiplier + _temporarySalaryIncrease)/26f;
        }

        if (card.Type == CardType.end_of_month)
        {
            if (_debt * interest - _debt > _balance)
            {
                _creditScore -= 5;
            }
            else if (_debt * interest - _debt < _balance)
            {
                _creditScore += 5;
            }

            _debt -= _balance;
            _debt *= interest;
            _balance = 0;
            SetInterest();
            if (_fired)
            {
                _fired = false;
            }

            if (_temporarySalaryMonthsLeft > 0)
            {
                _temporarySalaryMonthsLeft--;
                if (_temporarySalaryMonthsLeft == 0)
                {
                    _temporarySalaryIncrease = 0;
                }
            }

            if (_temporarySalaryMultiplier > 1.0)
            {
                _temporarySalaryMultiplier = 1.0f;
            }
            else if (_temporarySalaryMultiplierMonthsLeft == 1)
            {
                _temporarySalaryMultiplier = 1.1f;
                _temporarySalaryMultiplierMonthsLeft = 0;
            }
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
    
    private static T RandomEnumValue<T> ()
    {
        var v = Enum.GetValues (typeof (T));
        return (T) v.GetValue (new System.Random().Next(v.Length));
    }
}