using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pruebaCola : MonoBehaviour
{


    public Queue<int> colaPrueba;


    // Start is called before the first frame update
    void Start()
    {
        colaPrueba = new Queue<int>();
        for (int i = 0; i<10; i++)
        {
            colaPrueba.Enqueue(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        int aux = colaPrueba.Dequeue();
        print("Debug");
    }
}
