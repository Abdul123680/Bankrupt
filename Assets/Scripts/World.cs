using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : Singleton<World>
{
    [NonSerialized]
    public CardObject[] Cards = new CardObject[16];
    
    
}
