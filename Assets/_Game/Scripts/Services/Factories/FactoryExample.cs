using System;
using UnityEngine;

namespace _Game.Scripts.Services.Factories
{
    public class FactoryExample : MonoBehaviour
    {
        private Factory<CubeProduct> _factory;
        private float _delta = 1;
        
        private void Start()
        {
            _factory = new Factory<CubeProduct>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                CreateExampleProducts();
                _delta += 1;
            }
        }

        private void CreateExampleProducts()
        {
            _factory.CreateProductWithPosition(Products.Cube, new Vector3(0, _delta, _delta), Quaternion.identity, (cube) =>
            {
                if (cube != null)
                {
                    Debug.Log("Cube created successfully!");
                }
            });
            
            _factory.CreateProductWithPosition(Products.Sphere, new Vector3(2, _delta, _delta), Quaternion.identity, (sphere) =>
            {
                if (sphere != null)
                {
                    Debug.Log("Sphere created successfully!");
                }
            });
        }
        
        private void OnDestroy()
        {
            _factory?.ReleaseAllProducts();
        }
    }
}