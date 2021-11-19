using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace SoulPieces
{
    class World : MonoBehaviour
    {
        Tilemap[] maps;
        Bounds[] mapBounds;
        Bounds maxBounds;
        Player player;
        Bounds currentMapBounds;
        bool isPlayerWithinBounds = false;

        public Bounds MaxBounds => maxBounds;
        public Bounds[] MapBounds => mapBounds;
        public Bounds CurrentMapBounds => CurrentMapBounds;
        public bool IsPlayerWithinBounds => isPlayerWithinBounds;

        private void Start()
        {
            player = FindObjectOfType<Player>();
            maps = FindObjectsOfType<Tilemap>();
            mapBounds = new Bounds[maps.Length];
            for (int i = 0; i < maps.Length; i++)
            {
                Tilemap m = maps[i];
                Vector3 max = m.transform.TransformPoint(m.localBounds.max);
                Vector3 min = m.transform.TransformPoint(m.localBounds.min);
                mapBounds[i] = new Bounds() { max = max, min = min } ;
                maxBounds.Encapsulate(mapBounds[i]);
            }
        }

        private void Update()
        {
            isPlayerWithinBounds = false;
            foreach (Bounds b in mapBounds)
            {
                if (b.Contains(player.transform.position))
                {
                    currentMapBounds = b;
                    isPlayerWithinBounds = true;
                }
            }
        }
    }
}
