using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoardGameController : MonoBehaviour
{
    [SerializeField]
    private GameObject bigCard;
    [SerializeField]
    private GameObject smallCard;

    [SerializeField]
    private Sprite[] staticSprites;
    [SerializeField]
    private List<Sprite> sprites;
    
    void Start()
    {
        Transform parent = GameObject.Find("Board").GetComponent<Transform>();
        GameObject newCard;
        SpriteRenderer renderer;
        float x = -3.666667f, y = 0.1000013f, z = -3.666667f;
        for (int bottomIndex = 0; bottomIndex < 5; ++bottomIndex)
        {
            if (bottomIndex == 0 || bottomIndex == 4)
            {
                newCard = Instantiate(bigCard, parent);
                renderer = newCard.GetComponent<SpriteRenderer>();
                if (bottomIndex == 0)
                {
                    renderer.sprite = staticSprites[1];
                }
                else if (bottomIndex == 4)
                {
                    renderer.sprite = staticSprites[0];
                }

                newCard.GetComponent<Transform>().localPosition = new Vector3(x, y, z);
                x += 2;
            }
            else
            {
                newCard = Instantiate(smallCard, parent);
                renderer = newCard.GetComponent<SpriteRenderer>();
                renderer.sprite = bottomIndex == 3 ? staticSprites[3] : GetAndRemoveRandom(sprites);
                newCard.GetComponent<Transform>().localPosition = new Vector3(x, y, z);
                x += bottomIndex == 3 ? 2 : 1.666667f;
            }
        }

        x = -3.666667f;
        z = 3.666667f;
        
        for (int topIndex = 0; topIndex < 5; ++topIndex)
        {
            Transform newTransform;
            if (topIndex == 0 || topIndex == 4)
            {
                newCard = Instantiate(bigCard, parent);
                renderer = newCard.GetComponent<SpriteRenderer>();
                if (topIndex == 0)
                {
                    renderer.sprite = staticSprites[2];
                }
                else if (topIndex == 4)
                {
                    renderer.sprite = staticSprites[1];
                }

                newTransform = newCard.GetComponent<Transform>();
                newTransform.localPosition = new Vector3(x, y, z);
                x += 2;
            }
            else
            {
                newCard = Instantiate(smallCard, parent);
                renderer = newCard.GetComponent<SpriteRenderer>();
                renderer.sprite = GetAndRemoveRandom(sprites);
                newTransform = newCard.GetComponent<Transform>();
                newTransform.localPosition = new Vector3(x, y, z);
                x += topIndex == 3 ? 2 : 1.666667f;
            }
            newTransform.Rotate(new Vector3(180, 0, 0));
        }
        
        x = -3.666667f;
        z = -1.55f;
        
        for (int leftIndex = 1; leftIndex <= 3; ++leftIndex)
        {
            Transform newTransform;
            newCard = Instantiate(smallCard, parent);
            renderer = newCard.GetComponent<SpriteRenderer>();
            renderer.sprite = GetAndRemoveRandom(sprites);
            newTransform = newCard.GetComponent<Transform>();
            newTransform.localPosition = new Vector3(x, y, z);
            z += 1.55f;
            newTransform.Rotate(new Vector3(0, 0, -90));
        }
        
        x = 3.666667f;
        z = -1.55f;
        
        for (int rightIndex = 1; rightIndex <= 3; ++rightIndex)
        {
            Transform newTransform;
            newCard = Instantiate(smallCard, parent);
            renderer = newCard.GetComponent<SpriteRenderer>();
            renderer.sprite = GetAndRemoveRandom(sprites);
            newTransform = newCard.GetComponent<Transform>();
            newTransform.localPosition = new Vector3(x, y, z);
            z += 1.55f;
            newTransform.Rotate(new Vector3(0, 0, 90));
        }
    }

    private Sprite GetAndRemoveRandom(List<Sprite> sprites)
    {
        int randomIndex = Random.Range(0, sprites.Count - 1);
        return GetAndRemove(sprites, randomIndex);
    }

    private Sprite GetAndRemove(List<Sprite> sprites, int index)
    {
        Sprite sprite = sprites[index];
        sprites.RemoveAt(index);
        return sprite;
    }
}
