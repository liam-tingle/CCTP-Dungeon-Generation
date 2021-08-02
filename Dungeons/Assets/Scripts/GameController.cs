using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    static float health = 6;
    static int max_health = 6;

    static float move_speed = 5f;
    static float fire_rate = 2f;
    static float bullet_damage = 0.5f;

    public static float Health { get => health; set => health = value; }
    public static int MaxHealth { get => max_health; set => max_health = value; }

    public static float MoveSpeed { get => move_speed; set => move_speed = value; }
    public static float FireRate { get => fire_rate; set => fire_rate = value; }
    public static float BulletDamage { get => bullet_damage; set => bullet_damage = value; }

    public Text life_counter;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    void Update()
    {
        life_counter.text = "Lives: " + health;
    }

    public static void TakeDamage(int damage)
    {
        Health -= damage;

        if(Health <= 0)
        {
            KillPlayer();
        }
    }

    public static void RecieveHealth(float heal_amount)
    {
        health = Mathf.Min(max_health, health + heal_amount);
    }

    public static void MoveSpeedMultiplier(float speed)
    {
        move_speed += speed;
    }

    public static void FireRateMultiplier(float new_rate)
    {
        fire_rate -= new_rate;
    }

    public static void DamageMultiplier(float incereased_damage)
    {
        bullet_damage += incereased_damage;
    }

    private static void KillPlayer()
    {

    }
}
