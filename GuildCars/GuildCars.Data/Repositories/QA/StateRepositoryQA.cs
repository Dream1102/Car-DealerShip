using GuildCars.Data.Interfaces;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Repositories.QA
{
    public class StateRepositoryQA : IStateRepository
    {
        private List<State> _states = new List<State>();

        public StateRepositoryQA()
        {
            SeedStateRepository();
        }

        private void SeedStateRepository()
        {
            State state1 = new State();
            state1.StateAbbreviation = "KY";
            state1.StateName = "Kentucky";
            _states.Add(state1);

            State state2 = new State();
            state2.StateAbbreviation = "OH";
            state2.StateName = "Ohio";
            _states.Add(state2);

            State state3 = new State();
            state3.StateAbbreviation = "IN";
            state3.StateName = "Indiana";
            _states.Add(state3);

            State state4 = new State();
            state4.StateAbbreviation = "IL";
            state4.StateName = "Illinois";
            _states.Add(state4);

            State state5 = new State();
            state5.StateAbbreviation = "PA";
            state5.StateName = "Pennsylvania";
            _states.Add(state5);
        }
            public List<State> GetAllStates()
        {
            List<State> states = new List<State>();

            foreach (var state in _states)
            {
                states.Add(state);
            }
            return states;
        }
    }
}
