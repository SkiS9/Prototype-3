using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool gameOver = false;
    public float JumpForce = 10f;
        public float GravityModifier = 1f;
    public bool IsOnGround = true;

    private Rigidbody _playerRB;

    // Start is called before the first frame update
    void Start()
    {
     _playerRB = GetComponent<Rigidbody>();
        //_playerRB.AddForce(Vector3.up * 100);
        Physics.gravity *= GravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsOnGround)
            {
              _playerRB.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
               IsOnGround = false;
            }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            IsOnGround = true;
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            Debug.Log("Game Over!");
        }
    }
}
