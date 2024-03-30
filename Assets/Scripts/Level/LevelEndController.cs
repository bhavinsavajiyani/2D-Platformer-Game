using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEndController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerController>() != null)
        {
            // SceneManager.LoadScene(nextSceneToLoad);
            SoundManager.Instance.PlaySound(SoundType.LevelComplete);
            LevelManager.Instance.LevelComplete();
        }
    }
}
