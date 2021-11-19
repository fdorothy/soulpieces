using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace SoulPieces
{
    [RequireComponent(typeof(CharacterController2D))]
    public class Player : MonoBehaviour
    {
        CharacterController2D controller;
        float jumpCooldown = 0.5f;
        float jumpTime = -1.0f;
        public LayerMask whatIsEnemy;
        public Rigidbody2D rb;
        AudioSource audioSource;
        public AudioClip jumpSfx;
        public List<Heart> hearts;
        public int health = 3;
        public bool holdingDownJump = false;

        private void Start()
        {
            controller = GetComponent<CharacterController2D>();
            rb = GetComponent<Rigidbody2D>();
            audioSource = GetComponent<AudioSource>();
            controller.OnLandEvent += (x) => PlayJumpSound();
            controller.OnJump += PlayJumpSound;
            SyncHearts();
        }

        public void PlayJumpSound()
        {
            if (jumpSfx != null && audioSource != null)
                audioSource.PlayOneShot(jumpSfx, .25f);

        }

        public Bounds OrthographicBounds(Camera camera)
        {
            float screenAspect = (float)Screen.width / (float)Screen.height;
            float cameraHeight = camera.orthographicSize * 2;
            Bounds bounds = new Bounds(
                camera.transform.position,
                new Vector3(cameraHeight * screenAspect, cameraHeight, 0));
            return bounds;
        }

        private void Update()
        {
            if (Game.singleton.gameOver)
            {
                rb.isKinematic = true;
                rb.velocity = Vector2.zero;
                return;
            }

            if (!Game.singleton.paused)
            {
                float h = Input.GetAxis("Horizontal");
                bool jump = Input.GetKeyDown(KeyCode.Space) && jumpTime <= 0.0f;
                controller.holdingJump = jump && controller.holdingJump;
                controller.Move(h, false, jump);
                if (jump)
                    jumpTime = jumpCooldown;
                if (jumpTime > 0.0f)
                    jumpTime -= Time.deltaTime;
            }
        }

        Vector3 ClampToBounds(Bounds b, Vector3 p)
        {
            if (p.x < b.min.x)
                p = new Vector3(b.min.x, p.y, p.z);
            if (p.x > b.max.x)
                p = new Vector3(b.max.x, p.y, p.z);
            if (p.y < b.min.y)
                p = new Vector3(p.x, b.min.y, p.z);
            if (p.y > b.max.y)
                p = new Vector3(p.x, b.max.y, p.z);
            return p;
        }

        void GameOver()
        {
            Game.singleton.GameOver();
        }

        public void hit()
        {
            health--;
            SyncHearts();
            if (health <= 0)
                GameOver();
        }

        public void SyncHearts()
        {
            for (int i = 0; i < hearts.Count; i++)
                if (i < health)
                    hearts[i].SetFull();
                else
                    hearts[i].SetEmpty();
        }
    }
}
