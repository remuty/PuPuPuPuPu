using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
    
    public GameObject jump_effect;
    [SerializeField] float jump_power = 600, speed = 0.1f;
    Rigidbody2D _rb;
    int jump_count;
    bool rot_start = false;
    float rot,variation;
    Vector2 initial_position;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        variation = 360 / 0.5f;             //1秒間の回転量
        initial_position = transform.position;
        jump_count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
    }

    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.tag == "Ground")
        {
            jump_count = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag == "GameOver")
        {
            SceneManager.LoadScene("GameOver");
        }
        if (c.gameObject.tag == "Clear")
        {
            SceneManager.LoadScene("Clear");
        }
    }

    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        Vector2 pos = transform.position;
        pos.x += x * speed;
        transform.position = pos;
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && jump_count < 2)
        {
            Vector2 pos = transform.position;
            pos.y = -1.6f;
            Instantiate(jump_effect, pos,Quaternion.identity);
            _rb.velocity = Vector2.zero;
            _rb.AddForce(Vector2.up * jump_power);
            jump_count++;
        }
        if (Input.GetButtonDown("Jump") && jump_count == 2)
        {
            jump_count++;
            rot = 0f;
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            rot_start = true;
        }
        if (rot_start)
        {
            transform.Rotate(0, 0, -variation * Time.deltaTime);
            rot += variation * Time.deltaTime;
            if (rot >= 360)
            {
                rot_start = false;
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }
}
