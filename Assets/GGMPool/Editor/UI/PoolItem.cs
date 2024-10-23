using GGMPool;
using System;
using UnityEngine;
using UnityEngine.UIElements;

public class PoolItem : MonoBehaviour
{
    private Label _nameLabel;
    private Button _deleteBtn;
    private VisualElement _rootElem;

    public event Action<PoolItem> OnDeleteEvent;
    public event Action<PoolItem> OnSelectEvent;

    public string Name
    {
        get => _nameLabel.text;
        set => _nameLabel.text = value;
    }

    public PoolingItemSO itemSO;

    public bool IsActive
    {
        get => _rootElem.ClassListContains("active");
        set
        {
            if (value)
            {
                _rootElem.AddToClassList("active");
            }
            else
            {
                _rootElem.RemoveFromClassList("active");
            }
        }
    }

    public PoolItem(VisualElement root, PoolingItemSO itemSO)
    {
        this.itemSO = itemSO;
        _rootElem = root.Q("PoolItem");
        _nameLabel = root.Q<Label>("ItemName");
        _deleteBtn = root.Q<Button>("BtnDelete");
        _deleteBtn.RegisterCallback<ClickEvent>(evt =>
        {
            OnDeleteEvent?.Invoke(this);
        });

        _rootElem.RegisterCallback<ClickEvent>(evt =>
        {
            OnSelectEvent?.Invoke(this);
            evt.StopPropagation();
        });
    }

}
