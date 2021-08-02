using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    Idle,
    Follow,
    Die,
    Attack,
};

public enum EnemyType
{
    Melee,
    Ranged,
};

public class EnemyController : MonoBehaviour
{
    GameObject Player;

    public GameObject projectile_prefab;

    public EnemyState current_state = EnemyState.Idle;
    public EnemyType enemy_type;

    public float range;
    public float move_speed;
    public float attack_range;
    public float attack_delay;

    private bool chooseDir = false;
    //private bool is_dead = false;
    private bool has_attacked = false;

    private Vector3 random_direction;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        switch(current_state)
        {
            case (EnemyState.Idle):
                Idle();
                break;
            case (EnemyState.Follow):
                Follow();
                break;
            case (EnemyState.Die):
                Die();
                break;
            case (EnemyState.Attack):
                Attack();
                break;
        }

        if(IsInRange(range) && current_state != EnemyState.Die)
        {
            current_state = EnemyState.Follow;
        }
        else if(!IsInRange(range) && current_state != EnemyState.Die)
        {
            current_state = EnemyState.Idle;
        }

        if(Vector3.Distance(transform.position, Player.transform.position) <= attack_range)
        {
            current_state = EnemyState.Attack;
        }
    }

    IEnumerator ChooseDirection()
    {
        chooseDir = true;
        yield return new WaitForSeconds(Random.Range(2f, 8f));
        random_direction = new Vector3(0, 0, Random.Range(0, 360));
        Quaternion next_rotation = Quaternion.Euler(random_direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, next_rotation, Random.Range(0.5f, 2.5f));
        chooseDir = false;
    }

    bool IsInRange(float range)
    {
        return Vector3.Distance(transform.position, Player.transform.position) <= range;
    }

    void Idle()
    {
        if(!chooseDir)
        {
            StartCoroutine(ChooseDirection());
        }

        transform.position += -transform.right * move_speed * Time.deltaTime;

        if(IsInRange(range))
        {
            current_state = EnemyState.Follow;
        }
    }

    void Follow()
    {
        transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, move_speed * Time.deltaTime);
    }

    void Attack()
    {
        if(!has_attacked)
        {
            //GameController.TakeDamage(1);
            //StartCoroutine(CoolDown());

        switch(enemy_type)
            {
                case (EnemyType.Melee):
                    GameController.TakeDamage(1);
                    StartCoroutine(CoolDown());
                    break;
                case (EnemyType.Ranged):
                    GameObject e_projectile = Instantiate(projectile_prefab, transform.position, Quaternion.identity) as GameObject;
                    e_projectile.GetComponent<ProjectileController>().GetPlayer(Player.transform);
                    e_projectile.AddComponent<Rigidbody2D>().gravityScale = 0;
                    e_projectile.GetComponent<ProjectileController>().isEnemyProjectile = true;
                    StartCoroutine(CoolDown());
                    break;
            }
        }
        
    }

    IEnumerator CoolDown()
    {
        has_attacked = true;
        yield return new WaitForSeconds(attack_delay);
        has_attacked = false;
    }

    public void Die()
    {
        Destroy(gameObject);
        //is_dead = true;
    }
}
