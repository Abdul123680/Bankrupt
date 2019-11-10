using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : Singleton<World>
{
    [NonSerialized]
    public CardObject[] Cards = new CardObject[16];

    [NonSerialized]
    public Entity CurrentPlayer;

    private void Start()
    {
        GameObject playerObj = GameObject.Find("Player");
        CurrentPlayer = playerObj.GetComponent<Player>();
    }
}
