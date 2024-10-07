using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10;
    private Rigidbody rb;
    private float forwardInput;
    private GameObject focalPoint;
    public int powerupActive = 0;
    public float pushPower = 10;
    public GameObject powerupIndicator;
    //public bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        //gameOver = false;
        rb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        forwardInput = Input.GetAxis("Vertical");
        rb.AddForce(focalPoint.transform.forward * speed * forwardInput);
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
        // if(rb.position.y < 0){
        //     gameOver = true;
        // }
    }

    private void OnTriggerEnter(Collider other){

        if(other.gameObject.CompareTag("Powerup")){
            powerupActive += 1;
            powerupIndicator.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdown());
        }
    }

    IEnumerator PowerupCountdown(){
        yield return new WaitForSeconds(5);
        powerupActive -= 1;
        if(powerupActive<1){
        powerupIndicator.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision other){

        if(other.gameObject.CompareTag("Enemy") && powerupActive > 0){

            Rigidbody enemy = other.gameObject.GetComponent<Rigidbody>();
            Vector3 pushAway = (other.gameObject.transform.position - transform.position).normalized;
            enemy.AddForce(pushAway * pushPower, ForceMode.Impulse);
        }
    }
}
