/*
 * MIT License
 * 
 * Copyright (c) 2016 Kevin Pho (OmnInfinity) <kevinkhoapho@gmail.com>,
 *                    CAMS VEX 687 "Nerd Herd",
 *                    CAMS VEX 687-E
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

/*
 * The general imports
 * 
 * @author Kevin Pho
 */
using System; // For input/output
using System.Collections.Generic; // For collections
using System.Linq; // For selection
using System.Text; // For text manipulation
using System.Threading.Tasks; // For multitasking

/*
 * The local imports
 * 
 * @author    Kevin Pho
 */
using Octothorpe = edu.cams.vex.Octothorpe; // For localizing the workspace

/*
 * A compiler framework
 * 
 * @author    Kevin Pho
 */
namespace edu.cams.vex.Octothorpe {
  /*
   * The main program for executing builds and tests
   * 
   * @author    Kevin Pho
   */
  class Program {
    // Execution codes
    public static int Success = 0;
    public static int Failure = 1;

    static int Main(string[] args) {
      /* And then,
       * there was light */

      Console.WriteLine("Octothorpe by OmnInfinity");

      /* A world had been made;
       * from creation to destruction */

      /* Tokenizer for lexical analysis */
      
      // The states to travel between
      Table states = new Table() {
        { "q0", new State("q0").Start().End() },
        { "q1", new State("q1")               },
        { "q2", new State("q2")        .End() },
      };

      // The set of causes and effects for states
      Cause a = delegate (object input) { return "a".Contains((string) input); };
      Effect q0 = delegate (bool input) { return (string) "No input"; };
      Effect q2 = delegate (bool input) { return (string) "Even"; };

      // The set of transitions between states
      states["q0"].Connect(a, states["q1"], q0);
      states["q1"].Connect(a, states["q2"], q2);
      states["q2"].Connect(a, states["q1"], null);

      // The constructed state automaton
      Machine token = new Machine(states);
      Console.WriteLine(token.ToString());
      // token.Accepts("");
      // token.Accepts("a");
      // token.Accepts("aa");

      /* And thus,
       * there was void */

      Console.ReadKey();
      return Program.Success;
    }

    static int Update(string [] args) { return Program.Success; }
  }
}
