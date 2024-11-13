using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Transporter
{
    public static IEnumerator TransportEnumerator;

    public static void TransportObject(MonoBehaviour requester, Transform obj, Transform target)
    {
        requester.StartCoroutine(TransportRoutine(obj, target));
    }

    static IEnumerator TransportRoutine(Transform obj, Transform target)
    {
        while (true)
        {
            if (Vector3.Distance(obj.position, target.position) > 0.1f)
            {
                obj.position = Vector3.Slerp(obj.position, target.position, 5 * Time.deltaTime);
            }
            else
            {
                yield break;
            }

            yield return null;
        }
    }
}
