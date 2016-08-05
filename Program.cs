using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octothorpe {
  delegate bool Condition<T>(T input);

  interface aTree<T> {
    Branch<T> Insert(Branch<T> node);
  }

  class Branch<T> : aTree<T> {
    // Root value
    T root;
    public T Root {
      get { return this.root; }
      set { this.root = value; }
    }

    // List of children branches under this root
    List<Branch<T>> children;
    public List<Branch<T>> Children {
      get { return this.children; }
      set { this.children = value; }
    }

    // Construct a branch
    public Branch(T root) {
      this.root = root;
      this.children = new List<Branch<T>>();
    }

    // Insert branches into the children branches
    public Branch<T> Insert(Branch<T> node) {
      this.children.Add(node);
      return this;
    }

    // Create a copy of the branch without the children branches
    public Branch<T> New() {
      return new Branch<T>(this.root);
    }
  }

  class Transition<T> {
    public static int Not = -1;

    public static int All = 0;
    public static int Alphabet = 1;
    public static int Digit = 2;

    Condition<T> condition;

    public Transition(int preset) {
      switch (Math.Abs(preset)) {
        case 0 /* Transition.All */:
          this.condition = delegate(T input) {
            bool evaluation = true;
            return Negate(evaluation, preset);
          };
          break;
        case 1 /* Transition.Alphabet */:
          this.condition = delegate (T input) {
            bool evaluation = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".Contains(input + "");
            return Negate(evaluation, preset);
          };
          break;
        case 2 /* Transition.Digit */:
          this.condition = delegate (T input) {
            bool evaluation = "0123456789".Contains(input + "");
            return Negate(evaluation, preset);
          };
          break;
        default:
          Console.WriteLine("Transition | Not implemented");
          break;
      }
    }

    public bool Negate(bool input, int preset) {
      if (preset < 0) {
        input = !input;
      }
      return input;
    }
  }

  class State : Branch<Tuple<Transition<char>, int>>, aTree<Tuple<Transition<char>, int>> {
    // Types
    public static int Alpha = 0;
    public static int Phi = 1;
    public static int Omega = 2;

    // Type of state
    int type;
    public int Type {
      get { return this.type; }
      set { this.type = value; }
    }

    // Construct a state automaton
    public State(Transition<char> root, int type) : base(new Tuple<Transition<char>, int>(root, type)) { }

    // Link to self
    public State Plus() {
      this.Insert(this);
      return this;
    }

    // Link to self and allow any
    public State Star() {
      this.Insert(this);
      this.Insert(new State(new Transition<char>(Transition<char>.All), (int) State.Phi));
      return this;
    }

    // Clone onto itself
    public State Multiply(int times) {
      aTree<Tuple<Transition<char>, int>> stack = this.New();
      for (int i = 1; i < times; i++) {
        stack.Insert(this.New());
      }
      return (State) stack;
    }

    // Add a state
    public State Connect(State node) {
      this.Insert(node.New());
      return this;
    }

    // Test an input
    public bool Run(string input) {
      for (int i = 0; i < this.Children.Count; i++) {
        Console.WriteLine(this.Children.ElementAt(i).GetType().ToString());
      }
      return true;
    }
  }

  class Program {
    // Execution codes
    public static int Success = 0;

    static int Main(string[] Args) {
      // And then, there was light
      Console.WriteLine("Octothorpe by OmnInfinity");

      // Testing branches
      Branch<int> test = new Branch<int>(0);
      Branch<int> test2 = new Branch<int>(1);

      test.Insert(test2);

      // Testing state automaton
      string input = "abcdef1";

      State variable = new State(new Transition<char>((int) Transition<char>.All), (int) State.Alpha);
      State alphabet = new State(new Transition<char>((int) Transition<char>.Alphabet), (int) State.Phi);
      State digit = new State(new Transition<char>((int) Transition<char>.Digit), (int) State.Omega);

      variable.Connect(alphabet.Multiply(6));
      variable.Run(input);

      // And thus, there was void
      Console.ReadKey();
      return Program.Success;
    }
  }
}
