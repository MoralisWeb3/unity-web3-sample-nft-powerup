using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace NFT_PowerUp
{
    public class InventoryItem : MonoBehaviour
    {
        public static Action<InventoryItem> onSelected;

        // Power-Up values
        [HideInInspector] public float boostPercentage;
        [HideInInspector] public float boostDuration;
        
        // Metadata values
        [HideInInspector] public string myTokenId;
        [HideInInspector] public MetadataObject myMetadataObject;

        [Header("UI Components")]
        [SerializeField] private Image myIcon;
        [SerializeField] private Button myButton;
        
        public void Init(string tokenId, MetadataObject metadataObject)
        {
            myTokenId = tokenId;
            myMetadataObject = metadataObject;
            
            if (myMetadataObject.attributes is null)
            {
                Debug.Log("No attributes found");
                return;
            }
            
            // Get Power-Up values
            foreach (var attribute in myMetadataObject.attributes)
            {
                if (attribute.display_type == "boost_percentage")
                {
                    if (attribute.trait_type == "Movement")
                    {
                        boostPercentage = attribute.value;   
                    }
                }

                if (attribute.display_type == "boost_number")
                {
                    if (attribute.trait_type == "Duration")
                    {
                        boostDuration = attribute.value;
                    }
                }
            }

            StartCoroutine(GetTexture(myMetadataObject.image));
        }

        public void Selected()
        {
            onSelected?.Invoke(this);
        }

        private IEnumerator GetTexture(string imageUrl)
        {
            using UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(imageUrl);
        
            yield return uwr.SendWebRequest();

            if (uwr.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(uwr.error);
                uwr.Dispose();
            }
            else
            {
                var tex = DownloadHandlerTexture.GetContent(uwr);
                myIcon.sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), tex.height);
                
                //Now we are able to click the button and we will pass the loaded sprite :)
                myIcon.gameObject.SetActive(true);
                myButton.interactable = true;
            
                uwr.Dispose();
            }
        }
    }   
}
