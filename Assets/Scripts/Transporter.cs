using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transporter : MonoBehaviour
{
    public static Transporter Instance;

    private Queue<Transport> _queue = new Queue<Transport>();

    private Dictionary<string, Queue<Transport>> _transports = new Dictionary<string, Queue<Transport>>();


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void TransportObject(string transportID, Transform obj, Transform target, Action callBack)
    {
        Transport transport = new Transport(transportID, obj, target, callBack);

        if (_transports.ContainsKey(transportID))
        {
            if (_transports[transport.ID].Count == 0)
            {
                _transports[transport.ID].Enqueue(transport);
                StartCoroutine(TransportRoutine(_transports[transport.ID], transport.ObjectToBeTransported, transport.Destination, transport.CallBack));
            }
            else
            {
                _transports[transport.ID].Enqueue(transport);
            }
        }
        else
        {
            _transports.Add(transport.ID, new Queue<Transport>());
            _transports[transport.ID].Enqueue(transport);
            StartCoroutine(TransportRoutine(_transports[transport.ID], transport.ObjectToBeTransported, transport.Destination, transport.CallBack));
        }

        // if (_queue.Count == 0)
        // {
        //     _queue.Enqueue(transport);
        //     StartCoroutine(TransportRoutine(transport.ObjectToBeTransported, transport.Destination, transport.CallBack));
        // }
        // else
        // {
        //     _queue.Enqueue(transport);
        // }
    }

    IEnumerator TransportRoutine(Queue<Transport> queue, Transform obj, Transform target, Action callBack)
    {
        Transport transport = queue.Peek();

        while (true)
        {
            if (Vector3.Distance(obj.position, target.position) > 0.1f)
            {
                obj.position = Vector3.Lerp(obj.position, target.position, 10 * Time.deltaTime);
            }
            else
            {
                callBack?.Invoke();
                queue.Dequeue();

                if (queue.Count > 0)
                {
                    Transport transportTemp = queue.Peek();
                    StartCoroutine(TransportRoutine(queue, transportTemp.ObjectToBeTransported, transportTemp.Destination, transportTemp.CallBack));
                }
                else
                {
                    _transports.Remove(transport.ID);
                }

                yield break;
            }

            yield return null;
        }
    }
}

public struct Transport
{
    public string ID;
    public Transform ObjectToBeTransported;
    public Transform Destination;
    public Action CallBack;

    public Transport(string _transportID, Transform _object, Transform _destination, Action _callBack)
    {
        ID = _transportID;
        ObjectToBeTransported = _object;
        Destination = _destination;
        CallBack = _callBack;
    }
}
