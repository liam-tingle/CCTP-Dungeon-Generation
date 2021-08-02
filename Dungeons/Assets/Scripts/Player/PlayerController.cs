using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float move_speed;
    public float projectile_speed;
    public float fire_rate;
    public float projectile_delay;

    public Animator anim;
    public playerCombat combat;
    public AudioSource shout;

    Rigidbody2D m_rb;

    public GameObject projectile;
    //public Text treasure_text;
    //public static int treasure_amount = 0;

    private bool is_moving;
    public bool attacking;
    public bool has_key = false;
    private Vector2 last_move;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        m_rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Collection();
        Projectile();
    }

    void Move()
    {
        is_moving = false;

        if(is_moving == false)
        {
            m_rb.velocity = Vector2.zero;
        }

        if(Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f)
        {
            transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * move_speed * Time.deltaTime, 0f, 0f));
            is_moving = true;
            last_move = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);
        }

        if (Input.GetAxisRaw("Vertical") > 0.5f || Input.GetAxisRaw("Vertical") < -0.5f)
        {
            transform.Translate(new Vector3(0f, Input.GetAxisRaw("Vertical") * move_speed * Time.deltaTime, 0f));
            is_moving = true;
            last_move = new Vector2(0f, Input.GetAxisRaw("Vertical"));
        }

        if(Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
        {
            is_moving = false;
        }

        if(Input.GetButtonDown("Attack") && !attacking)
        {
            attacking = true;
            shout.Play();
            StartCoroutine(Attack());
        }

        anim.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
        anim.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
        anim.SetBool("IsMoving", is_moving);
        anim.SetFloat("LastMoveX", last_move.x);
        anim.SetFloat("LastMoveY", last_move.y);
    }

    IEnumerator Attack()
    {
        anim.SetBool("IsAttacking", true);
        yield return new WaitForSeconds(0.25f);
        anim.SetBool("IsAttacking", false);
        attacking = false;
    }

    void Projectile()
    {
        float horizontal_f = Input.GetAxis("HorizontalFire");
        float vertical_f = Input.GetAxis("VerticalFire");

        if((horizontal_f != 0 || vertical_f != 0) && Time.time > fire_rate + projectile_delay)
        {
            Fire(horizontal_f, vertical_f);
            fire_rate = Time.time;
        }
    }

    void Fire(float x, float y)
    {
        GameObject m_projectile = Instantiate(projectile, transform.position, transform.rotation) as GameObject;
        m_projectile.AddComponent<Rigidbody2D>().gravityScale = 0;
        m_projectile.GetComponent<Rigidbody2D>().velocity = new Vector3(
            (x < 0) ? Mathf.Floor(x) * projectile_speed : Mathf.Ceil(x) * projectile_speed,
            (y < 0) ? Mathf.Floor(y) * projectile_speed : Mathf.Ceil(y) * projectile_speed, 0);
    }

    void Collection()
    {
        //treasure_text.text = "Rupee's: " + treasure_amount;
    }
}
