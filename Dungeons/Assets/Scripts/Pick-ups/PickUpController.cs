using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// to see in inspector
[System.Serializable]

public class Item
{
    public string name;
    public string desc;
    public Sprite item_sprite;
}

public class PickUpController : MonoBehaviour
{
    public Item item;

    public float health_multiplier;
    public float speed_multiplier;
    public float fire_multiplier;
    public float damage_multiplier;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = item.item_sprite;
        Destroy(GetComponent<PolygonCollider2D>());
        gameObject.AddComponent<PolygonCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            //PlayerController.treasure_amount++;
            GameController.RecieveHealth(health_multiplier);
            GameController.MoveSpeedMultiplier(speed_multiplier);
            GameController.FireRateMultiplier(fire_multiplier);
            GameController.DamageMultiplier(damage_multiplier);

            Destroy(gameObject);
        }
    }
}
