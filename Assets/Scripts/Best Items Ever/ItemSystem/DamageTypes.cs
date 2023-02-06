[System.Flags]public enum DamageTypes
{
    Fire = 1,
    Bleed = 2,
    Piercing = 4,
    Corruption = 8,
}

/// <summary>
/// DamageTypes allows for weapons to have more than one
/// effect applied to it. System.Flags allows this functionality
/// of applying more than one
/// </summary>