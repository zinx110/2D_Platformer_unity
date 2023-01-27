
using UnityEngine;
using UnityEngine.UI;

public class WaveUI : MonoBehaviour
{

    [SerializeField]
    WaveSpawner spawnner;

    [SerializeField]
    Animator waveAnimator;

    [SerializeField]
    Text waveCountdownText;

    [SerializeField]
    Text waveNumberText;

    private WaveSpawner.SpawnState previousState;


    void Start()
    {
        if (spawnner == null)
        {
            Debug.LogError("No spawnner referenced");
            this.enabled = false;
        }
        if (waveAnimator == null)
        {
            Debug.LogError("No waveAnimator referenced");
            this.enabled = false;
        }
        if (waveCountdownText == null)
        {
            Debug.LogError("No waveCountdownField referenced");
            this.enabled = false;
        }
        if (waveNumberText == null)
        {
            Debug.LogError("No waveNumberText referenced");
            this.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (spawnner.State)
        {
            case WaveSpawner.SpawnState.COUNTING:
                UpdateCountingDownUI();
                break;
            case WaveSpawner.SpawnState.SPAWNING:
                SpawningUI();
                break;
            case WaveSpawner.SpawnState.WAITING:
                break;
        }
        previousState = spawnner.State;
    }


    void UpdateCountingDownUI()
    {
        if (previousState != WaveSpawner.SpawnState.COUNTING)
        {
            waveAnimator.SetBool("WaveIncoming", false);
            waveAnimator.SetBool("WaveCountDown", true);
        }
        waveCountdownText.text = ((int)spawnner.WaveCountdown).ToString();
    }
    void SpawningUI()
    {
        if (previousState != WaveSpawner.SpawnState.SPAWNING)
        {
            waveAnimator.SetBool("WaveCountDown", false);
            waveAnimator.SetBool("WaveIncoming", true);
            waveNumberText.text = spawnner.NextWave.ToString();
        }
    }
}
