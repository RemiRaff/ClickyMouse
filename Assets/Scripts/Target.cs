using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    // different pointValue to set in each prefabs
    public int pointValue;
    // visual effect for target destroy
    public ParticleSystem explosionParticle;

    private Rigidbody targetRb;
    private float minSpeed = 12; // 12
    private float maxSpeed = 16; // 16
    private float maxTorque = 10;
    private float xRange = 4;
    private float ySpawnPos = 0; // pour spawn hors ?cran
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        targetRb = GetComponent<Rigidbody>();

        targetRb.AddForce(RandomForce(), ForceMode.Impulse);

        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);

        targetRb.position = RandomSpawnPos();

        // setup the link to the gamemanager for scoring
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (gameManager.isGameActive)
        {
            Destroy(gameObject);

            // score when an object is destroyed
            gameManager.UpdateScore(pointValue);

            // particules effect
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // destroy the object if it collides sensor
        Destroy(gameObject);

        // only bad can be destroyed by the sensor
        if (!gameObject.CompareTag("Bad"))
        {
            gameManager.GameOver();
        }
    }

    private Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    private float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    private Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos); // z value 0 not nescessary to add
    }
}
