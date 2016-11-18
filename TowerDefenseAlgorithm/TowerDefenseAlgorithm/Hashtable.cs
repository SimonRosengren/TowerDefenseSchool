using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TowerDefenseAlgorithm
{
    class Hashtable
    {
        private LinkedList<object> insertionOrder = new LinkedList<object>();
        private LinkedList<Entry>[] table;
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="size"></param>
        public Hashtable(int size)
        {
            table = new LinkedList<Entry>[size];
            for (int i = 0; i < size; i++)
            {
                table[i] = new LinkedList<Entry>();
            }
        }
        /// <summary>
        /// Genererar och returnerar en hashkod beroende av nyckeln
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private int HashIndex(object key)
        {
            int hashCode = key.GetHashCode();
            hashCode = hashCode % table.Length;
            return (hashCode < 0) ? -hashCode : hashCode;
        }
        /// <summary>
        /// Returnera value från ett entry, där nyckeln stämmer övrens.
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Returnera Entry.value om nyckel finns, annars null</returns>
        public object Get(object key)
        {
            int hashIndex = HashIndex(key);
            if (table[hashIndex].Contains(new Entry(key, null)))
            {
                Entry entry = table[hashIndex].Find(new Entry(key, null)).Value;
                return entry.value;
            }
            return null;
        }
        /// <summary>
        /// Returnerar längden på insertionOrder
        /// </summary>
        /// <returns>insertionOrder.Count</returns>
        public int Count()
        {
            return insertionOrder.Count;
        }
        /// <summary>
        /// Adderar ett nytt element till ahshtabellen samt till insertion sort sålänge inte nyckeln redan finns. Returnerar false om nyckel finns
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns>True om addering är godkänd</returns>
        public bool Put(object key, object value)
        {
            int hashIndex = HashIndex(key);
            if (Get(key) != null)
            {
                return false;
            }
            table[hashIndex].AddLast(new Entry(key, value));
            insertionOrder.AddLast(value);
            return true;
        }
        /// <summary>
        /// Tar bort ett entry från tabellen samt värdet med samma nyckel från insertionOrder.
        /// </summary>
        /// <param name="key"></param>
        public void Remove(object key)
        {
            int hashIndex = HashIndex(key);
            if (table[hashIndex].Contains(new Entry(key, null)))
            {
                table[hashIndex].Remove(new Entry(key, null));
                insertionOrder.Remove(key);
            }
        }
    }
}
