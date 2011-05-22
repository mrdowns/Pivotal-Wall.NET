using System;
using System.Collections.Generic;
using pivotal.wall.model;

namespace pivotal.wall.web.test.Fixtures
{
    public class NewStory
    {
        private int _points;
        private State _state;
        private string _title;
        private string[] _labels;
        private string _owner = Guid.NewGuid().ToString();

        protected NewStory(){}

        public static NewStory With { get { return new NewStory(); } }

        public NewStory Points(int points)
        {
            _points = points;
            return this;
        }

        public NewStory State(State state)
        {
            _state = state;
            return this;
        }

        public NewStory Title(string title)
        {
            _title = title;
            return this;
        }

        public NewStory Labels(params string[] labels)
        {
            _labels = labels;
            return this;
        }

        public NewStory Owner(string owner)
        {
            _owner = owner;
            return this;
        }

        public NewStory NoOwner()
        {
            _owner = null;
            return this;
        }

        public static implicit operator Story(NewStory story)
        {
            return story.Create();
        }

        public Story Create()
        {
            var story = new Story
            {
                Points = _points,
                State = _state,
                Title = _title,
                Owner = _owner,
                Labels = _labels == null ? null : new List<string>(_labels)
            };
            return story;
        }
    }
}