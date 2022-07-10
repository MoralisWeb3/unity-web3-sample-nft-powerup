using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

using MoralisUnity;
using MoralisUnity.Web3Api.Models;

using Newtonsoft.Json; //Used to deserialize complex JSON --> https://code-maze.com/csharp-deserialize-complex-json-object/

namespace NFT_PowerUp
{
    public class MetadataObject
    {
        public string name { get; set; }
        public string description { get; set; }
        public string image { get; set; }

        [CanBeNull] public List<AttributeObject> attributes { get; set; }
    }

    public class AttributeObject
    {
        [CanBeNull] public string display_type { get; set; }
        public string trait_type { get; set; }
        public float value { get; set; }
    }
    
    public class Inventory : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private TextMeshProUGUI titleLabel;
        [SerializeField] private string titleText;
        [SerializeField] private InventoryItem itemPrefab;
        [SerializeField] private Transform content;

        private InventoryItem _currentSelectedItem;
        private int _currentItemsCount;


        #region UNITY_LIFECYCLE

        private void OnEnable()
        {
            InventoryItem.onSelected += CaptureSelectedItem;
            
            titleLabel.text = titleText;
        }

        private void OnDisable()
        {
            InventoryItem.onSelected -= CaptureSelectedItem;
        }

        #endregion


        #region PUBLIC_METHODS

        public async void LoadItems(string playerAddress, string contractAddress, ChainList contractChain)
        {
            try
            {
                NftOwnerCollection noc =
                    await Moralis.GetClient().Web3Api.Account.GetNFTsForContract(playerAddress.ToLower(),
                        contractAddress, contractChain);
            
                List<NftOwner> nftOwners = noc.Result;

                // We only proceed if we find some
                if (!nftOwners.Any())
                {
                    Debug.Log("You don't own items");
                    return;
                }

                if (nftOwners.Count == _currentItemsCount)
                {
                    Debug.Log("There are no new items to load");
                    return;
                }
                
                // We clear the grid before adding new items
                ClearAllItems(); 
                
                // If we own one or more NFTs...
                foreach (var nftOwner in nftOwners)
                {
                    if (nftOwner.Metadata == null)
                    {
                        // Sometimes GetNFTsForContract fails to get NFT Metadata. We need to re-sync
                        await Moralis.GetClient().Web3Api.Token.ReSyncMetadata(nftOwner.TokenAddress, nftOwner.TokenId, contractChain);
                        
                        Debug.Log("We couldn't get NFT Metadata. Re-syncing...");
                        continue;
                    }

                    // Check if tokenUri is null. If it's null it means it has probably been burned but it still appears
                    if (nftOwner.TokenUri is null)
                    {
                        Debug.Log("Token already burned");
                        return;
                    }
                    
                    // Deserialize metadata JSON to MetadataObject
                    var metadata = nftOwner.Metadata;
                    MetadataObject metadataObject = DeserializeUsingNewtonSoftJson(metadata);
                    
                    // We ONLY want objects with attributes. If metadataObject is null or metadataObject.attributes is null, we don't continue
                    if (metadataObject?.attributes is null)
                    {
                        return;
                    }

                    // Populate new item
                    PopulatePlayerItem(nftOwner.TokenId, metadataObject);
                }
            }
            catch (Exception exp)
            {
                Debug.LogError(exp.Message);
            }
        }
        
        public void DeleteCurrentSelectedItem()
        {
            Destroy(_currentSelectedItem.gameObject);
            _currentItemsCount--;
        }

        #endregion


        #region PRIVATE_METHODS

        private void PopulatePlayerItem(string tokenId, MetadataObject metadataObject)
        {
            InventoryItem newItem = Instantiate(itemPrefab, content);
            
            newItem.Init(tokenId, metadataObject);

            _currentItemsCount++;
        }

        public void DeleteItem(string id)
        {
            foreach (Transform item in content)
            {
                InventoryItem itemClass = item.GetComponent<InventoryItem>(); // Assuming every item has InventoryItem script

                if (itemClass.myTokenId == id)
                {
                    Destroy(item.gameObject);
                    _currentItemsCount--;
                }
            }
        }

        private void ClearAllItems()
        {
            foreach (Transform item in content)
            {
                Destroy(item.gameObject);
            }

            _currentItemsCount = 0;
        }
        
        [CanBeNull]
        private MetadataObject DeserializeUsingNewtonSoftJson(string json)
        {
            var metadataObject = JsonConvert.DeserializeObject<MetadataObject>(json);
            return metadataObject;
        }

        #endregion


        #region EVENT_HANDLERS

        private void CaptureSelectedItem(InventoryItem selectedItem)
        {
            _currentSelectedItem = selectedItem;
        }

        #endregion
    }   
}
