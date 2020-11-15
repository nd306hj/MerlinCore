using Merlin2d.Game.Items;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MerlinGame
{
    class Backpack : IInventory
    {
        IItem[] items;
        int position = 0;

        public Backpack(int count)
        {
            items = new IItem[count];
        }
        public void AddItem(IItem item)
        {
            items[position++] = item;
        }

        public void DropAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerator<IItem> GetEnumerator()
        {
            foreach (IItem item in items)
            {
                if (item == null)
                {
                    break;
                }
                yield return item;
            }
        }

        public IItem GetItem()
        {
            throw new NotImplementedException();
        }

        public void RemoveItem(IItem item)
        {
            throw new NotImplementedException();
        }

        public void RemoveItem(int index)
        {
            throw new NotImplementedException();
        }

        public void ShiftLeft()
        {
            throw new NotImplementedException();
        }

        public void ShiftRight()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return items.GetEnumerator();
        }
    }
}
