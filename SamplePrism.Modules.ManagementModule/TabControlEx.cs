﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace SamplePrism.Modules.ManagementModule
{
    [TemplatePart(Name = "PART_ItemsHolder", Type = typeof(Panel))]
    public class TabControlEx : TabControl
    {
        private Panel m_itemsHolder = null;

        public TabControlEx() : base()
        {
            this.ItemContainerGenerator.StatusChanged += ItemContainerGenerator_StatusChanged;
        }

        private void ItemContainerGenerator_StatusChanged(object sender, EventArgs e)
        {
            if (this.ItemContainerGenerator.Status == GeneratorStatus.ContainersGenerated)
            {
                this.ItemContainerGenerator.StatusChanged -= ItemContainerGenerator_StatusChanged;
                UpdateSelectedItem();
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            m_itemsHolder = GetTemplateChild("PART_ItemsHolder") as Panel;
            UpdateSelectedItem();
        }

        protected override void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
        {
            base.OnItemsSourceChanged(oldValue, newValue);
            UpdateSelectedItem();
        }

        protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnItemsChanged(e);

            if (m_itemsHolder == null) return;

            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                case NotifyCollectionChangedAction.Remove:
                    if (e.OldItems != null)
                    {
                        foreach (var item in e.OldItems)
                        {
                            ContentPresenter cp = FindChildContentPresenter(item);
                            if (cp != null)
                            {
                                m_itemsHolder.Children.Remove(cp);
                            }
                        }
                    }

                    UpdateSelectedItem();
                    break;
                case NotifyCollectionChangedAction.Replace:
                    throw new NotImplementedException("Replace not implemented yet");
                case NotifyCollectionChangedAction.Reset:
                    m_itemsHolder.Children.Clear();
                    break;
                default:
                    break;
            }
        }

        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            base.OnSelectionChanged(e);
            UpdateSelectedItem();
        }

        void UpdateSelectedItem()
        {
            if (m_itemsHolder == null)
                return;

            TabItem item = GetSelectedTabItem();
            if (item != null)
                CreateChildContentPresenter(item);

            foreach (ContentPresenter child in m_itemsHolder.Children)
            {
                child.Visibility = ((child.Tag as TabItem).IsSelected) ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        ContentPresenter CreateChildContentPresenter(object item)
        {
            if (item == null) return null;

            ContentPresenter cp = FindChildContentPresenter(item);

            if (cp != null) return cp;

            cp = new ContentPresenter();
            cp.Content = (item is TabItem) ? (item as TabItem).Content : item;
            cp.ContentTemplate = this.SelectedContentTemplate;
            cp.ContentTemplateSelector = this.SelectedContentTemplateSelector;
            cp.ContentStringFormat = this.SelectedContentStringFormat;
            cp.Visibility = Visibility.Collapsed;
            cp.Tag = (item is TabItem) ? item : (this.ItemContainerGenerator.ContainerFromItem(item));
            m_itemsHolder.Children.Add(cp);
            return cp;
        }

        ContentPresenter FindChildContentPresenter(object data)
        {
            if (data is TabItem)
                data = (data as TabItem).Content;

            if (data == null) return null;

            if (m_itemsHolder == null) return null;

            foreach (ContentPresenter cp in m_itemsHolder.Children)
            {
                if (cp.Content == data)
                    return cp;
            }

            return null;
        }

        protected TabItem GetSelectedTabItem()
        {
            object selectedItem = base.SelectedItem;
            if (selectedItem == null) return null;
            TabItem item = selectedItem as TabItem;
            if (item == null)
                item = base.ItemContainerGenerator.ContainerFromIndex(base.SelectedIndex) as TabItem;
            return item;
        }
    }
}
