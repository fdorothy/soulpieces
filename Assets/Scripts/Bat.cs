using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SoulPieces
{
    public class Bat : MonoBehaviour
    {
        public enum BatState
        {
            SPAWNING,
            AIM,
            ATTACK
        }

        BatState state = BatState.SPAWNING;
        float flapDir = -1f;
        float aimSpeed = 1.0f;
        float attackSpeed = 5.0f;

        // Start is called before the first frame update
        IEnumerator Start()
        {
            Player player = FindObjectOfType<Player>();
            yield return new WaitForSeconds(1.0f);
            state = BatState.AIM;
            yield return new WaitUntil(() => player.transform.position.y > transform.position.y - 0.5f);
            if (player.transform.position.x < transform.position.x)
                flapDir = -1f;
            else
                flapDir = 1f;
            state = BatState.ATTACK;
        }

        // Update is called once per frame
        void Update()
        {
            switch (state)
            {
                case BatState.SPAWNING:
                    break;
                case BatState.AIM:
                    transform.position += Vector3.down * Time.deltaTime * aimSpeed;
                    break;
                case BatState.ATTACK:
                    transform.position += Vector3.right * flapDir * Time.deltaTime * attackSpeed;
                    break;
            }
        }
    }
}