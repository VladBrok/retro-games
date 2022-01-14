using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using SnakeGame.Helpers;

namespace SnakeGame.Editor.Tests.Helpers
{
    public class FieldTests
    {
        [Test]
        public void Update_TargetOutsideOfField_RaisesEvent()
        {
            var fieldArea = new Bounds(Vector2.zero, Vector2.one);
            var target = Substitute.For<IBody>();
            target.Position.Returns((Vector2)fieldArea.max + Vector2.one);
            var field = new Field(target, fieldArea);
            bool eventRaised = false;
            field.TargetLeftField += () => eventRaised = true;

            field.Update();

            Assert.That(eventRaised);
        }
    }
}
