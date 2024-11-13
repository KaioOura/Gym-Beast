using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackSystem : MonoBehaviour
{
    [SerializeField] private List<Body> gameObjects = new List<Body>();

    public float maxRotSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        AlignRotatio();
    }

    void AlignRotatio()
    {
        for (int i = 0; i < gameObjects.Count; i++)
        {
            if (i == 0)
            {
                continue;
            }
            float angleDiference = Quaternion.Angle(gameObjects[i].transform.rotation, gameObjects[0].BodyPos.rotation);
            float rotationSpeed = Mathf.Lerp(0, maxRotSpeed, angleDiference);

            gameObjects[i].transform.position = Vector3.Lerp(gameObjects[i].transform.position, gameObjects[i - 1].BodyPos.position, rotationSpeed * Time.deltaTime) ;
            gameObjects[i].transform.rotation = Quaternion.Slerp(gameObjects[i].transform.rotation, gameObjects[i - 1].transform.rotation, rotationSpeed * Time.deltaTime);
        }
    }
}
