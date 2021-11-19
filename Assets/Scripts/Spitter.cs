using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoulPieces
{
    [Serializable]
    public class SpitterFrame
    {
        public float delay = 1.0f;
        public int rotate = 0;
        public int spits = 1; // evenly distributed around
    }

    [Serializable]
    public class SpitterAnimation
    {
        public List<SpitterFrame> frames;
    }

    public enum SpitterType
    {
        ROTATE_4_SHOTS
    }

    public class Spitter : MonoBehaviour
    {
        public Spit spitPrefab;
        public SpitterType spitterType;
        protected float targetRotation;
        protected float targetRotationTime;
        protected SpitterAnimation rotate4Shots = new SpitterAnimation();

        Spitter()
        {
            rotate4Shots.frames = new List<SpitterFrame>()
            {
                new SpitterFrame() {delay = 1.0f, rotate = 45, spits = 4}
            };
        }

        public SpitterAnimation SpitterAnimation
        {
            get
            {
                switch (spitterType)
                {
                    case SpitterType.ROTATE_4_SHOTS:
                        return rotate4Shots;
                }
                return null;
            }
        }

        // Start is called before the first frame update
        IEnumerator Start()
        {
            Renderer renderer = GetComponent<Renderer>();
            Quaternion originalRotation = transform.rotation;
            int index = 0;
            while (true)
            {
                if (renderer.isVisible)
                    yield return ProcessSpitterFrame(SpitterAnimation.frames[index++]);
                else
                {
                    index = 0;
                    transform.rotation = originalRotation;
                }
                if (index >= SpitterAnimation.frames.Count)
                    index = 0;
                yield return new WaitForSeconds(1.0f);
            }
        }

        public IEnumerator ProcessSpitterFrame(SpitterFrame frame)
        {
            targetRotation += frame.rotate;
            yield return new WaitForSeconds(frame.delay);
            if (frame.spits > 0)
            {
                Vector3 up = transform.up;
                float div = 360f / frame.spits;
                for (int i = 0; i < frame.spits; i++)
                {
                    float theta = div * i + targetRotation;
                    Spit(Quaternion.AngleAxis(theta, Vector3.forward) * transform.up);
                }
            }
        }

        public void Spit(Vector3 dir)
        {
            Spit spit = Instantiate<Spit>(spitPrefab, transform);
            spit.transform.position = transform.position;
            spit.Begin(transform.position, dir, 2.0f);
        }
    }
}