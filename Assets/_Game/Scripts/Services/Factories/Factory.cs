using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace _Game.Scripts.Services.Factories
{
    public class Factory<T> where T : MonoBehaviour, IProduct
    {
        private Dictionary<Products, string> _productAddresses = new Dictionary<Products, string>()
        {
            { Products.Sphere, "Assets/_Game/Prefabs/SphereProduct.prefab" },
            { Products.Cube, "Assets/_Game/Prefabs/CubeProduct.prefab" }
        };
        
        //private Dictionary<Products, AsyncOperationHandle<GameObject>> _cachedProducts = new();
        private Dictionary<Products, AsyncOperationHandle<GameObject>> _cachedHandles = new();
        private Dictionary<Products, List<T>> _createdProducts = new(); 
    
       
        public async void CreateProduct(Products productType, System.Action<T> onComplete = null)
        {
            if (!_productAddresses.ContainsKey(productType))
            {
                Debug.LogError($"Product {productType} not found in address dictionary");
                onComplete?.Invoke(null);
                return;
            }
            
            string address = _productAddresses[productType];
            
            try
            {
                AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(address);
                await handle.Task;
                
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    GameObject productObject = Object.Instantiate(handle.Result);
                    T product = productObject.GetComponent<T>();
                    product.Initialize();
            
                    _cachedHandles[productType] = handle;
            
                    if (!_createdProducts.ContainsKey(productType))
                        _createdProducts[productType] = new List<T>();
                    _createdProducts[productType].Add(product);
            
                    onComplete?.Invoke(product);
                }
                else
                {
                    Debug.LogError($"Failed to load product {productType} from address: {address}");
                    onComplete?.Invoke(null);
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"Error creating product {productType}: {ex.Message}");
                onComplete?.Invoke(null);
            }
        }
        
        public async void CreateProductWithPosition(Products productType, Vector3 position, Quaternion rotation, System.Action<T> onComplete = null)
        {
            CreateProduct(productType, (product) =>
            {
                if (product != null)
                {
                    product.transform.position = position;
                    product.transform.rotation = rotation;
                }
                onComplete?.Invoke(product);
            });
        }
        
        public void ReleaseAllProducts()
        {
            foreach (var productList in _createdProducts.Values)
            {
                foreach (var product in productList)
                {
                    if (product != null)
                        Debug.Log($"Releasing {product.name}");
                }
            }
            _createdProducts.Clear();
        
            foreach (var handle in _cachedHandles.Values)
            {
                Addressables.Release(handle);
                Debug.Log($"Released Addressable asset: {handle}");
            }
            _cachedHandles.Clear();
        
            Debug.Log("All products and assets released successfully");
        }
        
        
        public void SetProductAddress(Products productType, string address)
        {
            if (_productAddresses.ContainsKey(productType))
            {
                _productAddresses[productType] = address;
            }
            else
            {
                _productAddresses.Add(productType, address);
            }
        }
    }
}