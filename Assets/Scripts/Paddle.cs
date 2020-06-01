using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField]
    public float speed;
    float height;

    string input;
    public bool isRight;

    // Start is called before the first frame update
    void Start()
    {
        height = transform.localScale.y;
        speed = 10f;
    }

    public void Init(bool isRightPaddle) {
        isRight = isRightPaddle;
        Vector2 pos = Vector2.zero;

        if (isRightPaddle) {
            pos = new Vector2(GameManager.topRight.x, 0);
            pos += Vector2.left * transform.localScale.x;

            GetComponent<Renderer>().material.color = Color.blue;
            input = "PaddleRight";
        }
        else {
            pos = new Vector2(GameManager.bottomLeft.x, 0);
            pos += Vector2.right * transform.localScale.x;

            GetComponent<Renderer>().material.color = Color.red;
            input = "PaddleLeft";
        }

        // Update this paddle's position
        transform.position = pos;
        transform.name = input;
    }

    // Update is called once per frame
    void Update()
    {
        // GetAxis is a number between -1 to 1 (-1 for down, 1 for up)
        float move = Input.GetAxis(input) * Time.deltaTime * speed;

        // Restrict paddle movement
        if (transform.position.y < GameManager.bottomLeft.y + height / 2 && move < 0) {
            move = 0;
        }
        if (transform.position.y > GameManager.topRight.y - height / 2 && move > 0) {
            move = 0;
        }

        transform.Translate(move * Vector2.up);
    }
}
