using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool gameOver = false;
    public float JumpForce = 10f;
        public float GravityModifier = 1f;
    public bool IsOnGround = true;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public AudioClip jumpSound;
    public AudioClip crashSound;

    private Rigidbody _playerRB;
    private Animator _playerAnim;
    private AudioSource _playerAudio;

    // Start is called before the first frame update
    void Start()
    {
     _playerRB = GetComponent<Rigidbody>();
        _playerAnim = GetComponent<Animator>();
        Physics.gravity *= GravityModifier;
        _playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsOnGround && !gameOver)
            {
            dirtParticle.Stop();
              _playerRB.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
               IsOnGround = false;
            _playerAnim.SetTrigger("Jump_trig");
            _playerAudio.PlayOneShot(jumpSound, 1.0f);
            }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            dirtParticle.Play();
            IsOnGround = true;
        }

        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            explosionParticle.Play();
            gameOver = true;
            Debug.Log("Game Over!");
            _playerAnim.SetBool("Death_b", true);
            _playerAnim.SetInteger("DeathType_int)", 1);
            dirtParticle.Stop();
            _playerAudio.PlayOneShot(crashSound, 1.0f);
        }
    }
}
