using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorpseBuyer : MonoBehaviour
{
    [SerializeField] private Transform _corpseTarget;

    //Prefab de notas, fazer as notas lerparem até o player através de evento

    private List<Body> _corpses = new List<Body>();

    public event Action<Body> OnBodyBought;
    public event Action<int> OnPayPlayer;

    // Start is called before the first frame update
    void Start()
    {
        OnPayPlayer += PlayerManager.Instance.OnReceivePayment;
    }

    void OnDestroy()
    {
        OnPayPlayer -= PlayerManager.Instance.OnReceivePayment;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TryToBuyCorpse(EmpilhamentoController empilhamentoController)
    {
        if (empilhamentoController.Bodies.Count > 0)
        {
            OnBodyBought += empilhamentoController.OnBodyBought;

            List<Body> bodiesToAdd = new List<Body>();
            //Move o corpo da pilha para o buyer e paga o player
            foreach (Body body in empilhamentoController.Bodies)
            {
                if (_corpses.Contains(body))
                    continue;

                bodiesToAdd.Add(body);
            }

            foreach (var item in bodiesToAdd)
            {
                _corpses.Add(item);
                OnBodyBought?.Invoke(item);

                Transporter.Instance.TransportObject("Body", item.BodyBones, _corpseTarget, () =>
                {
                    OnPayPlayer?.Invoke(item.SellValue);
                    _corpses.RemoveAt(0);
                });
            }

            //StartCoroutine(MoveCorpses(OnPayPlayer));
            OnBodyBought -= empilhamentoController.OnBodyBought;
        }
    }

    IEnumerator MoveCorpses(Action<int> callback)
    {
        while (_corpses.Count > 0)
        {
            if (Vector3.Distance(_corpses[0].BodyBones.position, _corpseTarget.position) > 0.1f)
                _corpses[0].BodyBones.position = Vector3.MoveTowards(_corpses[0].BodyBones.position, _corpseTarget.position, 10 * Time.deltaTime);
            else
            {
                callback?.Invoke(_corpses[0].SellValue);
                _corpses.RemoveAt(0);
            }

            yield return null;
        }
    }

    public void OnTriggerEnter(Collider collider)
    {

        if (!collider.CompareTag("Player"))
            return;


        if (collider.TryGetComponent(out EmpilhamentoController empilhamentoController))
        {
            TryToBuyCorpse(empilhamentoController);
        }
    }
    void OnTriggerExit(Collider collider)
    {

    }



}
