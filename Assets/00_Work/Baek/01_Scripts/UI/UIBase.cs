using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public abstract class UIBase : MonoBehaviour
{
    protected UIDocument _doc;
    protected Dictionary<TabBarType, Button> _tabBarDictionary = new Dictionary<TabBarType, Button>();
    protected Dictionary<Button, VisualElement> _tabBarOutlineDictionary = new Dictionary<Button, VisualElement>();
    protected virtual void Awake()
    {
        _doc = GetComponent<UIDocument>();
    }
}
