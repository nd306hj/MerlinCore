using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Merlin2d.Game.Items
{
    public interface IInventory : IEnumerable<IItem>
    {
        IItem GetItem();
        void AddItem(IItem item);

        void RemoveItem(IItem item);

        void RemoveItem(int index);

        void ShiftLeft();

        void ShiftRight();
    }
}
