using UnityEngine;
using UnityEngine.UI;



public class LivesCounterUI : MonoBehaviour
{

    private  Text livesText;
    private void Awake()
    {
        livesText = GetComponent<Text>();
    }

    private void Update()
    {
        livesText.text = "LIVES " + GameMaster.RemainingLives.ToString();

    }


}
