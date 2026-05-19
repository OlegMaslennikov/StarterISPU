using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Game.Scripts.Services.Factories
{
    public class SimpleFactory : MonoBehaviour
    {
        [SerializeField] private GameObject sphere;
        [SerializeField] private GameObject cube;
        private GameObject capsule;
        private Object[] allSounds;

        private void Start()
        {
            capsule = Resources.Load<GameObject>("Units/Capsule");
            allSounds = Resources.LoadAll<GameObject>("Sounds");
            
        }
        

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
                CreateProduct(Products.Sphere);

            if (Input.GetKeyDown(KeyCode.C))
                CreateProduct(Products.Cube);
        }

        public GameObject CreateProduct(Products productType)
        {
            GameObject newProduct = null;
            GameObject prefabToSpawn = null;
            
            switch (productType)
            {
                case Products.Cube:
                    prefabToSpawn = cube;
                    break;
                case Products.Sphere:
                    prefabToSpawn = sphere;
                    break;
                default:
                    Debug.LogError($"Unknown product type: {productType}");
                    return null;
            }
            
            if (prefabToSpawn == null)
            {
                Debug.LogError($"Prefab for {productType} is not assigned in inspector!");
                return null;
            }
            
            newProduct = Instantiate(prefabToSpawn, transform.position, transform.rotation);
            
            IProduct productComponent = newProduct.GetComponent<IProduct>();
            if (productComponent != null)
            {
                productComponent.Initialize();
            }
            else
            {
                Debug.LogWarning($"Product {productType} has no IProduct component!");
            }
            
            return newProduct;
        }
        
        public GameObject CreateProduct(Products productType, Vector3 position, Quaternion rotation)
        {
            GameObject newProduct = CreateProduct(productType);
            if (newProduct != null)
            {
                newProduct.transform.position = position;
                newProduct.transform.rotation = rotation;
            }
            return newProduct;
        }
    }
}