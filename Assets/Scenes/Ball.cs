using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    float speed;
    int isMoving = 1;
    float radius;
    Vector2 direction;
    Vector2 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        direction = Vector2.one.normalized;
        radius = transform.localScale.x / 2; // half the width
        startPosition = new Vector2(transform.position.x, transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * isMoving * Time.deltaTime);

        if (transform.position.y < GameManager.bottomLeft.y + radius && direction.y < 0) {
            direction.y = -direction.y;
        } 
        if (transform.position.y > GameManager.topRight.y - radius && direction.y > 0) {
            direction.y = -direction.y;
        }

        // Game over
        if (transform.position.x < GameManager.bottomLeft.x + radius && direction.x < 0) {
            GameManager.instance.addScorePlayer("p2");
            StartCoroutine( Reset() );
        } 
        if (transform.position.x > GameManager.topRight.x - radius && direction.x > 0) {
            GameManager.instance.addScorePlayer("p1");
            StartCoroutine( Reset() );
        }
    }

    IEnumerator Reset() {
        isMoving = 0;
        transform.position = startPosition;

        yield return new WaitForSeconds(2);

        isMoving = 1;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Paddle") {
            bool isRight = other.GetComponent<Paddle> ().isRight;

            if (isRight && direction.x > 0) {
                direction.x = -direction.x;
            }
            if (!isRight && direction.x < 0) {
                direction.x = -direction.x;
            }
        }

    }
}
