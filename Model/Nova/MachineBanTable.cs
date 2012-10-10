using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Nova
{
    public class MachineBanTable
    {
        private readonly Dictionary<Machine, DateTime> lookupTable;
        
        /// <summary>
        /// Gets the total number of machines registered in the lookup table.
        /// </summary>
        public long Count
        {
            get { return lookupTable.Count; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MachineLookupTable"/> class.
        /// </summary>
        public MachineBanTable()
        {
            lookupTable = new Dictionary<Machine, DateTime>(10000);
        }

        /// <summary>
        /// Purges all Machines from the database and releases all active IDs.
        /// </summary>
        public void Reset()
        {
            lookupTable.Clear();
        }

        /// <summary>
        /// Returns true if the the ID, exists.
        /// </summary>
        public bool IdExists(Machine machine)
        {
            return (IndexOf(machine) >= 0);
        }

        public int IndexOf(Machine machine)
        {
            int count = 0;
            foreach (var entity in lookupTable)
            {
                if ((machine.Identity == entity.Key.Identity) && (machine.PublicEndPoint == entity.Key.PublicEndPoint))
                {
                    return count;
                }
                count++;
            }
            return -1;
        }

        public Machine MachineAt(int index)
        {
            return lookupTable.ElementAt(index).Key;
        }

        public void Add(Machine machine, DateTime paroleTime)
        {
            if (!IdExists(machine))
            {
                lookupTable.Add(machine, paroleTime);
            }
            else
            {
                lookupTable[MachineAt(IndexOf(machine))] = paroleTime;
            }
        }

        public DateTime Get(Machine machine)
        {
            return lookupTable.ElementAt(IndexOf(machine)).Value;
        }

        public void Remove(Machine machine)
        {
            if (IdExists(machine))
            {
                lookupTable.Remove(MachineAt(IndexOf(machine)));
            }
        }
    }
}
