using UnityEngine.SceneManagement;
using UnityEngine;

public class TitleComponent : MonoBehaviour
{
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            SceneManager.LoadScene("shop");
        }
    }

    void Start()
    {
        GameManager.playerLives = 3;
    }
}