using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Collider2D viewWindow;

    Bounds maxBounds;
    Tilemap[] maps;
    Bounds[] mapBounds;
    public float speed = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        maxBounds = new Bounds();
        maps = FindObjectsOfType<Tilemap>();
        mapBounds = new Bounds[maps.Length];
        for (int i=0; i<maps.Length; i++)
        {
            Tilemap m = maps[i];
            maxBounds.Encapsulate(m.transform.TransformPoint(m.localBounds.min));
            maxBounds.Encapsulate(m.transform.TransformPoint(m.localBounds.max));

            Vector3 max = m.transform.TransformPoint(m.localBounds.max);
            Vector3 min = m.transform.TransformPoint(m.localBounds.min);
            Bounds bounds = new Bounds();
            bounds.max = max;
            bounds.min = min;
            mapBounds[i] = bounds;
        }
        Debug.Log(maxBounds);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        ConstrainCameraToViewWindow();
        ConstrainCameraToMapBounds();
    }

    void ConstrainCameraToViewWindow()
    {
        transform.position += ConstrainToBounds(target.transform.position, viewWindow.bounds);
    }

    void ConstrainCameraToMapBounds()
    {
        foreach (Bounds b in mapBounds)
        {
            if (b.Contains(target.transform.position))
            {
                Bounds cameraBounds = Camera.main.OrthographicBounds();
                transform.position += ConstrainToBounds(cameraBounds, b);
                return;
            }
        }
    }

    Vector3 ConstrainToBounds(Bounds a, Bounds b)
    {
        float dx = 0f, dy = 0f;
        if (a.min.x < b.min.x || a.extents.x > b.extents.x)
            dx = b.min.x - a.min.x;
        else if (a.max.x > b.max.x)
            dx = b.max.x - a.max.x;
        if (a.min.y < b.min.y || a.extents.y > b.extents.y)
            dy = b.min.y - a.min.y;
        else if (a.max.y > b.max.y)
            dy = b.max.y - a.max.y;
        return new Vector3(dx, dy);
    }

    Vector3 ConstrainToBounds(Vector3 a, Bounds b)
    {
        float dx = 0f, dy = 0f;
        if (a.x < b.min.x)
            dx = a.x - b.min.x;
        else if (a.x > b.max.x)
            dx = a.x - b.max.x;
        if (a.y < b.min.y)
            dy = a.y - b.min.y;
        else if (a.y > b.max.y)
            dy = a.y - b.max.y;
        return new Vector3(dx, dy);
    }
}
