namespace Mentula.GuiItems.Core
{
    using Interfaces;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public partial class GuiItem : IDisposable, IToggleable
    {
        /// <summary>
        /// A collection for storing <see cref="GuiItem"/> in a list with a specified owner.
        /// </summary>
#if !DEBUG
        [System.Diagnostics.DebuggerStepThrough]
#endif
        public class GuiItemCollection : IList<GuiItem>
        {
            /// <summary>
            /// Gets the <see cref="GuiItem"/> that owns this <see cref="GuiItemCollection"/>.
            /// </summary>
            public GuiItem Owner { get; private set; }
            /// <summary>
            /// Gets or sets the items in this <see cref="GuiItemCollection"/>.
            /// </summary>
            protected List<GuiItem> Items { get; set; }

            /// <summary>
            /// Get the number of elements actually contained in the <see cref="GuiItemCollection"/>.
            /// </summary>
            public int Count { get { return Items.Count; } }
            /// <summary>
            /// Gets a value indicating whether the collection is read-only.
            /// </summary>
            public virtual bool IsReadOnly { get { return false; } }
            /// <summary>
            /// Initializes a new instance of the <see cref="GuiItemCollection"/> class.
            /// </summary>
            /// <param name="owner"> A <see cref="GuiItem"/> that owns the collection. </param>
            public GuiItemCollection(GuiItem owner) { Owner = owner; Items = new List<GuiItem>(); }
            /// <summary>
            /// Indicates the <see cref="GuiItem"/> at the specified indexed location in the collection.
            /// </summary>
            /// <param name="index"> The index of the <see cref="GuiItem"/> to retriece from the <see cref="GuiItemCollection"/>. </param>
            /// <returns> The <see cref="GuiItem"/> located at the specified index location within the <see cref="GuiItemCollection"/>. </returns>
            /// <exception cref="ArgumentOutOfRangeException"> The index value is less than zero or is greater than or equal to the number of <see cref="GuiItem"/> in the collection. </exception>
            public virtual GuiItem this[int index] { get { return Items[index]; } set { Items[index] = value; } }
            /// <summary>
            /// Indicates a <see cref="GuiItem"/> with the specified key in the collection.
            /// </summary>
            /// <param name="key"> The <see cref="Name"/> of the <see cref="GuiItem"/> to retrieve from the <see cref="GuiItemCollection"/>. </param>
            /// <returns> The <see cref="GuiItem"/> with the specified key within the collection. </returns>
            public virtual GuiItem this[string key] { get { return Items.First(i => i.Name == key); } set { Items[Items.FindIndex(i => i.Name == key)] = value; } }
            /// <summary>
            /// Retrieves the index of the specified <see cref="GuiItem"/> in the <see cref="GuiItemCollection"/>.
            /// </summary>
            /// <param name="item"> The <see cref="GuiItem"/> to locate in the collection. </param>
            /// <returns> A zero-based index value that represents the position of the specified <see cref="GuiItem"/> in the <see cref="GuiItemCollection"/>. </returns>
            public virtual int IndexOf(GuiItem item) { return Items.IndexOf(item); }
            /// <summary>
            /// Instert the specified <see cref="GuiItem"/> to the <see cref="GuiItemCollection"/> at the specified index.
            /// </summary>
            /// <param name="index"> The index of the <see cref="GuiItem"/> used to insert the Item. </param>
            /// <param name="item"> The <see cref="GuiItem"/> to add to the collection. </param>
            public virtual void Insert(int index, GuiItem item) { Items.Insert(index, item); }
            /// <summary>
            /// Removes a <see cref="GuiItem"/> from the <see cref="GuiItemCollection"/> at the specifed indexed location.
            /// </summary>
            /// <param name="index"> The index value of the <see cref="GuiItem"/> to remove. </param>
            public virtual void RemoveAt(int index) { Items.RemoveAt(index); }
            /// <summary>
            /// Adds the specified <see cref="GuiItem"/> to the <see cref="GuiItemCollection"/>.
            /// </summary>
            /// <param name="item"> The <see cref="GuiItem"/> to add to the collection. </param>
            public virtual void Add(GuiItem item) { Items.Add(item); }
            /// <summary>
            /// Removes all <see cref="GuiItem"/> from the <see cref="GuiItemCollection"/>.
            /// </summary>
            public virtual void Clear() { Items.Clear(); }
            /// <summary>
            /// Determines whether the specified <see cref="GuiItem"/> is a member of the <see cref="GuiItemCollection"/>.
            /// </summary>
            /// <param name="item"> The <see cref="GuiItem"/> to locate in the collection. </param>
            /// <returns> true if the <see cref="GuiItem"/> is a member of the collection; otherwise false. </returns>
            public virtual bool Contains(GuiItem item) { return Items.Contains(item); }
            /// <summary>
            /// Copies the entire contents of this <see cref="GuiItemCollection"/> to a compatible one-dimensional <see cref="GuiItem"/>[],
            /// starting at the specified index of the target array.
            /// </summary>
            /// <param name="array"> 
            /// The one-dimensional <see cref="GuiItem"/>[] that is the destination of the elements copied from the current collection.
            /// The array must have zero-based indexing.
            /// </param>
            /// <param name="arrayIndex"> The zero-based index in array at which copying begins. </param>
            /// <exception cref="ArgumentNullException"> Array is null. </exception>
            /// <exception cref="ArgumentOutOfRangeException"> index is less than 0. </exception>
            /// <exception cref="ArgumentException"> 
            /// Array is multidimensional. -or- 
            /// The number of elements in the source collection is greater than the available space from index to the end of array. </exception>
            public virtual void CopyTo(GuiItem[] array, int arrayIndex) { Items.CopyTo(array, arrayIndex); }
            /// <summary>
            /// Removes the specified <see cref="GuiItem"/> from the <see cref="GuiItemCollection"/>.
            /// </summary>
            /// <param name="item"> The <see cref="GuiItem"/> to remove from the <see cref="GuiItemCollection"/>. </param>
            /// <returns>
            /// true if item is successfully removed; otherwise, false. This method also returns false if item was not found.
            /// </returns>
            public virtual bool Remove(GuiItem item) { return Items.Remove(item); }
            /// <summary>
            /// Retrieves a refrence to an enumerator object that is used to iterate over a <see cref="GuiItemCollection"/>.
            /// </summary>
            /// <returns> An <see cref="IEnumerator{GuiItem}"/>. </returns>
            public virtual IEnumerator<GuiItem> GetEnumerator() { return Items.GetEnumerator(); }
            IEnumerator IEnumerable.GetEnumerator() { return Items.GetEnumerator(); }
        }
    }
}