using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octothorpe {
  interface Treelike<T> {

  }
  class Branch<T> : Treelike<T> {
    T value;
    public T Root {
      get { return this.value; }
      set { this.value = value; }
    }

    // List of children branches under this root
    List<Treelike<T>> children;
    public List<Treelike<T>> Children {
      get { return this.children; }
      set { this.children = value; }
    }

    // Construct a branch
    public Branch(T root) {
      this.root = root;
      this.children = new List<Treelike<T>>();
    }

    // Insert branches into the children branches
    public Treelike<T> Insert(Treelike<T> node) {
      this.children.Add(node);
      return this;
    }

    // Create a copy of the branch without the children branches
    public Treelike<T> New() {
      return new Branch<T>(this.value);
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

      Console.WriteLine(test.Root);
      for (int i = 0; i < test.Children.Count; i++) {
        Console.WriteLine(test.Children.ElementAt(i).Value);
      }

      // And thus, there was void
      Console.ReadKey();
      return Program.Success;
    }
  }
}
