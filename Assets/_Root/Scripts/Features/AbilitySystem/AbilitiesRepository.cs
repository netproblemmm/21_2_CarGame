using System.Collections.Generic;
using Features.AbilitySystem.Abilities;

namespace Features.AbilitySystem
{
    internal interface IAbilitiesRepository : IRepository
    {
        IReadOnlyDictionary<string, IAbility> Items { get; }
    }

    internal class AbilitiesRepository : Repository<string, IAbility, IAbilityItem>, IAbilitiesRepository
    {
        public AbilitiesRepository(IEnumerable<IAbilityItem> configs) : base(configs)
        { }

        protected override string GetKey(IAbilityItem config) => config.Id;

        protected override IAbility CreateItem(IAbilityItem config) =>
            config.Type switch
            {
                AbilityType.Gun => new GunAbility(config),
                _ => StubAbility.Default
            };
    }
}
