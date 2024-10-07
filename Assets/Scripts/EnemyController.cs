using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 5;
    private Rigidbody enemyRB;
    private GameObject player;
    private Vector3 toPlayer;
    // Start is called before the first frame update
    void Start()
    {
        enemyRB = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        toPlayer = (player.transform.position - transform.position).normalized;
        enemyRB.AddForce(toPlayer * speed);
        if(transform.position.y < -3){
            Destroy(gameObject);
        }
    }
}
