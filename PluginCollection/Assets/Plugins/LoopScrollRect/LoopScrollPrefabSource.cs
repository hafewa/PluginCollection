using UnityEngine;
using System.Collections;

namespace UnityEngine.UI
{
    [System.Serializable]
    public class LoopScrollPrefabSource
    {
        public GameObject ItemTemplatePrefab;
        public int poolSize = 5;

        private bool inited = false;
        public virtual GameObject GetObject()
        {
            if (!inited)
            {
                SG.ResourceManager.Instance.InitPool(ItemTemplatePrefab, poolSize);
                inited = true;
            }

            var go = SG.ResourceManager.Instance.GetObjectFromPool(ItemTemplatePrefab);
            go.SendMessage("ScrollCellInit", SendMessageOptions.DontRequireReceiver);

            return go;
        }

        public virtual void ReturnObject(Transform go)
        {
            go.SendMessage("ScrollCellReturn", SendMessageOptions.DontRequireReceiver);
            SG.ResourceManager.Instance.ReturnObjectToPool(go.gameObject);
        }

        public void Destroy()
        {
            inited = false;
            SG.ResourceManager.Instance.DestroyPool(ItemTemplatePrefab);
        }
    }
}
