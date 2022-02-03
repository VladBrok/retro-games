using System;
using UnityEngine;
using NUnit.Framework;
using NSubstitute;
using Asteroids;

namespace Editor.Tests
{
    internal class WeaponTests
    {
        private readonly float _defaultFireRate = 1;

        [Test]
        public void Update_DeltaTimeIsHigherThanFireRate_RaisesFiredEvent()
        {
            var weapon = CreateDefaultWeapon(inputFireReturns: true);
            bool wasRaised = false;
            weapon.Fired += () => wasRaised = true;

            weapon.Update(_defaultFireRate * 2);

            Assert.IsTrue(wasRaised);
        }

        [Test]
        public void Update_DeltaTimeIsLessThanFireRate_NotRaisesFiredEvent()
        {
            var weapon = CreateDefaultWeapon(inputFireReturns: true);
            bool wasRaised = false;
            weapon.Fired += () => wasRaised = true;

            weapon.Update(_defaultFireRate / 2);

            Assert.IsFalse(wasRaised);
        }

        [Test]
        public void Update_InputFireReturnsFalse_NotRaisesFiredEvent()
        {
            var weapon = CreateDefaultWeapon(inputFireReturns: false);
            bool wasRaised = false;
            weapon.Fired += () => wasRaised = true;

            weapon.Update(_defaultFireRate * 2);

            Assert.IsFalse(wasRaised);
        }

        private Weapon<int> CreateDefaultWeapon(bool inputFireReturns = false)
        {
            var input = Substitute.For<IWeaponInput>();
            var pool = Substitute.For<IPool<int>>();
            var weapon = new Weapon<int>(input, pool, _defaultFireRate);
            input.Fire.Returns(inputFireReturns);
            return weapon;
        }
    }
}
