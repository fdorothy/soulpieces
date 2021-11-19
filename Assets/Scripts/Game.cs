using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace SoulPieces
{
    public class Game : MonoBehaviour
    {
        public static Game singleton;
        public Player player;
        public SpriteRenderer fader;
        public bool paused = false;
        public bool gameOver = false;
        Coroutine mainRoutine;
        protected AudioSource music;

        private void Awake()
        {
            singleton = this;
            fader.color = new Color(0, 0, 0, 1f);
            fader.DOFade(0.0f, 1.0f);
            music = GetComponent<AudioSource>();
        }

        public void GameOver()
        {
            if (!gameOver)
            {
                music.DOFade(0.0f, 2.0f);
                gameOver = true;
                StartCoroutine(GameOverRoutine());
            }
        }

        public IEnumerator GameOverRoutine()
        {
            if (mainRoutine != null)
                StopCoroutine(mainRoutine);
            yield return new WaitForEndOfFrame();
            fader.DOFade(1.0f, 1.0f);
            yield return new WaitForSeconds(1.0f);
            yield return DropText("Game Over");
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }

        IEnumerator DropText(string text)
        {
            yield return new WaitForSeconds(2.5f);
        }
    }
}