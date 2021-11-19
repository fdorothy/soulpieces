using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SoulPieces
{
    public class Heart : MonoBehaviour
    {
        public Sprite emptySprite, fullSprite;
        public SpriteRenderer sr;

        public void Start()
        {
            sr = GetComponent<SpriteRenderer>();
        }

        public void SetEmpty()
        {
            sr.sprite = emptySprite;
        }

        public void SetFull()
        {
            sr.sprite = fullSprite;
        }
    }
}
