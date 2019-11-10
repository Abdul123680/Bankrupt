using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class RollButtonScript : MonoBehaviour
{
    public static Computer Computer;
    public static Player Player;
    private static Text _text;
    
    private void Start()
    {
        Computer = GameObject.Find("Computer").GetComponent<Computer>();
        Player = GameObject.Find("Player").GetComponent<Player>();
        _text = GameObject.Find("RollDescription").GetComponent<Text>();
        _text.enabled = false;
    }

    public void RollDiceClick()
    {
        if (World.Instance.CurrentPlayer is Player)
        {
            RollDice();
        }
    }

    public static void RollDice()
    {
        int moves = Random.Range(1, 6);
        World.Instance.CurrentPlayer.Move(moves);
        if (World.Instance.CurrentPlayer is Player)
        {
            _text.text = "You rolled a " + moves + "!";
            _text.enabled = true;
        }
        else
        {
            _text.enabled = false;
        }
    }
}
