using UnityEngine;
using UnityEngine.UI;

public class ShopItemLoader : MonoBehaviour
{
   public Image image;

   public void Load(Sprite sprite)
   {
      image.sprite = sprite;
   }
}
