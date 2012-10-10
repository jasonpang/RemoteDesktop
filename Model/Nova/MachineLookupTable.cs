using System;
using System.Collections.Generic;

namespace Model.Nova
{
    public class MachineLookupTable
    {
        private readonly Dictionary<String, LookupMachine> lookupTable;
        private readonly Dictionary<LookupMachine, String> reverseLookupTable;

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
        public MachineLookupTable()
        {
            lookupTable = new Dictionary<string, LookupMachine>(10000);
            reverseLookupTable = new Dictionary<LookupMachine, string>(10000);
        }

        /// <summary>
        /// Purges all Machines from the database and releases all active IDs.
        /// </summary>
        public void Reset()
        {
            lookupTable.Clear();
            reverseLookupTable.Clear();
        }

        public LookupMachine GetMachineById(string key)
        {
            return lookupTable[key];
        }

        public string GetIdByMachine(LookupMachine machine)
        {
            return reverseLookupTable[machine];
        }

        /// <summary>
        /// Returns true if the the ID, exists.
        /// </summary>
        public bool IdExists(string id)
        {
            return (lookupTable.ContainsKey(id));
        }

        /// <summary>
        /// Returns true if the the ID, exists.
        /// </summary>
        public bool IdExists(LookupMachine machine)
        {
            return (reverseLookupTable.ContainsKey(machine));
        }

        public void Add(string id, LookupMachine machine)
        {
            if (!IdExists(id))
            {
                lookupTable.Add(id, machine);
                reverseLookupTable.Add(machine, id);
            }
            else
            {
                lookupTable[id] = machine;
                reverseLookupTable[machine] = id;
            }
        }

        public void Add(LookupMachine machine, string id)
        {
            if (!IdExists(machine))
            {
                reverseLookupTable.Add(machine, id);
                lookupTable.Add(id, machine);
            }
            else
            {
                reverseLookupTable[machine] = id;
                lookupTable[id] = machine;
            }
        }

        public void Remove(string id)
        {
            if (IdExists(id))
            {
                reverseLookupTable.Remove(GetMachineById(id));
                lookupTable.Remove(id);
            }
        }

        public void Remove(LookupMachine machine)
        {
            if (IdExists(machine))
            {
                lookupTable.Remove(GetIdByMachine(machine));
                reverseLookupTable.Remove(machine);
            }
        }
    }
}