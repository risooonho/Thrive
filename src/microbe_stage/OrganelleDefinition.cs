﻿using System.Collections.Generic;

/// <summary>
///   Definition for a type of an organelle. This is not a placed organelle in a microbe
/// </summary>
public class OrganelleDefinition : IRegistryType
{
    /// <summary>
    ///   User readable name
    /// </summary>
    public string Name;

    /// <summary>
    ///   One letter code for this organelle. These must be unique!
    /// </summary>
    public string Gene;

    /// <summary>
    ///   A path to a scene to display this organelle with. If empty won't have a display model
    /// </summary>
    public string DisplayScene;

    public float Mass;

    public float ChanceToCreate;

    /// <summary>
    ///   Same as ChanceToCreate but for prokaryotes (bacteria)
    /// </summary>
    public float ProkaryoteChance;

    public OrganelleComponentFactoryInfo Components;

    /// <summary>
    ///   Defines the processes this organelle does and their speed multipliers
    /// </summary>
    public Dictionary<string, float> Processes;

    public List<Hex> Hexes;

    /// <summary>
    ///   The compounds this organelle consists of (how many resources
    ///   are needed to duplicate this)
    /// </summary>
    public Dictionary<string, float> InitialComposition;

    /// <summary>
    ///   Cost of placing this organelle in the editor
    /// </summary>
    public int MPCost;

    public List<IOrganelleComponentFactory> ComponentFactories
    {
        get
        {
            return Components.Factories;
        }
    }

    public void Check(string name)
    {
        if (Components == null)
        {
            throw new InvalidRegistryData(name, this.GetType().Name,
                "No components specified");
        }

        Components.Check(name);

        if (Components.Count < 1)
        {
            throw new InvalidRegistryData(name, this.GetType().Name,
                "No components specified");
        }

        if (Mass <= 0.0f)
        {
            throw new InvalidRegistryData(name, this.GetType().Name,
                "Mass is unset");
        }

        if (Mass <= 0.0f)
        {
            throw new InvalidRegistryData(name, this.GetType().Name,
                "Mass is unset");
        }

        if (Name == string.Empty)
        {
            throw new InvalidRegistryData(name, this.GetType().Name,
                "Name is not set");
        }

        if (Gene.Length != 1)
        {
            throw new InvalidRegistryData(name, this.GetType().Name,
                "Gene needs to be 1 character long");
        }

        if (InitialComposition == null || InitialComposition.Count < 1)
        {
            throw new InvalidRegistryData(name, this.GetType().Name,
                "InitialComposition is not set");
        }

        if (Hexes == null || Hexes.Count < 1)
        {
            throw new InvalidRegistryData(name, this.GetType().Name,
                "Hexes is empty");
        }

        // TODO: check processes
    }

    public class OrganelleComponentFactoryInfo
    {
        public NucleusComponentFactory Nucleus;
        public StorageComponentFactory Storage;
        public AgentVacuoleComponentFactory AgentVacuole;
        public MovementComponentFactory Movement;
        public PilusComponentFactory Pilus;

        private readonly List<IOrganelleComponentFactory> allFactories =
            new List<IOrganelleComponentFactory>();

        private int count = -1;

        /// <summary>
        ///   The number of components
        /// </summary>
        public int Count
        {
            get
            {
                return count;
            }
        }

        public List<IOrganelleComponentFactory> Factories
        {
            get
            {
                return allFactories;
            }
        }

        /// <summary>
        ///   Checks and initializes the factory data
        /// </summary>
        public void Check(string name)
        {
            count = 0;

            if (Nucleus != null)
            {
                Nucleus.Check(name);
                allFactories.Add(Nucleus);
                count++;
            }

            if (Storage != null)
            {
                Storage.Check(name);
                allFactories.Add(Storage);
                count++;
            }

            if (AgentVacuole != null)
            {
                AgentVacuole.Check(name);
                allFactories.Add(AgentVacuole);
                count++;
            }

            if (Movement != null)
            {
                Movement.Check(name);
                allFactories.Add(Movement);
                count++;
            }

            if (Pilus != null)
            {
                Pilus.Check(name);
                allFactories.Add(Pilus);
                count++;
            }
        }
    }
}