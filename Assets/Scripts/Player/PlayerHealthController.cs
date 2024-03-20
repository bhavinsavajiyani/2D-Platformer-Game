using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthController : MonoBehaviour
{
    public int health { get; private set; }
    [SerializeField] private Image[] hearts;

    private void Awake()
    {
        health = 3;
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Image img in hearts)
        {
            img.color = Color.black;
        }

        for(int i = 0; i < health; i++)
        {
            hearts[i].color = Color.white;
        }
    }

    public int LifeLost()
    {
        StartCoroutine(GetHurt());
        health--;
        return health;
    }

    private IEnumerator GetHurt()
    {
        Physics2D.IgnoreLayerCollision(6, 7);
        yield return new WaitForSeconds(1.5f);
        Physics2D.IgnoreLayerCollision(6, 7, false);
    }
}
