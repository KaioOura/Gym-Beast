using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorpseBuyer : MonoBehaviour
{
    [SerializeField] private Transform _corpseTarget;

    private List<Body> _corpses = new List<Body>();

    public event Action<Body> OnBodyBought;
    public event Action<int> OnPayPlayer;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_corpses.Count > 0)
        {

            for (int i = _corpses.Count; i > 0; i--)
            {
                if (_corpses[i - 1].BodyBones.position != _corpseTarget.position)
                    return;
                _corpses[i].BodyBones.position = Vector3.Lerp(_corpses[i].BodyBones.position, _corpseTarget.position, 5 * Time.deltaTime);
                _corpses[i].BodyBones.rotation = Quaternion.Lerp(_corpses[i].BodyBones.rotation, _corpseTarget.rotation, 5 * Time.deltaTime);
            }
        }
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
                OnPayPlayer?.Invoke(10);
            }

            StartCoroutine(MoveCorpses());
            OnBodyBought -= empilhamentoController.OnBodyBought;
        }
    }

    IEnumerator MoveCorpses()
    {
        while (_corpses.Count > 0)
        {
            if (Vector3.Distance(_corpses[0].BodyBones.position, _corpseTarget.position) > 0.1f)
                _corpses[0].BodyBones.position = Vector3.MoveTowards(_corpses[0].BodyBones.position, _corpseTarget.position, 10 * Time.deltaTime);
            else
                _corpses.RemoveAt(0);

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
