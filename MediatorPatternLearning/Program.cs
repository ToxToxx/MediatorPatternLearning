using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatorPatternLearning
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PlayerCounselorMediator mediator = new PlayerCounselorMediator();
            Colleague builder = new BuilderColleague(mediator);
            Colleague guard = new GuardColleague(mediator);
            Colleague ruler = new RulerColleague(mediator);
            mediator.Builder = builder;
            mediator.Guard = guard;
            mediator.Ruler = ruler;
            builder.Send("Castle", 50);
            Console.WriteLine();
            guard.Send("Armory", 5);
            Console.WriteLine();
            ruler.Send("Library", 100);

            Console.Read();
        }
    }

    abstract class Mediator
    {
        public abstract void Send(string place, int cost, Colleague colleague);
    }
    abstract class Colleague
    {
        protected Mediator mediator;

        public Colleague(Mediator mediator)
        {
            this.mediator = mediator;
        }

        public virtual void Send(string place, int cost)
        {
            mediator.Send(place, cost, this);
        }
        public abstract void Notify(string place, int cost);
    }
    // builders colleague
    class BuilderColleague : Colleague
    {
        public BuilderColleague(Mediator mediator)
            : base(mediator)
        { }

        public override void Notify(string place, int cost)
        {
            Console.WriteLine($"Need to build: {place} | price of the building: {cost} gold") ;
        }
    }
    // guard colleague
    class GuardColleague : Colleague
    {
        public GuardColleague(Mediator mediator)
            : base(mediator)
        { }

        public override void Notify(string place, int cost)
        {
            Console.WriteLine($"Guard from the {place} comes | price of the guard: {cost} gold ");
        }
    }
    // king colleague
    class RulerColleague : Colleague
    {
        public RulerColleague(Mediator mediator)
            : base(mediator)
        { }

        public override void Notify(string place, int cost)
        {
            Console.WriteLine($"The {place} needs a ruler| price of the ruler: {cost} gold ");
        }
    }
    //concrete mediator
    class PlayerCounselorMediator : Mediator
    {
        public Colleague Builder { get; set; }
        public Colleague Guard { get; set; }
        public Colleague Ruler { get; set; }
        public override void Send(string place, int cost, Colleague colleague)
        {
            // if the building is build - send a message to a guard
            if (Ruler == colleague)
            {
                Console.WriteLine($"I want to build a  {place}");
                Builder.Notify(place, cost);
            }
               
                

            //if the guard patroling - send a message to a ruler to come 
            else if (Guard == colleague)
            {
                Console.WriteLine($"The place is now patroling, need a ruler to come");
                Ruler.Notify(place, cost);
            }
                

            //if the governor wants something to build - send a message to a builders
            else if (Builder == colleague)
            {
                Console.WriteLine($"The place is now building, need a patroling guard");
                Guard.Notify(place, cost);
            }
                
        }
    }
}
