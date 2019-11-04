using System;
using System.Collections.Generic;
using System.Text;

namespace ExampleConsole.Actions
{
    class NameChangeAction
    {
        public string Name { get; }

        public NameChangeAction(string name)
        {
            Name = name;
        }
    }
}
