using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [System.Serializable]
    public class PlayerStats
    {
        public int maxHealth = 100;
        private int _currentHealth;
        public int currentHealth
        {
            get { return _currentHealth; }
            set { _currentHealth = Mathf.Clamp(value, 0, maxHealth); }
        }

        public void Init()
        {
            currentHealth = maxHealth;
        }



    }

    [SerializeField]
    private StatusIndicator statusIndicator;

    private void Start()
    {
        stats.Init();
        if (statusIndicator == null)
        {
            Debug.LogWarning("No Status indicator referrenced on player.");
        }
        else
        {
            statusIndicator.SetHealth(stats.currentHealth, stats.maxHealth);
        }
       
    }




    public int fallBounddary = -20;

    public PlayerStats stats = new PlayerStats();

    public void DamagePlayer(int damage)
    {
        stats.currentHealth -= damage;
        if (stats.currentHealth <= 0)
        {
            GameMaster.KillPlayer(this);
        }
        if (statusIndicator == null)
        {
            Debug.LogWarning("No Status indicator referrenced on player.");
        }
        else
        {
            statusIndicator.SetHealth(stats.currentHealth, stats.maxHealth);
        }
    }

    private void Update()
    {
        if (transform.position.y <= fallBounddary)
        {
            DamagePlayer(999);
        }

    }
}
