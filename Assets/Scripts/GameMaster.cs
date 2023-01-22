using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster gm;
    private void Awake()
    {
        if (gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        }
    }
    public Transform playerPrefab;
    public Transform spawnPoint;
    public int respawnDelayTime = 4;
    public AudioSource respawnAudio;


    public CameraShake cameraShake;


    public Transform spawnPrefab;



    private void Start()
    {
        if (cameraShake == null)
        {
            Debug.LogError("No camera shake referrence in GameMaster");
        }
    }


    public IEnumerator RespawnPlayer()
    {
        respawnAudio.Play();
        yield return new WaitForSeconds(respawnDelayTime);

        Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        Transform clone = Instantiate(spawnPrefab, spawnPoint.position, spawnPoint.rotation) as Transform;
        Destroy(clone.gameObject, 3f);

    }
    public static void KillPlayer(Player player)
    {

        Destroy(player.gameObject);
        gm.StartCoroutine(gm.RespawnPlayer());
    }

    public static void KillEnemy(Enemy _enemy)
    {
        gm._killEnemy(_enemy);
    }



    public void _killEnemy(Enemy _enemy)
    {
       Transform clone= Instantiate(_enemy.deathParticles, _enemy.transform.position, Quaternion.identity) as Transform;
        Destroy(_enemy.gameObject);
        Destroy(clone.gameObject, 5f);
        cameraShake.ShakeCamera(_enemy.shakeAmount,_enemy.shakeDuration);
    }
}
