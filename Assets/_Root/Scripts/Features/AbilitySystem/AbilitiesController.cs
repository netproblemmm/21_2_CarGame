using Tools;
using System;
using UnityEngine;
using JetBrains.Annotations;
using Features.AbilitySystem.Abilities;

namespace Features.AbilitySystem
{
    internal interface IAbilitiesController
    { }

    internal class AbilitiesController : BaseController
    {
        private readonly IAbilitiesView _view;
        private readonly IAbilitiesRepository _repository;
        private readonly IAbilityActivator _abilityActivator;


        public AbilitiesController(
            [NotNull] IAbilityItem[] abilityItems,
            [NotNull] IAbilityActivator abilityActivator,
            [NotNull] IAbilitiesView abilitiesView,
            [NotNull] IAbilitiesRepository abilitiesRepository)
        {
            if (abilityItems == null)
                throw new ArgumentNullException(nameof(abilityItems));

            _abilityActivator = abilityActivator ?? throw new ArgumentNullException(nameof(abilityActivator));
            _view = abilitiesView ?? throw new ArgumentNullException(nameof(abilitiesView));
            _repository = abilitiesRepository ?? throw new ArgumentNullException(nameof(abilitiesRepository));

            _view.Display(abilityItems, OnAbilityViewClicked);
        }

        private void OnAbilityViewClicked(string abilityId)
        {
            if (_repository.Items.TryGetValue(abilityId, out IAbility ability))
                ability.Apply(_abilityActivator);
        }
    }
}
