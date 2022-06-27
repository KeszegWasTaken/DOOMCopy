using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    private Player player;

    private Text text;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        int currentHealth = player.Health;
        int currentShield = player.shield;

        text.text = currentShield + "\n" + currentHealth;
    }
}
