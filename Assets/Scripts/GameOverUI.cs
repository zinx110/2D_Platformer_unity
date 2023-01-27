
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOverUI : MonoBehaviour
{public void Quit()
    {
        Debug.Log("Appliation Quit");
        Application.Quit();
        
    }
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);        
    }
}
