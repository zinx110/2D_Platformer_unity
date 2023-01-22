using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [System.Serializable]
    public class EnemyStats
    {

        public float startPctHealth = 1f;
        public int maxHealth = 100;
        private int _currentHealth;
        public int currentHealth
        {
            get
            {
                return _currentHealth;
            }
            set
            {
                _currentHealth = Mathf.Clamp(value, 0, maxHealth);
            }
        }


        public int damage = 40;



        public void Init()
        {
            currentHealth = maxHealth;
        }






    }


    public EnemyStats stats = new EnemyStats();

    public float shakeAmount = 0.5f;
    public float shakeDuration = 0.4f;

    public Transform deathParticles;




    [Header("Optional:")]
    [SerializeField]
    private StatusIndicator statusIndicator;



    private void Start()
    {
        stats.Init();
        if (statusIndicator != null)
        {
            statusIndicator.SetHealth(stats.currentHealth, stats.maxHealth);
        }
        if (deathParticles == null)
        {
            Debug.LogError("No death particle referrence on enemy");
        }
    }







    public void DamageEnemy(int damage)
    {
        stats.currentHealth -= damage;
        if (stats.currentHealth <= 0)
        {
            GameMaster.KillEnemy(this);
        }
        if (statusIndicator != null)
        {
            statusIndicator.SetHealth(stats.currentHealth, stats.maxHealth);
        }
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player _player = collision.collider.GetComponent<Player>();
        if (_player != null)
        {
            _player.DamagePlayer(stats.damage);
            DamageEnemy(999);

        }
    }





}
