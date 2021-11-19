using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soul : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            transform.rotation *= Quaternion.Euler(0f, 0f, 90f);
        }
    }
}
