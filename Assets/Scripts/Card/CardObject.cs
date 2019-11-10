using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardObject : MonoBehaviour
{
    public CardType Type { get; set; }

    public CardObject(CardType type)
    {
        Type = type;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static CardType getCardType(String spriteName)
    {
        if (spriteName.Contains("corner_1"))
        {
            return CardType.end_of_month;
        }
        else if (spriteName.Contains("corner_2"))
        {
            return CardType.stocks;
        }
        else if (spriteName.Contains("corner_3"))
        {
            return CardType.gambling;
        }
        else if (spriteName.Contains("misc_card_1"))
        {
            return CardType.car_dealer;
        }
        else if (spriteName.Contains("misc_card_2"))
        {
            return CardType.chance;
        }
        else if (spriteName.Contains("money_card_1"))
        {
            return CardType.pay_day;
        }
        else if (spriteName.Contains("money_card_2"))
        {
            return CardType.lottery_winner;
        }
        else if (spriteName.Contains("money_card_3"))
        {
            return CardType.part_time_job;
        }
        else if (spriteName.Contains("spend_card_1"))
        {
            return CardType.grocery_store;
        }
        else if (spriteName.Contains("spend_card_2"))
        {
            return CardType.shop_at_mall;
        }
        else if (spriteName.Contains("spend_card_3"))
        {
            return CardType.income_tax;
        }
        else if (spriteName.Contains("spend_card_4"))
        {
            return CardType.fired;
        }
        else
        {
            return CardType.vacation;
        }
    }
}
