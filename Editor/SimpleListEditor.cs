using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace UnityForgeEditor.GameConfigs
{
    public abstract class SimpleListEditor<T> where T : class
    {
        private const int DEFAULT_ITEM_LIST_EDITOR_WIDTH = 150;

        private List<T> items_;
        private int selectedItemIndex_ = -1;

        private int itemListEditorWidth_ = DEFAULT_ITEM_LIST_EDITOR_WIDTH;
        private Vector2 listScrollPos_ = Vector2.zero;
        private Vector2 selectedItemConfigScrollPos_ = Vector2.zero;

        public SimpleListEditor(List<T> items)
        {
            items_ = items;
        }

        public SimpleListEditor(List<T> items, int itemListEditorWidth)
        {
            items_ = items;
            itemListEditorWidth_ = itemListEditorWidth;
        }

        protected int SelecteItemIndex
        {
            get { return selectedItemIndex_; }
        }

        protected T SelectedItem
        {
            get
            {
                T selectedItem = null;
                if (items_ != null && selectedItemIndex_ >= 0 && selectedItemIndex_ < items_.Count)
                {
                    selectedItem = items_[selectedItemIndex_];
                }
                return selectedItem;
            }
            set
            {
                selectedItemIndex_ = items_.IndexOf(value);
            }
        }

        public void DrawSimpleItemListEditorGUI()
        {
            EditorGUILayout.BeginHorizontal();
            {
                ItemsGUI();
                SelectedItemConfigGUI();
            }
            EditorGUILayout.EndHorizontal();
        }

        protected abstract T CreateItem();
        protected abstract void DrawItemGUI(T item);
        protected abstract string GetItemButtonText(T item);

        protected void Add()
        {
            if (items_ != null)
            {
                var item = CreateItem();
                items_.Add(item);
            }
        }

        protected void Insert(int index, T item)
        {
            if (items_ != null)
            {
                items_.Insert(index, item);
                SelectedItem = item;
            }
        }

        protected void Remove(int index)
        {
            if (items_ != null)
            {
                var item = items_[index];
                if (item == SelectedItem)
                {
                    SelectedItem = null;
                }
                items_.RemoveAt(index);
            }
        }

        private void ItemsGUI()
        {
            EditorGUILayout.BeginVertical(GUILayout.Width(itemListEditorWidth_));
            {
                ListEditGUI();
                ListItemsGUI();
            }
            EditorGUILayout.EndVertical();
        }

        private void ListEditGUI()
        {
            EditorGUILayout.BeginVertical(UnityForgeEditorStyles.BoxStyle);
            {
                if (GUILayout.Button("Add Item"))
                {
                    Add();
                }

                if (GUILayout.Button("Remove Item") && SelectedItem != null)
                {
                    Remove(selectedItemIndex_);
                }
            }
            EditorGUILayout.EndVertical();
        }

        private void ListItemsGUI()
        {
            EditorGUILayout.BeginVertical(UnityForgeEditorStyles.BoxStyle);
            {
                listScrollPos_ = EditorGUILayout.BeginScrollView(listScrollPos_);
                {
                    if (items_ != null)
                    {
                        foreach (var item in items_)
                        {
                            var style = (item == SelectedItem) ? UnityForgeEditorStyles.ListItemSelectedStyle : UnityForgeEditorStyles.ListItemStyle;
                            if (GUILayout.Button(GetItemButtonText(item), style))
                            {
                                if (SelectedItem != item)
                                {
                                    SelectedItem = item;
                                }
                            }
                        }
                    }
                }
                EditorGUILayout.EndScrollView();
            }
            EditorGUILayout.EndVertical();
        }

        private void SelectedItemConfigGUI()
        {
            selectedItemConfigScrollPos_ = EditorGUILayout.BeginScrollView(selectedItemConfigScrollPos_);
            {
                if (SelectedItem != null)
                {
                    DrawItemGUI(SelectedItem);
                }
            }
            EditorGUILayout.EndScrollView();
        }
    }
}
