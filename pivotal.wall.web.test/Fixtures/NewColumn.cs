using System.Linq;
using pivotal.wall.web.Helpers;

namespace pivotal.wall.web.test.Fixtures
{
    public class NewColumn
    {
        private string[] _labels;
        private string[] _states;

        protected NewColumn(){}

        public static NewColumn With { get { return new NewColumn(); } }

        public NewColumn Labels(params string[] labels)
        {
            _labels = labels;
            return this;
        }

        public NewColumn States(params string[] states)
        {
            _states = states;
            return this;
        }

        public static implicit operator Column(NewColumn column)
        {
            return column.Create();
        }

        public Column Create()
        {
            var column = new Column();
            
            foreach (var state in _states ?? new string[]{})
            {
                column.States.Add(state);
            }

            foreach (var label in _labels ?? new string[]{})
            {
                column.Labels.Add(label);
            }

            return column;
        }
    }
}