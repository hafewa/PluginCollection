using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace UnityEngine.UI
{
    public abstract class LoopScrollDataSource
    {
        public abstract void ProvideData(Transform transform, int idx);
    }

    public class LoopScrollSendIndexSource : LoopScrollDataSource
    {
        public static readonly LoopScrollSendIndexSource Instance = new LoopScrollSendIndexSource();

        LoopScrollSendIndexSource() { }

        public override void ProvideData(Transform transform, int idx)
        {
            transform.SendMessage("ScrollCellIndex", idx);
        }
    }

    public class LoopScrollArraySource<T> : LoopScrollDataSource
    {
        T[] objectsToFill;

        public LoopScrollArraySource(T[] objectsToFill)
        {
            this.objectsToFill = objectsToFill;
        }

        public override void ProvideData(Transform transform, int idx)
        {
            //transform.SendMessage("ScrollCellContent", objectsToFill[idx]);
            var content = transform.GetComponent<IScrollCellContent<T>>();
            if (content != null)
            {
                content.ScrollCellContent(idx, objectsToFill[idx]);
            }
        }
    }

    public class LoopScrollCallbackSource<T> : LoopScrollDataSource
    {
        private Action<int, Transform, T> _updateCallback;
        private List<T> _objectsToFill;

        public LoopScrollCallbackSource(List<T> objectsToFill, Action<int, Transform, T> updateCallback)
        {
            _objectsToFill = objectsToFill;
        }

        public override void ProvideData(Transform transform, int idx)
        {
            if (_updateCallback != null)
            {
                _updateCallback.Invoke(idx, transform, _objectsToFill[idx]);
            }
        }
    }
}